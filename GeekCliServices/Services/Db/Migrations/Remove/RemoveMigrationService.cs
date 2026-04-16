using GeekCliServices.Services.Db.Migrations.Remove.Models;

namespace GeekCliServices.Services.Db.Migrations.Remove
{
    public class RemoveMigrationService : ExternalProcessServiceBase<RemoveMigrationCommand>, IRemoveMigrationService
    {
        protected override string BuildArgs(RemoveMigrationCommand command)
        {
            string project = command.Init ? $"{command.ProjectName}.SchemaInitialization" : $"{command.ProjectName}.SchemaUpdates";
            string manager = $"{command.ProjectName}.Manager";


            return $"ef migrations remove " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
