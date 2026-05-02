using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Dotnet.UnitTestService
{
    internal sealed class DotnetUnitTestServiceSettings : DotnetProjectTemplateSettingsBase
    {
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
                return ValidationResult.Error("The --scope option for unittest-service must be basic, corp, or corp-co.");
            }

            return ValidationResult.Success();
        }
    }
}
