using GeekCliServices.Services.Ngx.Service;

namespace GeekCli.Commands.Ngx.Service
{
    class NgxServiceCommand : NgxCommandBase<NgxServiceSettings, INgxServiceService>
    {
        public NgxServiceCommand(INgxServiceService service) : base(service)
        {
        }
    }
}
