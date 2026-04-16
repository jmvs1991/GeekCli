using GeekCliServices.Services.Db.Migrations.Add.Models;

namespace GeekCliServices.Services.Db.Migrations.Add
{
    public class AddMigrationService : ExternalProcessServiceBase<AddMigrationCommand>, IAddMigrationService
    {

        protected override string BuildArgs(AddMigrationCommand command)
        {
            string project = command.Init ? $"{command.ProjectName}.SchemaInitialization" : $"{command.ProjectName}.SchemaUpdates";
            string issue = command.Issue;
            string migration = command.MigrationName;
            string manager = $"{command.ProjectName}.Manager";


            return $"ef migrations add {migration} " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-o Migrations/{issue} -v -- --Assembly:{project}";
        }
    }
}
