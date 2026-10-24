using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Page;
using GeekCliServices.Services.Ngx.Service;
using GeekCli.Commands.Ngx.Wizard.Models;
using GeekCli.Commands.Ngx.Wizard.Serialization;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Ngx.Wizard
{
    internal sealed class NgxWizardCommand : Command, INgxWizard
    {
        private const string ProcessToRun = "cmd";
        private const string BackAction = "Back";
        private const string CreatePageAction = "Create an Angular page";
        private const string CreateComponentAction = "Create an Angular component";
        private const string CreateServiceAction = "Create an Angular service";

        private readonly INgxPageService _ngxPageService;
        private readonly INgxComponentService _ngxComponentService;
        private readonly INgxServiceService _ngxServiceService;

        public NgxWizardCommand(INgxPageService ngxPageService, INgxComponentService ngxComponentService, INgxServiceService ngxServiceService)
        {
            _ngxPageService = ngxPageService;
            _ngxComponentService = ngxComponentService;
            _ngxServiceService = ngxServiceService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            ShowHeader();

            var choices = new List<string>
            {
                CreatePageAction,
                CreateComponentAction,
                CreateServiceAction
            };

            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose what you want to generate")
                    .PageSize(10)
                    .AddChoices(choices));

            if (showBackOption && action == BackAction)
            {
                return 0;
            }

            var name = AnsiConsole.Ask<string>("Angular [green]name[/] ([grey]example: user-profile[/])?");
            var command = action switch
            {
                CreateComponentAction => BuildNxCommand(name, "component", false),
                CreatePageAction => BuildNxCommand(name, "page", true),
                CreateServiceAction => BuildNxCommand(name, "service", false),
                _ => null
            };

            if (command is null)
            {
                return 1;
            }

            return RunSelectedAction(action, command);
        }

        private static NgxCommand? BuildNxCommand(string name, string itemName, bool applicationsOnly)
        {
            if (!AnsiConsole.Confirm("Is this an [green]Nx workspace[/]?", false))
            {
                return new NgxCommand(name);
            }

            var projects = GetAngularProjects(applicationsOnly);
            if (projects.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No matching Angular projects were found in the Nx workspace, or Nx could not read the project list.[/]");
                return null;
            }

            var project = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Choose the Angular [green]{(applicationsOnly ? "application" : "project")}[/] for the {itemName}")
                    .PageSize(10)
                    .AddChoices(projects));
            var folderPrompt = itemName == "component"
                ? "Workspace-relative destination [green]folder[/] beneath the project root (for example apps/<project>/src/app/Features or packages/<project>/src/lib/Features)?"
                : itemName == "service"
                    ? "Project-relative destination [green]folder[/] (for example Services or Features/Auth)?"
                    : "Destination [green]folder[/] (relative to src/app, or apps/<project>/src/app/...)?";
            var folder = AnsiConsole.Ask<string>(folderPrompt);

            if (itemName == "service")
            {
                return new NgxCommand(name, true, project, folder);
            }

            var modulePrompt = itemName == "component"
                ? "Workspace-relative declaring Angular [green]NgModule[/] file path, including .ts?"
                : "Declaring Angular [green]module[/] path?";
            var module = AnsiConsole.Ask<string>(modulePrompt);

            return new NgxCommand(name, true, project, folder, module);
        }

        private static List<string> GetAngularProjects(bool applicationsOnly = false)
        {
            var projectsResult = RunNxCommand("show projects --json");
            if (projectsResult.ExitCode != 0)
            {
                return new List<string>();
            }

            try
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new NxProjectListEntryConverter());
                var projects = JsonSerializer.Deserialize<NxProjectListEntry[]>(projectsResult.Output, options)
                    ?? Array.Empty<NxProjectListEntry>();
                return projects
                    .Select(project => project.Name)
                    .Where(project => IsAngularProject(project, applicationsOnly))
                    .OrderBy(project => project, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (JsonException)
            {
                return new List<string>();
            }
        }

        private static bool IsAngularProject(string project, bool applicationsOnly)
        {
            if (!Regex.IsMatch(project, "^[A-Za-z0-9_./-]+$"))
            {
                return false;
            }

            var result = RunNxCommand($"show project \"{project}\" --json");
            if (result.ExitCode != 0)
            {
                return false;
            }

            try
            {
                var projectConfiguration = JsonSerializer.Deserialize<NxProjectConfiguration>(result.Output);
                var hasAngularTag = projectConfiguration?.Tags?.Contains("tech:angular", StringComparer.OrdinalIgnoreCase) == true;
                return hasAngularTag
                    && (!applicationsOnly || string.Equals(projectConfiguration?.ProjectType, "application", StringComparison.OrdinalIgnoreCase));
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private static (int ExitCode, string Output) RunNxCommand(string arguments)
        {
            var startInfo = new ProcessStartInfo("cmd")
            {
                Arguments = $"/c npx nx {arguments}",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };
            try
            {
                process.Start();
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();
                process.WaitForExit();
                Task.WaitAll(outputTask, errorTask);
                return (process.ExitCode, outputTask.Result);
            }
            catch (Exception exception) when (exception is InvalidOperationException or System.ComponentModel.Win32Exception)
            {
                return (1, string.Empty);
            }
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green]Angular Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Use this wizard to scaffold Angular pages, components, and services with the existing project conventions.[/]");
            AnsiConsole.WriteLine();
        }

        private static void ShowSummary(string action, NgxCommand command)
        {
            var details = new List<string>
            {
                $"[grey]Action:[/] [green]{action}[/]",
                $"[grey]Name:[/] [green]{command.Name}[/]"
            };

            if (command.Nx)
            {
                details.Add($"[grey]Nx project:[/] [green]{command.Project}[/]");
                details.Add($"[grey]Folder:[/] [green]{command.Folder}[/]");
                if (action != CreateServiceAction)
                {
                    details.Add($"[grey]Module:[/] [green]{command.Module}[/]");
                }
            }

            var summary = string.Join(Environment.NewLine, details);

            AnsiConsole.Write(new Panel(summary)
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private static bool ConfirmExecution()
        {
            return AnsiConsole.Confirm("Run this [green]command[/] now?", true);
        }

        private int RunSelectedAction(string action, NgxCommand command)
        {
            ShowSummary(action, command);

            if (!ConfirmExecution())
            {
                return 0;
            }

            return action switch
            {
                CreatePageAction => _ngxPageService.RunProcess(ProcessToRun, command),
                CreateComponentAction => _ngxComponentService.RunProcess(ProcessToRun, command),
                CreateServiceAction => _ngxServiceService.RunProcess(ProcessToRun, command),
                _ => 1
            };
        }
    }
}
