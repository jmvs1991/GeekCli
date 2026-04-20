using GeekCliServices.Services.Db.Scaffold;
using GeekCliServices.Services.Db.Scaffold.Models;

namespace GeekCli.Commands.Db.Scaffold
{
    internal sealed class DbScaffoldCommand : CommandBase<DbScaffoldSettings, IDbScaffoldService, DbScaffoldDotnetCommand>
    {
        public DbScaffoldCommand(IDbScaffoldService service) : base(service, "dotnet")
        {
        }

        protected override DbScaffoldDotnetCommand MapToCommand(DbScaffoldSettings settings)
        {
            return new DbScaffoldDotnetCommand(
                settings.Table!,
                settings.OutputDir!,
                settings.ConnectionString!,
                settings.Provider!);
        }
    }
}
