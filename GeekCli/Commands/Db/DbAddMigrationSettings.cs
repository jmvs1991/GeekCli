using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db
{
    class DbAddMigrationSettings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the EF Core migration to create.")]
        public string MigrationName { get; set; }

        [CommandOption("--project")]
        [Description("The base name of the project (e.g., 'Booking'). Used to resolve csproj paths.")]
        public string ProjectName { get; set; }

        [CommandOption("--issue")]
        [Description("An optional issue or ticket number to attach to the migration name for traceability.")]
        public string Issue { get; set; }

        [CommandOption("--init")]
        [Description("Indicates whether this is an initialization migration. If true, output will go to 'Migrations/Init'.")]
        [DefaultValue(false)]
        public bool Init { get; set; }
    }
}
