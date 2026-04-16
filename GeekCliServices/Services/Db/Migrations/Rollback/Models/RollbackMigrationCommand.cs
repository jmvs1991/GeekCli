using GeekCliServices.Services.Db.Models;

namespace GeekCliServices.Services.Db.Migrations.Rollback.Models
{
    public sealed record RollbackMigrationCommand : DbCommandBase
    {
        public string MigrationName { get; init; }

        public RollbackMigrationCommand(string projectName, bool init, string migrationName) : base(projectName, init)
        {
            MigrationName = migrationName;
        }
    }
}
