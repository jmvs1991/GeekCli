using System.ComponentModel;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Page;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class NgxPageTool : McpToolBase
    {
        private readonly INgxPageService _service;

        public NgxPageTool(INgxPageService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates an Angular page component using the project Angular CLI setup.")]
        public McpToolResult NgxPage(string name)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name)));
    }
}
