using GeekCliServices.Services.Db.Migrations.Remove.Models;

namespace GeekCliServices.Services.Db.Migrations.Remove
{
    public class RemoveMigrationService : ExternalProcessServiceBase<RemoveMigrationCommand>, IRemoveMigrationService
    {
        protected override string BuildArgs(RemoveMigrationCommand command)
        {
            string project = command.Init ? $"{command.ProjectName}.SchemaInitialization" : $"{command.ProjectName}.SchemaUpdates";
            string manager = $"{command.ProjectName}.Manager";
            string projectPath = Path.Combine(".", project, $"{project}.csproj");
            string managerPath = Path.Combine(".", manager, $"{manager}.csproj");


            return $"ef migrations remove " +
                   $"--project \"{projectPath}\" " +
                   $"--startup-project \"{managerPath}\" " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
