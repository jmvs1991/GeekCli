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
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.StandardInput.Close();

            var standardOutputTask = Task.Run(() => ReadLines(process.StandardOutput, CommandOutput.Info));
            var standardErrorTask = Task.Run(() => ReadLines(process.StandardError, CommandOutput.Error));

            process.WaitForExit();
            Task.WaitAll(standardOutputTask, standardErrorTask);

            return process.ExitCode;
        }

        private static void ReadLines(StreamReader reader, Action<string?> write)
        {
            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                write(line);
            }
        }

        protected abstract string BuildArgs(TCommand command);
    }
}
