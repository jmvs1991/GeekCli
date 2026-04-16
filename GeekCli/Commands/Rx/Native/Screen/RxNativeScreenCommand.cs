using GeekCliServices.Services.Rx.Native.Screen;
using GeekCliServices.Services.Rx.Native.Screen.Models;

namespace GeekCli.Commands.Rx.Native.Screen
{
    class RxNativeScreenCommand : RxCommandBase<RxNativeScreenSettings, IRxNativeScreenService, RxScreenCommand>
    {
        public RxNativeScreenCommand(IRxNativeScreenService service) : base(service)
        {
        }

        protected override RxScreenCommand MapToCommand(RxNativeScreenSettings settings)
        {
            return new RxScreenCommand(settings.Name, settings.Flat, settings.Schema, settings.Wrapper);
        }
    }
}
