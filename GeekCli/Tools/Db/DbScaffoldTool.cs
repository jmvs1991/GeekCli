using System.ComponentModel;
using GeekCliServices.Services.Db.Scaffold;
using GeekCliServices.Services.Db.Scaffold.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DbScaffoldTool : McpToolBase
    {
        private readonly IDbScaffoldService _service;

        public DbScaffoldTool(IDbScaffoldService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Scaffolds an Entity Framework model from an existing database table.")]
        public McpToolResult DbScaffold(string table, string outputDir, string connectionString, string provider)
            => Capture(() => _service.RunProcess("dotnet", new DbScaffoldDotnetCommand(table, outputDir, connectionString, provider)));
    }
}
