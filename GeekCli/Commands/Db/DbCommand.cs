using Spectre.Console.Cli;
using Spectre.Console;

namespace GeekCli.Commands.Db
{
    class DbCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            AnsiConsole.MarkupLine("[blue]Opciones disponibles para [bold]db[/]:[/]");
            AnsiConsole.MarkupLine("- [green]migration[/]: Crear una nueva migración");
            AnsiConsole.MarkupLine("- [green]seed[/]: Ejecutar seeders");
            AnsiConsole.MarkupLine("- [green]drop[/]: Eliminar la base de datos");
            return 0;
        }
    }
}
