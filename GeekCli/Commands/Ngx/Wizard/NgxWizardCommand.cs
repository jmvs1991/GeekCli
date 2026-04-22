using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Page;
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
            var command = new NgxCommand(name);
            return RunSelectedAction(action, command);
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green]Angular Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Use this wizard to scaffold Angular pages and components with the existing project conventions.[/]");
            AnsiConsole.WriteLine();
        }

        private static void ShowSummary(string action, string name)
        {
            var summary = string.Join(Environment.NewLine, new[]
            {
                $"[grey]Action:[/] [green]{action}[/]",
                $"[grey]Name:[/] [green]{name}[/]"
            });

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
            ShowSummary(action, command.Name);

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
