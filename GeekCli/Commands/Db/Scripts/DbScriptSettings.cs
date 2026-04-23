using GeekCliServices.Services.Db.Scripts;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db.Scripts
{
    internal sealed class DbScriptSettings : DbSettingBase
    {
        [CommandOption("--schema <SCHEMA>")]
        [Description("Database schema affected by the migration.")]
        public string? Schema { get; set; }

        [CommandOption("--type <TYPE>")]
        [Description("Migration type. Supported values: Query, Modify SP, Create SP, Modify Table, Create Table, Create View, Modify View.")]
        public string? Type { get; set; }

        [CommandOption("--issue <ISSUE>")]
        [Description("Issue or ticket used to group the generated scripts.")]
        public string? Issue { get; set; }

        [CommandOption("--object-name <OBJECT_NAME>")]
        [Description("Object name for table, view, or stored procedure migrations.")]
        public string? ObjectName { get; set; }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                return ValidationResult.Error("The --project option is required.");
            }

            if (string.IsNullOrWhiteSpace(Schema))
            {
                return ValidationResult.Error("The --schema option is required.");
            }

            if (string.IsNullOrWhiteSpace(Type))
            {
                return ValidationResult.Error("The --type option is required.");
            }

            if (!DbScriptTypeParser.TryParse(Type, out var parsedType))
            {
                return ValidationResult.Error("The --type option must be Query, Modify SP, Create SP, Modify Table, Create Table, Create View, or Modify View.");
            }

            if (string.IsNullOrWhiteSpace(Issue))
            {
                return ValidationResult.Error("The --issue option is required.");
            }

            if (DbScriptRules.RequiresObjectName(parsedType) && string.IsNullOrWhiteSpace(ObjectName))
            {
                return ValidationResult.Error($"The --object-name option is required for {DbScriptTypeParser.ToDisplayName(parsedType)}.");
            }

            return ValidationResult.Success();
        }
    }
}
