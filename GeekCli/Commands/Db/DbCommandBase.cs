using GeekCliServices.Services;
using GeekCliServices.Services.Db.Models;

namespace GeekCli.Commands.Db
{
    abstract class DbCommandBase<TSettings, TService, TCommand> : CommandBase<TSettings, TService, TCommand> where TSettings : DbSettingBase
                                                                                                             where TService : ICommandService<TCommand>
                                                                                                             where TCommand : DbCommandBase
    {
        public DbCommandBase(TService service) : base(service, "dotnet")
        {
        }
    }
}
