using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Controller;
using GeekCliServices.Services.Dotnet.Controller.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetControllerTool : McpToolBase
    {
        private readonly IDotnetControllerService _service;

        public DotnetControllerTool(IDotnetControllerService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET controller template for an entity or view.")]
        public McpToolResult DotnetController(string name, string projectName, string codeField, bool view = false)
            => Capture(() => _service.RunProcess("dotnet", new DotnetControllerCommand(name, projectName, codeField, view)));
    }
}
