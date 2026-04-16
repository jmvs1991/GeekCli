using GeekCliServices.Services.Rx.Common.Context;
using GeekCliServices.Services.Rx.Models;

namespace GeekCli.Commands.Rx.Common.Context
{
    class RxContextCommand : RxCommandBase<RxContextSettings, IRxContextService, RxCommand>
    {
        public RxContextCommand(IRxContextService service) : base(service)
        {
        }

        protected override RxCommand MapToCommand(RxContextSettings settings)
        {
            return new RxCommand(settings.Name, settings.Flat);
        }
    }
}
