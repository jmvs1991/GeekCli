using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet.Controller
{
    internal sealed class DotnetControllerSettings : DotnetTemplateNameSettingsBase
    {
        [CommandOption("--projectName <PROJECT_NAME>")]
        [Description("The project name used for namespaces and references.")]
        public string? ProjectName { get; set; }

        [CommandOption("--codeField <CODE_FIELD>")]
        [Description("The code field used by the generated controller.")]
        public string? CodeField { get; set; }

        [CommandOption("--view")]
        [Description("Generates a view API controller instead of an entity API controller.")]
        public bool View { get; set; }

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

            if (string.IsNullOrWhiteSpace(CodeField))
            {
                return ValidationResult.Error("The --codeField option is required.");
            }

            return ValidationResult.Success();
        }
    }
}
