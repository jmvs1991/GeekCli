using GeekCliServices.Services;
using Spectre.Console.Cli;

namespace GeekCli.Commands
{
    abstract class CommandBase<TSettings, TService, TCommand> : Command<TSettings> where TSettings : CommandSettings
                                                                                   where TService : ICommandService<TCommand>
    {
        protected readonly TService _service;

        private readonly string _processToRun;

        public CommandBase(TService service, string processToRun)
        {
            _service = service;
            _processToRun = processToRun;
        }

        protected override int Execute(CommandContext context, TSettings settings, CancellationToken cancellationToken)
        {
            return _service.RunProcess(_processToRun, MapToCommand(settings));
        }

        protected abstract TCommand MapToCommand(TSettings settings);

    }
}
