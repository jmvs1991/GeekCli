namespace GeekCli.Commands.Db
{
    class DbRemoveMigrationCommand : DbCommandBase<DbRemoveMigrationSettings>
    {
        protected override string BuildArgs(DbRemoveMigrationSettings settings)
        {
            string project = settings.Init ? $"{settings.ProjectName}.SchemaInitialization" : $"{settings.ProjectName}.SchemaUpdates";
            string manager = $"{settings.ProjectName}.Manager";


            return $"ef migrations remove " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
