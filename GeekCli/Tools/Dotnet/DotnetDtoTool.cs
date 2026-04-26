using System.ComponentModel;
using GeekCliServices.Services.Dotnet.Dto;
using GeekCliServices.Services.Dotnet.Dto.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DotnetDtoTool : McpToolBase
    {
        private readonly IDotnetDtoService _service;

        public DotnetDtoTool(IDotnetDtoService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates a Geek .NET DTO or view DTO template.")]
        public McpToolResult DotnetDto(string name, string projectName, string scope = "basic", bool view = false)
            => Capture(() => _service.RunProcess("dotnet", new DotnetDtoCommand(name, projectName, ParseScope(scope), view)));
    }
}
