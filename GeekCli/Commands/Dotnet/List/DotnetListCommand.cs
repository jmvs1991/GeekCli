using GeekCliServices.Services.Dotnet.List;
using DotnetListSpec = GeekCliServices.Services.Dotnet.List.Models.DotnetListCommand;

namespace GeekCli.Commands.Dotnet.List
{
    internal sealed class DotnetListCommand : DotnetCommandExecutorBase<DotnetListSettings, IDotnetListService, DotnetListSpec>
    {
        public DotnetListCommand(IDotnetListService service) : base(service)
        {
        }

        protected override DotnetListSpec MapToCommand(DotnetListSettings settings)
        {
            return new DotnetListSpec();
        }

        protected override int ExecuteCommand(IDotnetListService service, string processToRun, DotnetListSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
