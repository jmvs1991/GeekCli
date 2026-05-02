using System.ComponentModel;
using GeekCliServices.Services.Dotnet.ApiUnitTest;
using GeekCliServices.Services.Dotnet.ApiUnitTest.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DotnetApiUnitTestTool : McpToolBase
    {
        private readonly IDotnetApiUnitTestService _service;

        public DotnetApiUnitTestTool(IDotnetApiUnitTestService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET API unit test template for paginationful filterless endpoints.")]
        public McpToolResult DotnetApiUnitTest(string name,
                                               string projectName,
                                               string codeField,
                                               string serviceInterface,
                                               string dtoName,
                                               string responseName,
                                               string contextTestBase,
                                               string endpoint,
                                               string scope = "basic")
            => Capture(() => _service.RunProcess("dotnet",
                new DotnetApiUnitTestCommand(name,
                                             projectName,
                                             codeField,
                                             serviceInterface,
                                             dtoName,
                                             responseName,
                                             contextTestBase,
                                             endpoint,
                                             ParseScope(scope))));
    }
}
