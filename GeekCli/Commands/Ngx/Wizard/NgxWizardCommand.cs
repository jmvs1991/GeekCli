using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Page;
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

        private readonly INgxPageService _ngxPageService;
        private readonly INgxComponentService _ngxComponentService;

        public NgxWizardCommand(INgxPageService ngxPageService, INgxComponentService ngxComponentService)
        {
            _ngxPageService = ngxPageService;
            _ngxComponentService = ngxComponentService;
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
                CreateComponentAction
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
            var command = action == CreateComponentAction
                ? BuildComponentCommand(name)
                : new NgxCommand(name);

            if (command is null)
            {
                return 1;
            }

            return RunSelectedAction(action, command);
        }

        private static NgxCommand? BuildComponentCommand(string name)
        {
            if (!AnsiConsole.Confirm("Is this an [green]Nx workspace[/]?", false))
            {
                return new NgxCommand(name);
            }

            var projects = GetAngularProjects();
            if (projects.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No Angular projects were found in the Nx workspace, or Nx could not read the project list.[/]");
                return null;
            }

            var project = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the Angular [green]project[/] for the component")
                    .PageSize(10)
                    .AddChoices(projects));
            var folder = AnsiConsole.Ask<string>("Destination [green]folder[/] ([grey]relative to src/app, or apps/<project>/src/app/...[/])?");
            var module = AnsiConsole.Ask<string>("Declaring Angular [green]module[/] path?");

            return new NgxCommand(name, true, project, folder, module);
        }

        private static List<string> GetAngularProjects()
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
                    .Where(IsAngularProject)
                    .OrderBy(project => project, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (JsonException)
            {
                return new List<string>();
            }
        }

        private static bool IsAngularProject(string project)
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
                var executor = projectConfiguration?.Targets?.GetValueOrDefault("build")?.Executor;
                return executor?.Contains("angular", StringComparison.OrdinalIgnoreCase) == true
                    || executor?.Contains("@angular-devkit/build", StringComparison.OrdinalIgnoreCase) == true
                    || executor?.Contains("@angular/build", StringComparison.OrdinalIgnoreCase) == true;
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
            AnsiConsole.MarkupLine("[grey]Use this wizard to scaffold Angular pages and components with the existing project conventions.[/]");
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
                details.Add($"[grey]Module:[/] [green]{command.Module}[/]");
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
                _ => 1
            };
        }
    }
}
