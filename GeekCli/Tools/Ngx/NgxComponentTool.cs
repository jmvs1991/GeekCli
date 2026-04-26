using System.ComponentModel;
using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class NgxComponentTool : McpToolBase
    {
        private readonly INgxComponentService _service;

        public NgxComponentTool(INgxComponentService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates an Angular component using the project Angular CLI setup.")]
        public McpToolResult NgxComponent(string name)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name)));
    }
}
