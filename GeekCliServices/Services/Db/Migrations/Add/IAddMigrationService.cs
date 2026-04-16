using GeekCliServices.Services.Db.Migrations.Add.Models;

namespace GeekCliServices.Services.Db.Migrations.Add
{
    public interface IAddMigrationService : ICommandService<AddMigrationCommand>
    {
    }
}
