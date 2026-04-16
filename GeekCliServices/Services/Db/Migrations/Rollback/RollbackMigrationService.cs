using GeekCliServices.Services.Db.Migrations.Rollback.Models;

namespace GeekCliServices.Services.Db.Migrations.Rollback
{
    public class RollbackMigrationService : ExternalProcessServiceBase<RollbackMigrationCommand>, IRollbackMigrationService
    {
        protected override string BuildArgs(RollbackMigrationCommand command)
        {
            string project = command.Init ? $"{command.ProjectName}.SchemaInitialization" : $"{command.ProjectName}.SchemaUpdates";
            string migration = command.MigrationName;
            string manager = $"{command.ProjectName}.Manager";

            return $"ef database update {migration} " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
