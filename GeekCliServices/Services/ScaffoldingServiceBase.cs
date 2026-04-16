using Spectre.Console;
using System.Globalization;

namespace GeekCliServices.Services
{
    public abstract class ScaffoldingServiceBase<TCommand>
    {
        protected readonly string _basePath = Directory.GetCurrentDirectory();

        protected readonly TextInfo _textInfo = CultureInfo.InvariantCulture.TextInfo;

        protected void CreateDirectory(string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                AnsiConsole.MarkupLine($"[bold green]Created folder:[/] {targetPath}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Skipped (already exists):[/] {targetPath}");
            }
        }

        protected void CreateSubfolder(string parent, string name)
        {
            string path = Path.Combine(parent, name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AnsiConsole.MarkupLine($"[grey]Created:[/] [yellow]{name}[/]");
            }

            string keepFilePath = Path.Combine(path, ".keep");
            File.WriteAllText(keepFilePath, string.Empty);
            AnsiConsole.MarkupLine($"[grey]Added:[/] [blue]{name}/.keep[/]");
        }

        protected void CreateFile(string path, string filename, string content)
        {
            string filePath = Path.Combine(path, filename);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, content);
                AnsiConsole.MarkupLine($"[bold grey]Generated:[/] [yellow]{filename}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Skipped (already exists):[/] [yellow]{filename}[/]");
            }
        }
        
        public abstract int RunProcess(string processToRun, TCommand command);

        protected abstract void Execute(string targetPath, string name, TCommand command);

    }
}
