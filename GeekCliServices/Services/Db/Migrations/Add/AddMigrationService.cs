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
            string projectPath = Path.Combine(".", project, $"{project}.csproj");
            string managerPath = Path.Combine(".", manager, $"{manager}.csproj");


            return $"ef migrations add {migration} " +
                   $"--project \"{projectPath}\" " +
                   $"--startup-project \"{managerPath}\" " +
                   $"-o Migrations/{issue} -v -- --Assembly:{project}";
        }
    }
}
