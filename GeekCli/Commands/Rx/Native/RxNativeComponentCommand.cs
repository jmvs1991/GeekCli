using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeComponentCommand : Command<RxNativeComponentSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] RxNativeComponentSettings settings)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            var componentName = textInfo.ToTitleCase(settings.Name.ToLower());

            var basePath = Directory.GetCurrentDirectory();
            var targetPath = settings.Flat ? basePath : Path.Combine(basePath, componentName);

            if (!settings.Flat && !Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                AnsiConsole.MarkupLine($"[green]Created component folder:[/] {targetPath}");
            }

            CreateFile(targetPath, $"{componentName}.component.tsx", $"");
            CreateFile(targetPath, $"{componentName}.style.tsx", $"");

            AnsiConsole.MarkupLine($"[bold green]✔ Component files created for:[/] [blue]{componentName}[/]");
            return 0;
        }

        private void CreateFile(string path, string filename, string content)
        {
            var filePath = Path.Combine(path, filename);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, content);
                AnsiConsole.MarkupLine($"[grey]Generated:[/] [yellow]{filename}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Skipped (already exists):[/] [yellow]{filename}[/]");
            }
        }
    }
}
