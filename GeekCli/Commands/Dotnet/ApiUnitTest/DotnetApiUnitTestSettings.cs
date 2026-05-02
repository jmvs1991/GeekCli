using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Dotnet.ApiUnitTest
{
    internal sealed class DotnetApiUnitTestSettings : DotnetTemplateNameSettingsBase
    {
        [CommandOption("--projectName <PROJECT_NAME>")]
        [Description("The project name used for namespaces and references.")]
        public string? ProjectName { get; set; }

        [CommandOption("--scope <SCOPE>")]
        [Description("Template scope. Supported values: basic, corp, corp-co.")]
        public string? Scope { get; set; }

        [CommandOption("--codeField <CODE_FIELD>")]
        [Description("The code field used by the generated unit test.")]
        public string? CodeField { get; set; }

        [CommandOption("--serviceInterface <SERVICE_INTERFACE>")]
        [Description("The API service interface used by the generated unit test.")]
        public string? ServiceInterface { get; set; }

        [CommandOption("--dtoName <DTO_NAME>")]
        [Description("The DTO type name used by the generated unit test.")]
        public string? DtoName { get; set; }

        [CommandOption("--responseName <RESPONSE_NAME>")]
        [Description("The response type name used by the generated unit test.")]
        public string? ResponseName { get; set; }

        [CommandOption("--contextTestBase <CONTEXT_TEST_BASE>")]
        [Description("The base test class used by the generated unit test.")]
        public string? ContextTestBase { get; set; }

        [CommandOption("--endpoint <ENDPOINT>")]
        [Description("The API endpoint segment used by the generated unit test.")]
        public string? Endpoint { get; set; }

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

            if (string.IsNullOrWhiteSpace(ServiceInterface))
            {
                return ValidationResult.Error("The --serviceInterface option is required.");
            }

            if (string.IsNullOrWhiteSpace(DtoName))
            {
                return ValidationResult.Error("The --dtoName option is required.");
            }

            if (string.IsNullOrWhiteSpace(ResponseName))
            {
                return ValidationResult.Error("The --responseName option is required.");
            }

            if (string.IsNullOrWhiteSpace(ContextTestBase))
            {
                return ValidationResult.Error("The --contextTestBase option is required.");
            }

            if (string.IsNullOrWhiteSpace(Endpoint))
            {
                return ValidationResult.Error("The --endpoint option is required.");
            }

            var scope = NormalizeScope(Scope);
            if (scope is not ("basic" or "corp" or "corp-co"))
            {
                return ValidationResult.Error("The --scope option for unittest-api must be basic, corp, or corp-co.");
            }

            return ValidationResult.Success();
        }
    }
}
