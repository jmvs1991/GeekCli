using GeekCliServices.Services;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Dotnet
{
    internal abstract class DotnetCommandExecutorBase<TSettings, TService, TCommand> : Command<TSettings>
        where TSettings : CommandSettings
        where TService : ICommandService<TCommand>
    {
        protected readonly TService Service;

        protected DotnetCommandExecutorBase(TService service)
        {
            Service = service;
        }

        protected sealed override int Execute(CommandContext context, TSettings settings, CancellationToken cancellationToken)
        {
            var command = MapToCommand(settings);
            return ExecuteCommand(Service, ProcessToRun, command);
        }

        protected abstract TCommand MapToCommand(TSettings settings);

        protected virtual string ProcessToRun => "dotnet";

        protected abstract int ExecuteCommand(TService service, string processToRun, TCommand command);
    }
}
