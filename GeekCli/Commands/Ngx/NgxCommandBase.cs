using GeekCliServices.Services;
using GeekCliServices.Services.Ngx.Models;

namespace GeekCli.Commands.Ngx
{
    abstract class NgxCommandBase<TService> : CommandBase<NgxSettingsBase, TService, NgxCommand> where TService : ICommandService<NgxCommand>
    {
        public NgxCommandBase(TService service) : base(service, "cmd")
        {
        }

        protected override NgxCommand MapToCommand(NgxSettingsBase settings)
        {
            return new NgxCommand(settings.Name);
        }
    }
}
