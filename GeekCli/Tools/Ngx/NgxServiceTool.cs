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
        [Description("Generates an Angular service in the current Angular project or a selected Nx project.")]
        public McpToolResult NgxService(
            [Description("Angular service name.")] string name,
            [Description("Generate the service in an Nx workspace.")] bool nx = false,
            [Description("The Nx Angular project name.")] string? project = null,
            [Description("The project-relative destination folder.")] string? folder = null)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name, nx, project, folder)));
    }
}
