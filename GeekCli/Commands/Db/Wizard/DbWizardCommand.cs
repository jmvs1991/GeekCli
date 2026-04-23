using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Add.Models;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Remove.Models;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Migrations.Rollback.Models;
using GeekCliServices.Services.Db.Scaffold;
using GeekCliServices.Services.Db.Scaffold.Models;
using GeekCliServices.Services.Db.Scripts;
using GeekCliServices.Services.Db.Scripts.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Db.Wizard
{
    internal sealed class DbWizardCommand : Command, IDbWizard
    {
        private const string ProcessToRun = "dotnet";
        private const string BackAction = "Back";
        private const string CreateMigrationAction = "Create a new migration";
        private const string RemoveMigrationAction = "Remove the last migration";
        private const string RollbackMigrationAction = "Rollback to a specific migration";
        private const string ScaffoldTableAction = "Scaffold a table";
        private const string GenerateSqlScriptsAction = "Generate SQL migration scripts";

        private readonly IAddMigrationService _addMigrationService;
        private readonly IRemoveMigrationService _removeMigrationService;
        private readonly IRollbackMigrationService _rollbackMigrationService;
        private readonly IDbScaffoldService _dbScaffoldService;
        private readonly IDbScriptService _dbScriptService;

        public DbWizardCommand(IAddMigrationService addMigrationService,
                               IRemoveMigrationService removeMigrationService,
                               IRollbackMigrationService rollbackMigrationService,
                               IDbScaffoldService dbScaffoldService,
                               IDbScriptService dbScriptService)
        {
            _addMigrationService = addMigrationService;
            _removeMigrationService = removeMigrationService;
            _rollbackMigrationService = rollbackMigrationService;
            _dbScaffoldService = dbScaffoldService;
            _dbScriptService = dbScriptService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            ShowHeader();

            var action = AskAction(showBackOption);

            if (showBackOption && action == BackAction)
            {
                return 0;
            }

            return action switch
            {
                CreateMigrationAction => RunCreateMigration(),
                RemoveMigrationAction => RunRemoveMigration(),
                RollbackMigrationAction => RunRollbackMigration(),
                ScaffoldTableAction => RunScaffoldTable(),
                GenerateSqlScriptsAction => RunGenerateSqlScripts(),
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

        private static string AskAction(bool showBackOption)
        {
            var choices = new List<string>
            {
                CreateMigrationAction,
                RemoveMigrationAction,
                RollbackMigrationAction,
                ScaffoldTableAction,
                GenerateSqlScriptsAction
            };

            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the database task you want to run")
                    .PageSize(10)
                    .AddChoices(choices));
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

        private static void ShowScriptSummary(string projectName, bool init, string schema, DbScriptType type, string issue, string? objectName)
        {
            var summary = new List<string>
            {
                $"[grey]Action:[/] [green]{GenerateSqlScriptsAction}[/]",
                $"[grey]Project:[/] [green]{projectName}[/]",
                $"[grey]Schema project:[/] [green]{(init ? "Initialization" : "Updates")}[/]",
                $"[grey]Schema:[/] [green]{schema}[/]",
                $"[grey]Type:[/] [green]{DbScriptTypeParser.ToDisplayName(type)}[/]",
                $"[grey]Issue:[/] [green]{issue}[/]"
            };

            if (!string.IsNullOrWhiteSpace(objectName))
            {
                summary.Add($"[grey]Object:[/] [green]{objectName}[/]");
            }

            ShowSummary(summary);
        }

        private static void ShowSummary(List<string> summaryLines)
        {
            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summaryLines))
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private static bool ConfirmExecution()
        {
            return AnsiConsole.Confirm("Run this [green]command[/] now?", true);
        }

        private static string AskSchema()
        {
            return AnsiConsole.Ask<string>("Database [green]schema[/] ([grey]example: Sales[/])?");
        }

        private static string AskIssue()
        {
            return AnsiConsole.Ask<string>("Issue or ticket ([grey]example: ABC-123[/])?");
        }

        private static DbScriptType AskScriptType()
        {
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the SQL migration [green]type[/]")
                    .PageSize(10)
                    .AddChoices("Query",
                                "Modify SP",
                                "Create SP",
                                "Modify Table",
                                "Create Table",
                                "Create View",
                                "Modify View"));

            DbScriptTypeParser.TryParse(selected, out var type);
            return type;
        }

        private static string AskObjectName(DbScriptType type)
        {
            string example = type switch
            {
                DbScriptType.ModifyStoredProcedure or DbScriptType.CreateStoredProcedure => "usp_GetCustomer",
                DbScriptType.ModifyTable or DbScriptType.CreateTable => "TR_CUSTOMER",
                DbScriptType.CreateView or DbScriptType.ModifyView => "VW_CUSTOMER",
                _ => "ObjectName"
            };

            return AnsiConsole.Ask<string>($"Database [green]object name[/] ([grey]example: {example}[/])?");
        }

        private int RunCreateMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();
            var migrationName = AnsiConsole.Ask<string>("Migration [green]name[/] ([grey]example: AddUserAuditTable[/])?");
            var issue = AnsiConsole.Ask<string>("Issue or ticket ([grey]optional, example: ABC-123[/])?", string.Empty);

            ShowMigrationSummary(CreateMigrationAction, projectName, init, migrationName, issue);

            if (!ConfirmExecution())
            {
                return 0;
            }

            var command = new AddMigrationCommand(projectName, init, migrationName, issue);
            return _addMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRemoveMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();

            ShowMigrationSummary(RemoveMigrationAction, projectName, init);

            if (!ConfirmExecution())
            {
                return 0;
            }

            var command = new RemoveMigrationCommand(projectName, init);
            return _removeMigrationService.RunProcess(ProcessToRun, command);
        }

        private int RunRollbackMigration()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();
            var migrationName = AnsiConsole.Ask<string>("Target [green]migration[/] name ([grey]example: AddUserAuditTable[/])?");

            ShowMigrationSummary(RollbackMigrationAction, projectName, init, migrationName);

            if (!ConfirmExecution())
            {
                return 0;
            }

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

            if (!ConfirmExecution())
            {
                return 0;
            }

            var command = new DbScaffoldDotnetCommand(table, outputDir, connectionString, provider);
            return _dbScaffoldService.RunProcess(ProcessToRun, command);
        }

        private int RunGenerateSqlScripts()
        {
            var projectName = AskProjectName();
            var init = AskInitFlag();
            var schema = AskSchema();
            var type = AskScriptType();
            var issue = AskIssue();
            var objectName = DbScriptRules.RequiresObjectName(type) ? AskObjectName(type) : null;

            ShowScriptSummary(projectName, init, schema, type, issue, objectName);

            if (!ConfirmExecution())
            {
                return 0;
            }

            var command = new DbScriptCommand(projectName, init, schema, type, issue, objectName);
            return _dbScriptService.RunProcess(string.Empty, command);
        }
    }
}
