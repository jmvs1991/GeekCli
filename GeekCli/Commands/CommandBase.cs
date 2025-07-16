using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics;
using System.Globalization;

namespace GeekCli.Commands
{
    abstract class CommandBase<S> : Command<S> where S : CommandSettings
    {
        protected readonly string _basePath = Directory.GetCurrentDirectory();

        protected readonly TextInfo _textInfo = CultureInfo.InvariantCulture.TextInfo;

        protected int RunProcess(string processToRun, string args)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processToRun,
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

        private string EscapeMarkup(string text)
        {
            return text.Replace("[", "[[").Replace("]", "]]");
        }
    }
}
