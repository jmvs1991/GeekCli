using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db
{
    class DbAddMigrationSettings : DbSettingBase
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the EF Core migration to create.")]
        public string MigrationName { get; set; }

        [CommandOption("--issue")]
        [Description("An optional issue or ticket number to attach to the migration name for traceability.")]
        public string Issue { get; set; }
    }
}
