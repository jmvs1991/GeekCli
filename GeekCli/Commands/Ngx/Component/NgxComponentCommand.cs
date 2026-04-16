using GeekCliServices.Services.Ngx.Component;

namespace GeekCli.Commands.Ngx.Component
{
    class NgxComponentCommand : NgxCommandBase<INgxComponentService>
    {
        public NgxComponentCommand(INgxComponentService service) : base(service)
        {
        }
    }
}
