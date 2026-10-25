using GeekCli.Commands.Db.Wizard;
using GeekCli.Commands.Ai.Wizard;
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
        private const string AiOption = "AI skills and agents";
        private const string ExitOption = "Exit";

        private readonly IDbWizard _dbWizardCommand;
        private readonly IDotnetWizard _dotnetWizardCommand;
        private readonly INgxWizard _ngxWizardCommand;
        private readonly IRxWizard _rxWizardCommand;
        private readonly IAiWizard _aiWizardCommand;

        public GeneralWizardCommand(IDbWizard dbWizardCommand,
                                    IDotnetWizard dotnetWizardCommand,
                                    INgxWizard ngxWizardCommand,
                                    IRxWizard rxWizardCommand,
                                    IAiWizard aiWizardCommand)
        {
            _dbWizardCommand = dbWizardCommand;
            _dotnetWizardCommand = dotnetWizardCommand;
            _ngxWizardCommand = ngxWizardCommand;
            _rxWizardCommand = rxWizardCommand;
            _aiWizardCommand = aiWizardCommand;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            AnsiConsole.Write(new Rule("[green]Geek CLI Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Choose an area and move back between sections without restarting the CLI.[/]");

            while (true)
            {
                AnsiConsole.WriteLine();

                var area = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What do you want to work on?")
                        .PageSize(10)
                        .AddChoices(DatabaseOption, DotnetOption, AngularOption, ReactOption, AiOption, ExitOption));

                switch (area)
                {
                    case DatabaseOption:
                        _dbWizardCommand.RunWizard(showBackOption: true);
                        break;
                    case DotnetOption:
                        _dotnetWizardCommand.RunWizard(showBackOption: true);
                        break;
                    case AngularOption:
                        _ngxWizardCommand.RunWizard(showBackOption: true);
                        break;
                    case ReactOption:
                        _rxWizardCommand.RunWizard(showBackOption: true);
                        break;
                    case AiOption:
                        _aiWizardCommand.RunWizard(showBackOption: true);
                        break;
                    case ExitOption:
                        return 0;
                    default:
                        return 1;
                }
            }
        }
    }
}
