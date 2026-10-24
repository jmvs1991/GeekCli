using GeekCliServices.Services.Ngx.Component;

namespace GeekCli.Commands.Ngx.Component
{
    class NgxComponentCommand : NgxCommandBase<NgxComponentSettings, INgxComponentService>
    {
        public NgxComponentCommand(INgxComponentService service) : base(service)
        {
        }

        protected override GeekCliServices.Services.Ngx.Models.NgxCommand MapToCommand(NgxComponentSettings settings)
        {
            return new GeekCliServices.Services.Ngx.Models.NgxCommand(
                settings.Name,
                settings.Nx,
                settings.Project,
                settings.Folder,
                settings.Module);
        }
    }
}
