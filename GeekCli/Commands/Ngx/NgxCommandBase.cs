using Spectre.Console.Cli;

namespace GeekCli.Commands.Ngx
{
    abstract class NgxCommandBase<S> : CommandBase<S> where S : NgxSettingsBase
    {
        protected abstract string BuildArgs(S settings);

        public override int Execute(CommandContext context, S settings)
        {
            return RunProcess("cmd", $"/c ng {BuildArgs(settings)}");
        }
    }
}
