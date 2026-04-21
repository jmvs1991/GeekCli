using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Read;
using GeekCliServices.Services.Dotnet.Read.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetReadTool : McpToolBase
    {
        private readonly IDotnetReadService _service;

        public DotnetReadTool(IDotnetReadService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET read repository template for an entity or view.")]
        public McpToolResult DotnetRead(string name, string dbSchema, string contextName, string scope = "basic", bool view = false)
            => Capture(() => _service.RunProcess("dotnet", new DotnetReadCommand(name, dbSchema, contextName, ParseScope(scope), view)));
    }
}
