using System.ComponentModel;
using GeekCliServices.Services.Dotnet.UnitTestService;
using GeekCliServices.Services.Dotnet.UnitTestService.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DotnetUnitTestServiceTool : McpToolBase
    {
        private readonly IDotnetUnitTestServiceService _service;

        public DotnetUnitTestServiceTool(IDotnetUnitTestServiceService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET service unit test template for paginationful filterless services.")]
        public McpToolResult DotnetUnitTestService(string name, string projectName, string scope = "basic")
            => Capture(() => _service.RunProcess("dotnet",
                new DotnetUnitTestServiceCommand(name, projectName, ParseScope(scope))));
    }
}
