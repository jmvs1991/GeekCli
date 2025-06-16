using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeScreenCommand : Command<RxNativeScreenSettings>
    {
        public override int Execute([NotNull] CommandContext context, [NotNull] RxNativeScreenSettings settings)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;
            var screenName = textInfo.ToTitleCase(settings.Name.ToLower());

            var basePath = Directory.GetCurrentDirectory();
            var targetPath = settings.Flat ? basePath : Path.Combine(basePath, screenName);

            if (!settings.Flat && !Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                AnsiConsole.MarkupLine($"[green]Created screen folder:[/] {targetPath}");
            }

            CreateFile(targetPath, $"{screenName}.screen.tsx", $"");
            CreateFile(targetPath, $"{screenName}.style.tsx", $"");

            if (settings.Schema)
            {
                CreateFile(targetPath, $"{screenName}.schema.tsx", $"// {screenName} schema file");
            }

            if (settings.Wrapper)
            {
                CreateFile(targetPath, $"{screenName}.wrapper.tsx", $"// {screenName} wrapper component");
            }

            AnsiConsole.MarkupLine($"[bold green]✔ Screen files created for:[/] [blue]{screenName}[/]");
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
