using GeekCliServices.Services.Db.Models;

namespace GeekCliServices.Services.Db.Migrations.Add.Models
{
    public sealed record AddMigrationCommand : DbCommandBase
    {
        public string MigrationName { get; init; }

        public string Issue { get; init; }

        public AddMigrationCommand(string projectName, bool init, string migrationName, string issue) : base(projectName, init)
        {
            MigrationName = migrationName;
            Issue = issue;
        }
    }
}
