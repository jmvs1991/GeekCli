namespace GeekCliServices.Services.Db.Scaffold.Models
{
    public sealed record DbScaffoldDotnetCommand(
        string Table,
        string OutputDir,
        string ConnectionString,
        string Provider);
}
