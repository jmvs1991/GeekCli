using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Write;
using GeekCliServices.Services.Dotnet.Write.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DotnetWriteTool : McpToolBase
    {
        private readonly IDotnetWriteService _service;

        public DotnetWriteTool(IDotnetWriteService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET write repository template.")]
        public McpToolResult DotnetWrite(string name, string dbSchema, string contextName)
            => Capture(() => _service.RunProcess("dotnet", new DotnetWriteCommand(name, dbSchema, contextName)));
    }
}
