using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx.Page
{
    public sealed class NgxPageService : NgxServiceBase, INgxPageService
    {
        protected override string BuildArgs(NgxCommand command)
        {
            return BuildArgs($"g c {command.Name} --type page");
        }
    }
}
