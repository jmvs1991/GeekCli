using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet
{
    internal abstract class DotnetTemplateNameSettingsBase : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The template name used for file names and type names.")]
        public string? Name { get; set; }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return ValidationResult.Error("The <name> argument is required.");
            }

            return ValidationResult.Success();
        }

        protected static string NormalizeScope(string? scope)
        {
            return DotnetScopeHelper.Normalize(scope);
        }
    }
}
