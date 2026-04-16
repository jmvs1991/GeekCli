using GeekCliServices.Services.Rx.Models;
using GeekCliServices.Services.Rx.Native.Module;

namespace GeekCli.Commands.Rx.Native.Module
{
    class RxNativeModuleCommand : RxCommandBase<RxNativeModuleSettings, IRxNativeModuleService, RxCommand>
    {
        public RxNativeModuleCommand(IRxNativeModuleService service) : base(service)
        {
        }

        protected override RxCommand MapToCommand(RxNativeModuleSettings settings)
        {
            return new RxCommand(settings.Name, settings.Flat);
        }
    }
}
