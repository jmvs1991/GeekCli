using System.ComponentModel;
using GeekCliServices.Services.Ngx.Models;
using GeekCliServices.Services.Ngx.Page;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class NgxPageTool : McpToolBase
    {
        private readonly INgxPageService _service;

        public NgxPageTool(INgxPageService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates an Angular page in a standalone Angular project or an Nx workspace. In Nx mode, provide the project, folder, and declaring module.")]
        public McpToolResult NgxPage(
            [Description("Page name. In Nx mode it is converted to kebab-case and used as the final path segment.")] string name,
            [Description("Set true to generate in an Nx workspace; defaults to false for standalone Angular CLI generation.")] bool nx = false,
            [Description("Nx Angular application name, such as ing-sel-crm. Required when nx is true.")] string? project = null,
            [Description("Folder beneath src/app, either relative to src/app or a workspace path beginning apps/<project>/src/app/. Required when nx is true; path segments are converted to PascalCase.")] string? folder = null,
            [Description("Workspace-relative path to the NgModule file that declares the page, such as apps/ing-sel-crm/src/app/Modules/Catalog/catalog.module.ts. Required when nx is true.")] string? module = null)
            => Capture(() => _service.RunProcess("cmd", new NgxCommand(name, nx, project, folder, module)));
    }
}
