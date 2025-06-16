using Spectre.Console.Cli;
using Spectre.Console;
using System.Diagnostics;

namespace GeekCli.Commands.Db
{
    class MigrationCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            var name = AnsiConsole.Ask<string>("¿Nombre de la migración?");
            var project = AnsiConsole.Ask<string>("¿Proyecto donde está DbContext?");
            var startup = AnsiConsole.Ask<string>("¿Proyecto de inicio (startup)?");

            var command = $"dotnet ef migrations add {name} -p {project} -s {startup}";

            AnsiConsole.MarkupLine($"\n[green]Ejecutando:[/] {command}");
            var process = new ProcessStartInfo("dotnet", command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process.Start(process)?.WaitForExit();

            return 0;
        }
    }
}
