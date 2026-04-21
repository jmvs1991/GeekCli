using System.ComponentModel;
using GeekCliServices.Services.Dotnet.List;
using GeekCliServices.Services.Dotnet.List.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetListTool : McpToolBase
    {
        private readonly IDotnetListService _service;

        public DotnetListTool(IDotnetListService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Lists installed Geek .NET templates.")]
        public McpToolResult DotnetList()
            => Capture(() => _service.RunProcess("dotnet", new DotnetListCommand()));
    }
}
