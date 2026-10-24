using GeekCliServices.Services;
using GeekCliServices.Services.Ngx.Models;

namespace GeekCli.Commands.Ngx
{
    abstract class NgxCommandBase<TSettings, TService> : CommandBase<TSettings, TService, NgxCommand>
        where TSettings : NgxSettingsBase
        where TService : ICommandService<NgxCommand>
    {
        public NgxCommandBase(TService service) : base(service, "cmd")
        {
        }

        protected override NgxCommand MapToCommand(TSettings settings)
        {
            return new NgxCommand(settings.Name);
        }
    }
}
