using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet
{
    internal abstract class DotnetDbTemplateSettingsBase : DotnetTemplateNameSettingsBase
    {
        [CommandOption("--dbSchema <SCHEMA>")]
        [Description("The database schema name.")]
        public string? DbSchema { get; set; }

        [CommandOption("--contextName <CONTEXT_NAME>")]
        [Description("The DbContext name.")]
        public string? ContextName { get; set; }

        [CommandOption("--scope <SCOPE>")]
        [Description("Template scope. Supported values: basic, corp, corp-co, corp-co-code.")]
        public string? Scope { get; set; }

        public override ValidationResult Validate()
        {
            var baseValidation = base.Validate();

            if (!baseValidation.Successful)
            {
                return baseValidation;
            }

            if (string.IsNullOrWhiteSpace(DbSchema))
            {
                return ValidationResult.Error("The --dbSchema option is required.");
            }

            if (string.IsNullOrWhiteSpace(ContextName))
            {
                return ValidationResult.Error("The --contextName option is required.");
            }

            return ValidationResult.Success();
        }
    }
}
