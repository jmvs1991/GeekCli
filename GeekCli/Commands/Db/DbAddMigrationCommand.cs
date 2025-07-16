namespace GeekCli.Commands.Db
{
    class DbAddMigrationCommand : DbCommandBase<DbAddMigrationSettings>
    {
        protected override string BuildArgs(DbAddMigrationSettings settings)
        {
            string project = settings.Init ? $"{settings.ProjectName}.SchemaInitialization" : $"{settings.ProjectName}.SchemaUpdates";
            string issue = settings.Issue;
            string migration = settings.MigrationName;
            string manager = $"{settings.ProjectName}.Manager";


            return $"ef migrations add {migration} " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-o Migrations/{issue} -v -- --Assembly:{project}";
        }
               
    }
}
