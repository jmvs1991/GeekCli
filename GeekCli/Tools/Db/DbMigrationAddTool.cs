using System.ComponentModel;
using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Add.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    public sealed class DbMigrationAddTool : McpToolBase
    {
        private readonly IAddMigrationService _service;

        public DbMigrationAddTool(IAddMigrationService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Adds a new Entity Framework migration for a Geek schema project.")]
        public McpToolResult DbMigrationAdd(string projectName, string migrationName, string issue, bool init = false)
            => Capture(() => _service.RunProcess("dotnet", new AddMigrationCommand(projectName, init, migrationName, issue)));
    }
}
