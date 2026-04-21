using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Cache;
using GeekCliServices.Services.Dotnet.Cache.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DotnetCacheTool : McpToolBase
    {
        private readonly IDotnetCacheService _service;

        public DotnetCacheTool(IDotnetCacheService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET cache template for the specified entity and scope.")]
        public McpToolResult DotnetCache(string name, string projectName, string scope = "basic")
            => Capture(() => _service.RunProcess("dotnet", new DotnetCacheCommand(name, projectName, ParseScope(scope))));
    }
}
