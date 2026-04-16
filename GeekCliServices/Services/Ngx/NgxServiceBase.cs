using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx
{
    public abstract class NgxServiceBase : ExternalProcessServiceBase<NgxCommand>
    {
        protected string BuildArgs(string command)
        {
            return $"/c ng {command}";
        }
    }
}
