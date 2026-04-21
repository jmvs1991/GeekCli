using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Resource;
using GeekCliServices.Services.Dotnet.Resource.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetResourceTool : McpToolBase
    {
        private readonly IDotnetResourceService _service;

        public DotnetResourceTool(IDotnetResourceService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET resource template for localized messages.")]
        public McpToolResult DotnetResource(string name, string projectName, string scope = "basic")
            => Capture(() => _service.RunProcess("dotnet", new DotnetResourceCommand(name, projectName, ParseScope(scope))));
    }
}
