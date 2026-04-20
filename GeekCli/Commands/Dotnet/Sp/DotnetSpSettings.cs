using Spectre.Console;

namespace GeekCli.Commands.Dotnet.Sp
{
    internal sealed class DotnetSpSettings : DotnetProjectTemplateSettingsBase
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
                return ValidationResult.Error("The --scope option for sp must be basic, corp, or corp-co.");
            }

            return ValidationResult.Success();
        }
    }
}
