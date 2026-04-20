using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet
{
    internal abstract class DotnetProjectTemplateSettingsBase : DotnetTemplateNameSettingsBase
    {
        [CommandOption("--projectName <PROJECT_NAME>")]
        [Description("The project name used for namespaces and references.")]
        public string? ProjectName { get; set; }

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

            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                return ValidationResult.Error("The --projectName option is required.");
            }

            return ValidationResult.Success();
        }
    }
}
