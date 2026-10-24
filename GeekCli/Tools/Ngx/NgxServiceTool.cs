using System.ComponentModel;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Service;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class NgxServiceTool : McpToolBase
    {
        private readonly INgxServiceService _service;

        public NgxServiceTool(INgxServiceService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates an Angular service in the current Angular project using Angular CLI defaults.")]
        public McpToolResult NgxService(
            [Description("Angular service name.")] string name)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name)));
    }
}
