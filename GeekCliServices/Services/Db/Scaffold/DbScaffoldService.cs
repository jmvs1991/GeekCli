using GeekCliServices.Services.Db.Scaffold.Models;

namespace GeekCliServices.Services.Db.Scaffold
{
    public sealed class DbScaffoldService : ExternalProcessServiceBase<DbScaffoldDotnetCommand>, IDbScaffoldService
    {
        protected override string BuildArgs(DbScaffoldDotnetCommand command)
        {
            string provider = ResolveProvider(command.Provider);

            var args = $"ef dbcontext scaffold " +
                       $"\"{EscapeArgument(command.ConnectionString)}\" " +
                       $"{provider} " +
                       $"--table \"{EscapeArgument(command.Table)}\" " +
                       $"--data-annotations " +
                       $"--output-dir \"{EscapeArgument(command.OutputDir)}\"";

            if (!string.IsNullOrWhiteSpace(command.StartupProject))
            {
                args += $" --startup-project \"{EscapeArgument(command.StartupProject)}\"";
            }

            if (!string.IsNullOrWhiteSpace(command.Project))
            {
                args += $" --project \"{EscapeArgument(command.Project)}\"";
            }

            return args;
        }

        private static string ResolveProvider(string provider)
        {
            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                return "Microsoft.EntityFrameworkCore.SqlServer";
            }

            if (provider.Equals("Postgres", StringComparison.OrdinalIgnoreCase))
            {
                return "Npgsql.EntityFrameworkCore.PostgreSQL";
            }

            throw new ArgumentOutOfRangeException(nameof(provider), provider, "Unsupported provider.");
        }

        private static string EscapeArgument(string value)
        {
            return value.Replace("\"", "\\\"");
        }
    }
}
