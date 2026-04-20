using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db.Scaffold
{
    internal sealed class DbScaffoldSettings : CommandSettings
    {
        [CommandOption("--table <TABLE>")]
        [Description("The database table to scaffold.")]
        public string? Table { get; set; }

        [CommandOption("--output-dir <DIR>")]
        [Description("The output directory where the generated entities will be written.")]
        public string? OutputDir { get; set; }

        [CommandOption("--connection-string <CONNECTION_STRING>")]
        [Description("The database connection string used for scaffolding.")]
        public string? ConnectionString { get; set; }

        [CommandOption("--provider <PROVIDER>")]
        [Description("The EF Core provider to use. Supported values: SqlServer, Postgres.")]
        public string? Provider { get; set; }

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(Table))
            {
                return ValidationResult.Error("The --table option is required.");
            }

            if (string.IsNullOrWhiteSpace(OutputDir))
            {
                return ValidationResult.Error("The --output-dir option is required.");
            }

            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                return ValidationResult.Error("The --connection-string option is required.");
            }

            if (string.IsNullOrWhiteSpace(Provider))
            {
                return ValidationResult.Error("The --provider option is required.");
            }

            if (!IsSupportedProvider(Provider))
            {
                return ValidationResult.Error("The --provider option must be SqlServer or Postgres.");
            }

            return ValidationResult.Success();
        }

        private static bool IsSupportedProvider(string provider)
        {
            return provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase) ||
                   provider.Equals("Postgres", StringComparison.OrdinalIgnoreCase);
        }
    }
}
