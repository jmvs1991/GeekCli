using GeekCliServices.Services.Ngx.Page;

using GeekCliServices.Services.Ngx.Models;

namespace GeekCli.Commands.Ngx.Page
{
    class NgxPageCommand : NgxCommandBase<NgxPageSettings, INgxPageService>
    {
        public NgxPageCommand(INgxPageService service) : base(service)
        {
        }

        protected override NgxCommand MapToCommand(NgxPageSettings settings)
        {
            return new NgxCommand(settings.Name, settings.Nx, settings.Project, settings.Folder, settings.Module);
        }
    }
}
