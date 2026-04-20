using GeekCliServices.Services.Dotnet.Write;
using DotnetWriteSpec = GeekCliServices.Services.Dotnet.Write.Models.DotnetWriteCommand;

namespace GeekCli.Commands.Dotnet.Write
{
    internal sealed class DotnetWriteCommand : DotnetCommandExecutorBase<DotnetWriteSettings, IDotnetWriteService, DotnetWriteSpec>
    {
        public DotnetWriteCommand(IDotnetWriteService service) : base(service)
        {
        }

        protected override DotnetWriteSpec MapToCommand(DotnetWriteSettings settings)
        {
            return new DotnetWriteSpec(settings.Name!,
                                       settings.DbSchema!,
                                       settings.ContextName!);
        }

        protected override int ExecuteCommand(IDotnetWriteService service, string processToRun, DotnetWriteSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
