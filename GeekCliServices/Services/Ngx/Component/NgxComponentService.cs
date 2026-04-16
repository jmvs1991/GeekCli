using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx.Component
{
    public sealed class NgxComponentService : NgxServiceBase, INgxComponentService
    {
        protected override string BuildArgs(NgxCommand command)
        {
            return BuildArgs($"g c {command.Name} --prefix ngx");
        }
    }
}
