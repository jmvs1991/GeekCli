using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet.Dto
{
    internal sealed class DotnetDtoSettings : DotnetProjectTemplateSettingsBase
    {
        [CommandOption("--view")]
        [Description("Generates a View DTO instead of an entity DTO.")]
        public bool View { get; set; }

        public override ValidationResult Validate()
        {
            var baseValidation = base.Validate();

            if (!baseValidation.Successful)
            {
                return baseValidation;
            }

            var scope = NormalizeScope(Scope);
            if (scope is not ("basic" or "corp" or "corp-co"))
            {
                return ValidationResult.Error("The --scope option for dto must be basic, corp, or corp-co.");
            }

            return ValidationResult.Success();
        }
    }
}
