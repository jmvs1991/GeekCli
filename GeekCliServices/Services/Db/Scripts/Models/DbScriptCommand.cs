using GeekCliServices.Services.Db.Models;

namespace GeekCliServices.Services.Db.Scripts.Models
{
    public sealed record DbScriptCommand : DbCommandBase
    {
        public string Schema { get; init; }

        public DbScriptType Type { get; init; }

        public string Issue { get; init; }

        public string? ObjectName { get; init; }

        public DbScriptCommand(string projectName, bool init, string schema, DbScriptType type, string issue, string? objectName)
            : base(projectName, init)
        {
            Schema = schema;
            Type = type;
            Issue = issue;
            ObjectName = objectName;
        }
    }
}
