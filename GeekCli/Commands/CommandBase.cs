using Spectre.Console.Cli;
using System.Diagnostics;

namespace GeekCli.Commands
{
    abstract class CommandBase : Command
    {
        protected int RunSubcommand(string args)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "geek-cli",
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            using var process = Process.Start(psi);
            process?.WaitForExit();
            return process?.ExitCode ?? 0;
        }
    }
}
