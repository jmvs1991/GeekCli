using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Add.Models;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Remove.Models;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Migrations.Rollback.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Db.Wizard
{
    internal sealed class DbWizardCommand : Command, IDbWizard
    {
        private const string ProcessToRun = "dotnet";
        private const string CreateMigrationAction = "Create a new migration";
        private const string RemoveMigrationAction = "Remove the last migration";
        private const string RollbackMigrationAction = "Rollback to a specific migration";

        private readonly IAddMigrationService _addMigrationService;
        private readonly IRemoveMigrationService _removeMigrationService;
        private readonly IRollbackMigrationService _rollbackMigrationService;

        public DbWizardCommand(IAddMigrationService addMigrationService,
                               IRemoveMigrationService removeMigrationService,
                               IRollbackMigrationService rollbackMigrationService)
        {
            _addMigrationService = addMigrationService;
            _removeMigrationService = removeMigrationService;
            _rollbackMigrationService = rollbackMigrationService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard()
        {
            ShowHeader();

            var projectName = AskProjectName();
            var init = AskInitFlag();
            var action = AskAction();

            return action switch
            {
                CreateMigrationAction => RunCreateMigration(projectName, init),
                RemoveMigrationAction => RunRemoveMigration(projectName, init),
                RollbackMigrationAction => RunRollbackMigration(projectName, init),
                _ => 1
            };
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green]Database Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]This wizard will guide you through the most common EF Core migration tasks.[/]");
            AnsiConsole.WriteLine();
        }

        private static string AskProjectName()
        {
            return AnsiConsole.Ask<string>("Base [green]project[/] name ([grey]example: Booking[/])?");
        }

        private static bool AskInitFlag()
        {
            return AnsiConsole.Confirm("Should this use the [green]init[/] schema project?", false);
        }

        private static string AskAction()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the database task you want to run")
                    .PageSize(10)
                    .AddChoices(CreateMigrationAction, RemoveMigrationAction, RollbackMigrationAction));
        }

        private static void ShowSummary(string action, string projectName, bool init, string? migrationName = null, string? issue = null)
        {
            var summary = new List<string>
            {
                $"[grey]Action:[/] [green]{action}[/]",
                $"[grey]Project:[/] [green]{projectName}[/]",
                $"[grey]Schema project:[/] [green]{(init ? "Initialization" : "Updates")}[/]"
            };

            if (!string.IsNullOrWhiteSpace(migrationName))
            {
                summary.Add($"[grey]Migration:[/] [green]{migrationName}[/]");
            }

            if (!string.IsNullOrWhiteSpace(issue))
            {
                summary.Add($"[grey]Issue:[/] [green]{issue}[/]");
            }

            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summary))
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private int RunCreateMigration(string projectName, bool init)
        {
            var migrationName = AnsiConsole.Ask<string>("Migration [green]name[/] ([grey]example: AddUserAuditTable[/])?");
            var issue = AnsiConsole.Ask<string>("Issue or ticket ([grey]optional, example: ABC-123[/])?", string.Empty);

            ShowSummary(CreateMigrationAction, projectName, init, migrationName, issue);

            var command = new AddMigrationCommand(projectName, init, migrationName, issue);
            return _addMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRemoveMigration(string projectName, bool init)
        {
            ShowSummary(RemoveMigrationAction, projectName, init);

            var command = new RemoveMigrationCommand(projectName, init);
            return _removeMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRollbackMigration(string projectName, bool init)
        {
            var migrationName = AnsiConsole.Ask<string>("Target [green]migration[/] name ([grey]example: AddUserAuditTable[/])?");

            ShowSummary(RollbackMigrationAction, projectName, init, migrationName);

            var command = new RollbackMigrationCommand(projectName, init, migrationName);
            return _rollbackMigrationService.RunProcess(ProcessToRun, command);
        }
    }
}
