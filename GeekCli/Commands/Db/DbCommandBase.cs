using Spectre.Console.Cli;

namespace GeekCli.Commands.Db
{
    abstract class DbCommandBase<S> : CommandBase<S> where S : DbSettingBase
    {
        public override int Execute(CommandContext context, S settings)
        {
           return RunProcess("dotnet",BuildArgs(settings));
        }

        protected abstract string BuildArgs(S settings);
    }
}
