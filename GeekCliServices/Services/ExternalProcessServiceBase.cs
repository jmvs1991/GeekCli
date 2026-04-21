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
                CommandOutput.Info(line);
            }

            while (!process.StandardError.EndOfStream)
            {
                var error = process.StandardError.ReadLine();
                CommandOutput.Error(error);
            }

            process.WaitForExit();

            return process.ExitCode;
        }

        protected abstract string BuildArgs(TCommand command);
    }
}
