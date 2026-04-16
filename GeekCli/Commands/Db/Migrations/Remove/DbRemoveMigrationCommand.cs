using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Remove.Models;

namespace GeekCli.Commands.Db.Migrations.Remove
{
    class DbRemoveMigrationCommand : DbCommandBase<DbRemoveMigrationSettings, IRemoveMigrationService, RemoveMigrationCommand>
    {
        public DbRemoveMigrationCommand(IRemoveMigrationService service) : base(service)
        {
        }

        protected override RemoveMigrationCommand MapToCommand(DbRemoveMigrationSettings settings)
        {
            return new RemoveMigrationCommand(settings.ProjectName, settings.Init);
        }
    }
}
