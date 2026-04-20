using Spectre.Console;

namespace GeekCli.Commands.Dotnet.Resource
{
    internal sealed class DotnetResourceSettings : DotnetProjectTemplateSettingsBase
    {
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
                return ValidationResult.Error("The --scope option for resource must be basic, corp, corp-co, or corp-co-code.");
            }

            return ValidationResult.Success();
        }
    }
}
