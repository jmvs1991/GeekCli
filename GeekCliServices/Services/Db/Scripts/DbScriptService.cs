using GeekCliServices.Services.Db.Scripts.Models;

namespace GeekCliServices.Services.Db.Scripts
{
    public sealed class DbScriptService : IDbScriptService
    {
        public int RunProcess(string processToRun, DbScriptCommand command)
        {
            DbScriptRules.Validate(command.ProjectName, command.Schema, command.Issue, command.Type, command.ObjectName);

            string finalProjectName = DbScriptRules.ResolveProjectName(command.ProjectName, command.Init);
            string fileVerb = DbScriptRules.ResolveVerb(command.Type);
            string fileToken = SanitizePathSegment(DbScriptRules.ResolveFileToken(command.Type, command.Issue, command.ObjectName));
            string issueFolder = SanitizePathSegment(command.Issue);
            string schemaFolder = SanitizePathSegment(command.Schema);
            string objectName = command.ObjectName?.Trim() ?? command.Issue.Trim();
            string folderName = DbScriptRules.ResolveFolderName(command.Type);

            string projectRoot = Path.Combine(Environment.CurrentDirectory, finalProjectName);
            string upDirectory = Path.Combine(projectRoot, "Scripts", schemaFolder, "Up", folderName, issueFolder);
            string downDirectory = Path.Combine(projectRoot, "Scripts", schemaFolder, "Down", folderName, issueFolder);

            Directory.CreateDirectory(upDirectory);
            Directory.CreateDirectory(downDirectory);

            string upFileName = $"{fileVerb}_{fileToken}.sql";
            string downFileName = $"ROLLBACK_{fileVerb}_{fileToken}.sql";

            string upPath = Path.Combine(upDirectory, upFileName);
            string downPath = Path.Combine(downDirectory, downFileName);

            File.WriteAllText(upPath, BuildUpContent(command.Type, command.Schema, objectName, command.Issue));
            File.WriteAllText(downPath, BuildDownContent(command.Type, command.Schema, objectName));

            CommandOutput.Info($"Generated Up script: {upPath}");
            CommandOutput.Info($"Generated Down script: {downPath}");

            if (DbScriptRules.CreatesObject(command.Type))
            {
                CreateSynonymScripts(projectRoot, issueFolder, command.Schema, objectName, fileVerb, fileToken);
            }

            return 0;
        }

        private static void CreateSynonymScripts(string projectRoot, string issueFolder, string schema, string objectName, string fileVerb, string fileToken)
        {
            string synonymUpDirectory = Path.Combine(projectRoot, "Scripts", "dbo", "Up", "Synonyms", issueFolder);
            string synonymDownDirectory = Path.Combine(projectRoot, "Scripts", "dbo", "Down", "Synonyms", issueFolder);

            Directory.CreateDirectory(synonymUpDirectory);
            Directory.CreateDirectory(synonymDownDirectory);

            string upPath = Path.Combine(synonymUpDirectory, $"{fileVerb}_{fileToken}.sql");
            string downPath = Path.Combine(synonymDownDirectory, $"ROLLBACK_{fileVerb}_{fileToken}.sql");

            File.WriteAllText(upPath, BuildCreateSynonymContent(schema, objectName));
            File.WriteAllText(downPath, BuildDropSynonymContent(objectName));

            CommandOutput.Info($"Generated synonym Up script: {upPath}");
            CommandOutput.Info($"Generated synonym Down script: {downPath}");
        }

        private static string BuildUpContent(DbScriptType type, string schema, string objectName, string issue)
        {
            return type switch
            {
                DbScriptType.Query =>
                    $"-- Issue: {issue}{Environment.NewLine}" +
                    $"-- Schema: {schema}{Environment.NewLine}" +
                    $"-- TODO: Add the SQL statements to apply this query migration.{Environment.NewLine}",
                DbScriptType.CreateStoredProcedure =>
                    $"CREATE PROCEDURE {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"AS{Environment.NewLine}" +
                    $"BEGIN{Environment.NewLine}" +
                    $"    -- TODO: Implement procedure body.{Environment.NewLine}" +
                    $"END{Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                DbScriptType.ModifyStoredProcedure =>
                    $"ALTER PROCEDURE {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"AS{Environment.NewLine}" +
                    $"BEGIN{Environment.NewLine}" +
                    $"    -- TODO: Implement procedure changes.{Environment.NewLine}" +
                    $"END{Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                DbScriptType.CreateTable =>
                    $"CREATE TABLE {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"({Environment.NewLine}" +
                    $"    -- TODO: Define columns.{Environment.NewLine}" +
                    $"){Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                DbScriptType.ModifyTable =>
                    $"ALTER TABLE {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"    -- TODO: Add ALTER TABLE statements here.{Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                DbScriptType.CreateView =>
                    $"CREATE VIEW {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"AS{Environment.NewLine}" +
                    $"    -- TODO: Define the view query.{Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                DbScriptType.ModifyView =>
                    $"ALTER VIEW {Qualify(schema, objectName)}{Environment.NewLine}" +
                    $"AS{Environment.NewLine}" +
                    $"    -- TODO: Define the updated view query.{Environment.NewLine}" +
                    $"GO{Environment.NewLine}",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported script type.")
            };
        }

        private static string BuildDownContent(DbScriptType type, string schema, string objectName)
        {
            return type switch
            {
                DbScriptType.CreateStoredProcedure => BuildDropProcedureContent(schema, objectName),
                DbScriptType.CreateTable => BuildDropTableContent(schema, objectName),
                DbScriptType.CreateView => BuildDropViewContent(schema, objectName),
                _ => string.Empty
            };
        }

        private static string BuildDropProcedureContent(string schema, string objectName)
        {
            return $"IF OBJECT_ID(N'{Qualify(schema, objectName)}', N'P') IS NOT NULL{Environment.NewLine}" +
                   $"BEGIN{Environment.NewLine}" +
                   $"    DROP PROCEDURE {Qualify(schema, objectName)};{Environment.NewLine}" +
                   $"END{Environment.NewLine}" +
                   $"GO{Environment.NewLine}";
        }

        private static string BuildDropTableContent(string schema, string objectName)
        {
            return $"IF OBJECT_ID(N'{Qualify(schema, objectName)}', N'U') IS NOT NULL{Environment.NewLine}" +
                   $"BEGIN{Environment.NewLine}" +
                   $"    DROP TABLE {Qualify(schema, objectName)};{Environment.NewLine}" +
                   $"END{Environment.NewLine}" +
                   $"GO{Environment.NewLine}";
        }

        private static string BuildDropViewContent(string schema, string objectName)
        {
            return $"IF OBJECT_ID(N'{Qualify(schema, objectName)}', N'V') IS NOT NULL{Environment.NewLine}" +
                   $"BEGIN{Environment.NewLine}" +
                   $"    DROP VIEW {Qualify(schema, objectName)};{Environment.NewLine}" +
                   $"END{Environment.NewLine}" +
                   $"GO{Environment.NewLine}";
        }

        private static string BuildCreateSynonymContent(string schema, string objectName)
        {
            return $"IF OBJECT_ID(N'[dbo].[{EscapeSqlName(objectName)}]', N'SN') IS NOT NULL{Environment.NewLine}" +
                   $"BEGIN{Environment.NewLine}" +
                   $"    DROP SYNONYM [dbo].[{EscapeSqlName(objectName)}];{Environment.NewLine}" +
                   $"END{Environment.NewLine}" +
                   $"GO{Environment.NewLine}" +
                   $"CREATE SYNONYM [dbo].[{EscapeSqlName(objectName)}] FOR {Qualify(schema, objectName)};{Environment.NewLine}" +
                   $"GO{Environment.NewLine}";
        }

        private static string BuildDropSynonymContent(string objectName)
        {
            return $"IF OBJECT_ID(N'[dbo].[{EscapeSqlName(objectName)}]', N'SN') IS NOT NULL{Environment.NewLine}" +
                   $"BEGIN{Environment.NewLine}" +
                   $"    DROP SYNONYM [dbo].[{EscapeSqlName(objectName)}];{Environment.NewLine}" +
                   $"END{Environment.NewLine}" +
                   $"GO{Environment.NewLine}";
        }

        private static string Qualify(string schema, string objectName)
        {
            return $"[{EscapeSqlName(schema)}].[{EscapeSqlName(objectName)}]";
        }

        private static string EscapeSqlName(string value)
        {
            return value.Replace("]", "]]");
        }

        private static string SanitizePathSegment(string value)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var sanitized = new string(value.Trim().Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray());
            return string.IsNullOrWhiteSpace(sanitized) ? "_" : sanitized;
        }
    }
}
