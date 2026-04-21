using System.ComponentModel;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Remove.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DbMigrationRemoveTool : McpToolBase
    {
        private readonly IRemoveMigrationService _service;

        public DbMigrationRemoveTool(IRemoveMigrationService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Removes the latest Entity Framework migration from a Geek schema project.")]
        public McpToolResult DbMigrationRemove(string projectName, bool init = false)
            => Capture(() => _service.RunProcess("dotnet", new RemoveMigrationCommand(projectName, init)));
    }
}
