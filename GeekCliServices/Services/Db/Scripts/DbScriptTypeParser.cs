namespace GeekCliServices.Services.Db.Scripts
{
    public static class DbScriptTypeParser
    {
        public static bool TryParse(string? value, out DbScriptType type)
        {
            type = default;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return Normalize(value) switch
            {
                "query" => Set(DbScriptType.Query, out type),
                "modifysp" or "modifystoredprocedure" or "altersp" or "alterstoredprocedure" =>
                    Set(DbScriptType.ModifyStoredProcedure, out type),
                "createsp" or "createstoredprocedure" =>
                    Set(DbScriptType.CreateStoredProcedure, out type),
                "modifytable" or "altertable" =>
                    Set(DbScriptType.ModifyTable, out type),
                "createtable" =>
                    Set(DbScriptType.CreateTable, out type),
                "createview" =>
                    Set(DbScriptType.CreateView, out type),
                "modifyview" or "alterview" =>
                    Set(DbScriptType.ModifyView, out type),
                _ => false
            };
        }

        public static string ToDisplayName(DbScriptType type)
        {
            return type switch
            {
                DbScriptType.Query => "Query",
                DbScriptType.ModifyStoredProcedure => "Modify SP",
                DbScriptType.CreateStoredProcedure => "Create SP",
                DbScriptType.ModifyTable => "Modify Table",
                DbScriptType.CreateTable => "Create Table",
                DbScriptType.CreateView => "Create View",
                DbScriptType.ModifyView => "Modify View",
                _ => type.ToString()
            };
        }

        private static string Normalize(string value)
        {
            return value.Replace("-", string.Empty)
                        .Replace("_", string.Empty)
                        .Replace(" ", string.Empty)
                        .Trim()
                        .ToLowerInvariant();
        }

        private static bool Set(DbScriptType parsedType, out DbScriptType type)
        {
            type = parsedType;
            return true;
        }
    }
}
