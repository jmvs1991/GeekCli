using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Service;
using GeekCliServices.Services.Dotnet.Service.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetServiceTool : McpToolBase
    {
        private readonly IDotnetServiceService _service;

        public DotnetServiceTool(IDotnetServiceService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET service template for an entity or view.")]
        public McpToolResult DotnetService(string name, string projectName, string scope = "basic", bool view = false)
            => Capture(() => _service.RunProcess("dotnet", new DotnetServiceCommand(name, projectName, ParseScope(scope), view)));
    }
}
