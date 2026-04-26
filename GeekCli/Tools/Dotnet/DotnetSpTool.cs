using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Sp;
using GeekCliServices.Services.Dotnet.Sp.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DotnetSpTool : McpToolBase
    {
        private readonly IDotnetSpService _service;

        public DotnetSpTool(IDotnetSpService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET stored procedure template.")]
        public McpToolResult DotnetSp(string name, string projectName, string scope = "basic")
            => Capture(() => _service.RunProcess("dotnet", new DotnetSpCommand(name, projectName, ParseScope(scope))));
    }
}
