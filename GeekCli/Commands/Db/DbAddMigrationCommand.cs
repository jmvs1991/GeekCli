using Spectre.Console.Cli;
using Spectre.Console;
using System.Diagnostics;

namespace GeekCli.Commands.Db
{
    class DbAddMigrationCommand : Command<DbAddMigrationSettings>
    {
        public override int Execute(CommandContext context, DbAddMigrationSettings settings)
        {
            string project = settings.Init ? $"{settings.ProjectName}.SchemaInitialization" : $"{settings.ProjectName}.SchemaUpdates";
            string issue = settings.Issue;
            string migration = settings.MigrationName;
            string manager = $"{settings.ProjectName}.Manager";


            var args = $"ef migrations add {migration} " +
                             $"--project .\\{project}\\{project}.csproj " +
                             $"--startup-project .\\{manager}\\{manager}.csproj " +
                             $"-o Migrations/{issue} -v -- --Assembly:{project}";

            AnsiConsole.MarkupLine($"[blue]Running:[/] dotnet {args}");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                AnsiConsole.MarkupLine($"[green]{EscapeMarkup(line)}[/]");
            }

            while (!process.StandardError.EndOfStream)
            {
                var error = process.StandardError.ReadLine();
                AnsiConsole.MarkupLine($"[red]{EscapeMarkup(error)}[/]");
            }

            process.WaitForExit();
            return process.ExitCode;

        }

        private string EscapeMarkup(string text)
        {
            return text.Replace("[", "[[").Replace("]", "]]");
        }
    }
}
