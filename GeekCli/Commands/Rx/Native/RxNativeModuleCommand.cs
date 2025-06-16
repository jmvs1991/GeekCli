using Spectre.Console.Cli;
using Spectre.Console;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeModuleCommand : Command<RxNativeModuleSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] RxNativeModuleSettings settings)
        {
            var basePath = Directory.GetCurrentDirectory();
            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            var moduleName = textInfo.ToTitleCase(settings.Name.ToLower());
            var modulePath = Path.Combine(basePath, moduleName);

            if (!Directory.Exists(modulePath))
            {
                Directory.CreateDirectory(modulePath);
                AnsiConsole.MarkupLine($"[green]Created module folder:[/] {modulePath}");
            }

            CreateSubfolder(modulePath, "Screens");
            CreateSubfolder(modulePath, "Core");
            CreateSubfolder(modulePath, "Modals");

            AnsiConsole.MarkupLine($"[bold green]✔ Native module structure created in:[/] [blue]{modulePath}[/]");
            return 0;
        }

        private void CreateSubfolder(string parent, string name)
        {
            var path = Path.Combine(parent, name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AnsiConsole.MarkupLine($"[grey]Created:[/] [yellow]{name}[/]");
            }

            var keepFilePath = Path.Combine(path, ".keep");
            File.WriteAllText(keepFilePath, string.Empty);
            AnsiConsole.MarkupLine($"[grey]Added:[/] [blue]{name}/.keep[/]");
        }
    }
}
