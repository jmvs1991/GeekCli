using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Add.Models;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Remove.Models;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Migrations.Rollback.Models;
using GeekCliServices.Services.Db.Scaffold;
using GeekCliServices.Services.Db.Scaffold.Models;
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
        private const string ScaffoldTableAction = "Scaffold a table";

        private readonly IAddMigrationService _addMigrationService;
        private readonly IRemoveMigrationService _removeMigrationService;
        private readonly IRollbackMigrationService _rollbackMigrationService;
        private readonly IDbScaffoldService _dbScaffoldService;

        public DbWizardCommand(IAddMigrationService addMigrationService,
                               IRemoveMigrationService removeMigrationService,
                               IRollbackMigrationService rollbackMigrationService,
                               IDbScaffoldService dbScaffoldService)
        {
            _addMigrationService = addMigrationService;
            _removeMigrationService = removeMigrationService;
            _rollbackMigrationService = rollbackMigrationService;
            _dbScaffoldService = dbScaffoldService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard()
        {
            ShowHeader();

            var action = AskAction();

            return action switch
            {
                CreateMigrationAction => RunCreateMigration(),
                RemoveMigrationAction => RunRemoveMigration(),
                RollbackMigrationAction => RunRollbackMigration(),
                ScaffoldTableAction => RunScaffoldTable(),
                _ => 1
            };
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green]Database Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]This wizard will guide you through common EF Core migration and scaffold tasks.[/]");
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
                    .AddChoices(CreateMigrationAction, RemoveMigrationAction, RollbackMigrationAction, ScaffoldTableAction));
        }

        private static void ShowMigrationSummary(string action, string projectName, bool init, string? migrationName = null, string? issue = null)
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

            ShowSummary(summary);
        }

        private static void ShowScaffoldSummary(string table, string outputDir, string provider)
        {
            var summary = new List<string>
            {
                $"[grey]Action:[/] [green]{ScaffoldTableAction}[/]",
                $"[grey]Table:[/] [green]{table}[/]",
                $"[grey]Output:[/] [green]{outputDir}[/]",
                $"[grey]Provider:[/] [green]{provider}[/]"
            };

            ShowSummary(summary);
        }

        private static void ShowSummary(List<string> summaryLines)
        {
            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summaryLines))
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private int RunCreateMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();
            var migrationName = AnsiConsole.Ask<string>("Migration [green]name[/] ([grey]example: AddUserAuditTable[/])?");
            var issue = AnsiConsole.Ask<string>("Issue or ticket ([grey]optional, example: ABC-123[/])?", string.Empty);

            ShowMigrationSummary(CreateMigrationAction, projectName, init, migrationName, issue);

            var command = new AddMigrationCommand(projectName, init, migrationName, issue);
            return _addMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRemoveMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();

            ShowMigrationSummary(RemoveMigrationAction, projectName, init);

            var command = new RemoveMigrationCommand(projectName, init);
            return _removeMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRollbackMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();
            var migrationName = AnsiConsole.Ask<string>("Target [green]migration[/] name ([grey]example: AddUserAuditTable[/])?");

            ShowMigrationSummary(RollbackMigrationAction, projectName, init, migrationName);

            var command = new RollbackMigrationCommand(projectName, init, migrationName);
            return _rollbackMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunScaffoldTable()
        {
            var table = AnsiConsole.Ask<string>("Database [green]table[/] name ([grey]example: TR_TAG_INVOICE[/])?");
            var outputDir = AnsiConsole.Ask<string>("Entity [green]output directory[/] ([grey]example: Parking/Entities[/])?");
            var connectionString = AnsiConsole.Prompt(
                new TextPrompt<string>("Database [green]connection string[/]?")
                    .PromptStyle("green")
                    .Secret());
            var provider = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the EF Core provider")
                    .AddChoices("SqlServer", "Postgres"));

            ShowScaffoldSummary(table, outputDir, provider);

            var command = new DbScaffoldDotnetCommand(table, outputDir, connectionString, provider);
            return _dbScaffoldService.RunProcess(ProcessToRun, command);
        }
    }
}
