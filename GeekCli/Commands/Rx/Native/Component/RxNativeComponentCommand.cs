using GeekCliServices.Services.Rx.Models;
using GeekCliServices.Services.Rx.Native.Component;

namespace GeekCli.Commands.Rx.Native.Component
{
    class RxNativeComponentCommand : RxCommandBase<RxNativeComponentSettings, IRxNativeComponentService, RxCommand>
    {
        public RxNativeComponentCommand(IRxNativeComponentService service) : base(service)
        {
        }

        protected override RxCommand MapToCommand(RxNativeComponentSettings settings)
        {
            return new RxCommand(settings.Name, settings.Flat);
        }
    }
}
