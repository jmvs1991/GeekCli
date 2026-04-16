using GeekCliServices.Services.Db.Migrations.Rollback.Models;

namespace GeekCliServices.Services.Db.Migrations.Rollback
{
    public interface IRollbackMigrationService : ICommandService<RollbackMigrationCommand>
    {
    }
}
