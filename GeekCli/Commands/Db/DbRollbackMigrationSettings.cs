using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db
{
    internal class DbRollbackMigrationSettings : DbSettingBase
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the EF Core migration to create.")]
        public string MigrationName { get; set; }
    }
}
