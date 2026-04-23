using GeekCliServices.Services.Db.Scripts;
using GeekCliServices.Services.Db.Scripts.Models;

namespace GeekCli.Commands.Db.Scripts
{
    internal sealed class DbGenerateScriptCommand : CommandBase<DbScriptSettings, IDbScriptService, DbScriptCommand>
    {
        public DbGenerateScriptCommand(IDbScriptService service) : base(service, string.Empty)
        {
        }

        protected override DbScriptCommand MapToCommand(DbScriptSettings settings)
        {
            DbScriptTypeParser.TryParse(settings.Type, out var type);

            return new DbScriptCommand(settings.ProjectName,
                                       settings.Init,
                                       settings.Schema!,
                                       type,
                                       settings.Issue!,
                                       settings.ObjectName);
        }
    }
}
