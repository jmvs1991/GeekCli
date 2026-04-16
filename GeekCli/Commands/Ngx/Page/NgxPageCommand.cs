using GeekCliServices.Services.Ngx.Page;

namespace GeekCli.Commands.Ngx.Page
{
    class NgxPageCommand : NgxCommandBase<INgxPageService>
    {
        public NgxPageCommand(INgxPageService service) : base(service)
        {
        }
    }
}
