using GeekCliServices.Services.Db.Migrations.Remove.Models;

namespace GeekCliServices.Services.Db.Migrations.Remove
{
    public interface IRemoveMigrationService : ICommandService<RemoveMigrationCommand>
    {
    }
}
