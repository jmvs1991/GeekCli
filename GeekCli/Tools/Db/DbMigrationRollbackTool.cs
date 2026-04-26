using System.ComponentModel;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Migrations.Rollback.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DbMigrationRollbackTool : McpToolBase
    {
        private readonly IRollbackMigrationService _service;

        public DbMigrationRollbackTool(IRollbackMigrationService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Rolls back a Geek schema database to a specific migration.")]
        public McpToolResult DbMigrationRollback(string projectName, string migrationName, bool init = false)
            => Capture(() => _service.RunProcess("dotnet", new RollbackMigrationCommand(projectName, init, migrationName)));
    }
}
