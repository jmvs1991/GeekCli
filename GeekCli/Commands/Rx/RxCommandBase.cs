using GeekCliServices.Services;
using GeekCliServices.Services.Rx.Models;

namespace GeekCli.Commands.Rx
{
    abstract class RxCommandBase<TSettings, TService, TCommand> : CommandBase<TSettings, TService, TCommand> where TSettings : RxSettingBase
        where TService : ICommandService<TCommand>
                                                                                                             where TCommand : RxCommand
    {
        protected RxCommandBase(TService service) : base(service, string.Empty)
        {
        }
    }
}
