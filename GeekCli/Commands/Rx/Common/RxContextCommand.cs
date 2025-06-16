using Spectre.Console;
using Spectre.Console.Cli;
using System.Globalization;

namespace GeekCli.Commands.Rx.Common
{
    class RxContextCommand : Command<RxContextSettings>
    {
        public override int Execute(CommandContext context, RxContextSettings settings)
        {
            var basePath = Directory.GetCurrentDirectory();
            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            var contextName = textInfo.ToTitleCase(settings.Name.ToLower());
            var targetPath = settings.Flat ? basePath : Path.Combine(basePath, settings.Name);

            if (!settings.Flat && !Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                AnsiConsole.MarkupLine($"[green]Created folder:[/] {targetPath}");
            }

            CreateFile(targetPath, $"{contextName}.actions.ts", "");
            CreateFile(targetPath, $"{contextName}.context.tsx", "");
            CreateFile(targetPath, $"{contextName}.hook.ts", "");
            CreateFile(targetPath, $"{contextName}.provider.tsx", "");
            CreateFile(targetPath, $"{contextName}.reducer.ts", "");
            CreateFile(targetPath, $"{contextName}.state.ts", "");

            AnsiConsole.MarkupLine($"[bold green]✔ Context files created successfully in:[/] [blue]{targetPath}[/]");
            return 0;
        }

        private void CreateFile(string path, string filename, string content)
        {
            var filePath = Path.Combine(path, filename);
            File.WriteAllText(filePath, content);
            AnsiConsole.MarkupLine($"[grey]Generated:[/] [yellow]{filename}[/]");
        }
    }
}
