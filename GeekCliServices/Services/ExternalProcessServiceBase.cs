using Spectre.Console;
using System.Diagnostics;

namespace GeekCliServices.Services
{
    public abstract class ExternalProcessServiceBase<TCommand>
    {

        public int RunProcess(string processToRun, TCommand command)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = processToRun,
                    Arguments = BuildArgs(command),
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
        
        protected abstract string BuildArgs(TCommand command);
    }
}
