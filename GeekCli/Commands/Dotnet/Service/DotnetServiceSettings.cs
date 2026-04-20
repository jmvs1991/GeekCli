using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet.Service
{
    internal sealed class DotnetServiceSettings : DotnetProjectTemplateSettingsBase
    {
        [CommandOption("--view")]
        [Description("Generates a view service instead of an entity service.")]
        public bool View { get; set; }

        public override ValidationResult Validate()
        {
            var baseValidation = base.Validate();

            if (!baseValidation.Successful)
            {
                return baseValidation;
            }

            var scope = NormalizeScope(Scope);
            if (scope is not ("basic" or "corp" or "corp-co" or "corp-co-code"))
            {
                return ValidationResult.Error("The --scope option for service must be basic, corp, corp-co, or corp-co-code.");
            }

            return ValidationResult.Success();
        }
    }
}
