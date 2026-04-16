using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Add.Models;

namespace GeekCli.Commands.Db.Migrations.Add
{
    class DbAddMigrationCommand : DbCommandBase<DbAddMigrationSettings, IAddMigrationService, AddMigrationCommand>
    {
        public DbAddMigrationCommand(IAddMigrationService service) : base(service)
        {
        }

        protected override AddMigrationCommand MapToCommand(DbAddMigrationSettings settings)
        {
            return new AddMigrationCommand(settings.ProjectName, settings.Init, settings.MigrationName, settings.Issue);
        }
    }
}
