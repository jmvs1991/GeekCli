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
            string projectPath = Path.Combine(".", project, $"{project}.csproj");
            string managerPath = Path.Combine(".", manager, $"{manager}.csproj");

            return $"ef database update {migration} " +
                   $"--project \"{projectPath}\" " +
                   $"--startup-project \"{managerPath}\" " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
