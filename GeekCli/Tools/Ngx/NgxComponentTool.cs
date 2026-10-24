using System.ComponentModel;
using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class NgxComponentTool : McpToolBase
    {
        private readonly INgxComponentService _service;

        public NgxComponentTool(INgxComponentService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates an Angular component in a standalone Angular project or an Nx workspace. In Nx mode, provide a tech:angular project (application or library), a workspace-relative destination folder, and a workspace-relative NgModule file path.")]
        public McpToolResult NgxComponent(
            [Description("Component name. In Nx mode it is converted to kebab-case and used as the final path segment.")] string name,
            [Description("Set true to generate in an Nx workspace; defaults to false for standalone Angular CLI generation.")] bool nx = false,
            [Description("Nx project name tagged tech:angular, such as ing-sel-crm or core. Required when nx is true.")] string? project = null,
            [Description("Workspace-relative destination folder under apps/<project>/src/app for applications or under the library sourceRoot. Required when nx is true; the path is used as provided.")] string? folder = null,
            [Description("Workspace-relative path to the declaring NgModule file, including .ts, such as packages/ngx/core/src/lib/core.module.ts. Required when nx is true.")] string? module = null)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name, nx, project, folder, module)));
    }
}
