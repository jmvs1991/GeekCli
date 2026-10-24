using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Service;

namespace GeekCli.Commands.Ngx.Service
{
    class NgxServiceCommand : NgxCommandBase<NgxServiceSettings, INgxServiceService>
    {
        public NgxServiceCommand(INgxServiceService service) : base(service)
        {
        }

        protected override NgxCommand MapToCommand(NgxServiceSettings settings)
        {
            return new NgxCommand(
                settings.Name,
                settings.Nx,
                settings.Project,
                settings.Folder);
        }
    }
}
