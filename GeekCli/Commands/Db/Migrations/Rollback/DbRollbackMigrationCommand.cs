using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Migrations.Rollback.Models;

namespace GeekCli.Commands.Db.Migrations.Rollback
{
    class DbRollbackMigrationCommand : DbCommandBase<DbRollbackMigrationSettings, IRollbackMigrationService, RollbackMigrationCommand>
    {
        public DbRollbackMigrationCommand(IRollbackMigrationService service) : base(service)
        {
        }

        protected override RollbackMigrationCommand MapToCommand(DbRollbackMigrationSettings settings)
        {
            return new RollbackMigrationCommand(settings.ProjectName, settings.Init, settings.MigrationName);
        }
    }
}
