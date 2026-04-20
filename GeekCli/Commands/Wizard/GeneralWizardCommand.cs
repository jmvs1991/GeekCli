using GeekCli.Commands.Db.Wizard;
using GeekCli.Commands.Dotnet.Wizard;
using GeekCli.Commands.Ngx.Wizard;
using GeekCli.Commands.Rx.Wizard;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Wizard
{
    internal sealed class GeneralWizardCommand : Command
    {
        private const string DatabaseOption = "Database";
        private const string DotnetOption = ".NET Templates";
        private const string AngularOption = "Angular";
        private const string ReactOption = "React / React Native";

        private readonly IDbWizard _dbWizardCommand;
        private readonly IDotnetWizard _dotnetWizardCommand;
        private readonly INgxWizard _ngxWizardCommand;
        private readonly IRxWizard _rxWizardCommand;

        public GeneralWizardCommand(IDbWizard dbWizardCommand,
                                    IDotnetWizard dotnetWizardCommand,
                                    INgxWizard ngxWizardCommand,
                                    IRxWizard rxWizardCommand)
        {
            _dbWizardCommand = dbWizardCommand;
            _dotnetWizardCommand = dotnetWizardCommand;
            _ngxWizardCommand = ngxWizardCommand;
            _rxWizardCommand = rxWizardCommand;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            AnsiConsole.Write(new Rule("[green]Geek CLI Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Choose an area and the wizard will guide you through the next steps.[/]");
            AnsiConsole.WriteLine();

            var area = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to work on?")
                    .PageSize(10)
                    .AddChoices(DatabaseOption, DotnetOption, AngularOption, ReactOption));

            return area switch
            {
                DatabaseOption => _dbWizardCommand.RunWizard(),
                DotnetOption => _dotnetWizardCommand.RunWizard(),
                AngularOption => _ngxWizardCommand.RunWizard(),
                ReactOption => _rxWizardCommand.RunWizard(),
                _ => 1
            };
        }
    }
}
