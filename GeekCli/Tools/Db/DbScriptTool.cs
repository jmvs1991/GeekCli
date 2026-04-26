using System.ComponentModel;
using GeekCliServices.Services.Db.Scripts;
using GeekCliServices.Services.Db.Scripts.Models;
using ModelContextProtocol.Server;

namespace GeekCli.Tools
{
    [McpServerToolType]
    public sealed class DbScriptTool : McpToolBase
    {
        private readonly IDbScriptService _service;

        public DbScriptTool(IDbScriptService service)
        {
            _service = service;
        }

        [McpServerTool]
        [Description("Generates Up and Down SQL migration scripts for a Geek schema project, including dbo synonyms for create operations.")]
        public McpToolResult DbScript(string projectName, string schema, string type, string issue, bool init = false, string? objectName = null)
        {
            if (!DbScriptTypeParser.TryParse(type, out var parsedType))
            {
                throw new ArgumentException("Invalid script type.", nameof(type));
            }

            return Capture(() => _service.RunProcess(string.Empty,
                                                     new DbScriptCommand(projectName, init, schema, parsedType, issue, objectName)));
        }
    }
}
