namespace GeekCli.Commands.Db
{
    class DbRollbackMigrationCommand : DbCommandBase<DbRollbackMigrationSettings>
    {
        protected override string BuildArgs(DbRollbackMigrationSettings settings)
        {
            string project = settings.Init ? $"{settings.ProjectName}.SchemaInitialization" : $"{settings.ProjectName}.SchemaUpdates";
            string migration = settings.MigrationName;
            string manager = $"{settings.ProjectName}.Manager";

            return $"ef database update {migration} " +
                   $"--project .\\{project}\\{project}.csproj " +
                   $"--startup-project .\\{manager}\\{manager}.csproj " +
                   $"-v -- --Assembly:{project}";
        }
    }
}
