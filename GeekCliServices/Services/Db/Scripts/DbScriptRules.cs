namespace GeekCliServices.Services.Db.Scripts
{
    public static class DbScriptRules
    {
        public static bool RequiresObjectName(DbScriptType type)
        {
            return type != DbScriptType.Query;
        }

        public static bool CreatesObject(DbScriptType type)
        {
            return type == DbScriptType.CreateStoredProcedure ||
                   type == DbScriptType.CreateTable ||
                   type == DbScriptType.CreateView;
        }

        public static string ResolveProjectName(string projectName, bool init)
        {
            return init ? $"{projectName}.SchemaInitialization" : $"{projectName}.SchemaUpdates";
        }

        public static string ResolveFolderName(DbScriptType type)
        {
            return type switch
            {
                DbScriptType.Query => "Queries",
                DbScriptType.ModifyStoredProcedure or DbScriptType.CreateStoredProcedure => "Sp",
                DbScriptType.ModifyTable or DbScriptType.CreateTable => "Tables",
                DbScriptType.CreateView or DbScriptType.ModifyView => "Views",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported script type.")
            };
        }

        public static string ResolveVerb(DbScriptType type)
        {
            return type switch
            {
                DbScriptType.Query => "QUERY",
                DbScriptType.ModifyStoredProcedure or DbScriptType.ModifyTable or DbScriptType.ModifyView => "ALTER",
                DbScriptType.CreateStoredProcedure or DbScriptType.CreateTable or DbScriptType.CreateView => "CREATE",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported script type.")
            };
        }

        public static void Validate(string projectName, string schema, string issue, DbScriptType type, string? objectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException("Project is required.", nameof(projectName));
            }

            if (string.IsNullOrWhiteSpace(schema))
            {
                throw new ArgumentException("Schema is required.", nameof(schema));
            }

            if (string.IsNullOrWhiteSpace(issue))
            {
                throw new ArgumentException("Issue is required.", nameof(issue));
            }

            if (RequiresObjectName(type) && string.IsNullOrWhiteSpace(objectName))
            {
                throw new ArgumentException($"Object name is required for {DbScriptTypeParser.ToDisplayName(type)}.", nameof(objectName));
            }
        }

        public static string ResolveFileToken(DbScriptType type, string issue, string? objectName)
        {
            if (!string.IsNullOrWhiteSpace(objectName))
            {
                return objectName;
            }

            return type == DbScriptType.Query ? issue : throw new ArgumentException("Object name is required.", nameof(objectName));
        }
    }
}
