using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet.Read
{
    internal sealed class DotnetReadSettings : DotnetDbTemplateSettingsBase
    {
        [CommandOption("--view")]
        [Description("Generates a view repository instead of an entity repository.")]
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
                return ValidationResult.Error("The --scope option for read must be basic, corp, corp-co, or corp-co-code.");
            }

            return ValidationResult.Success();
        }
    }
}
