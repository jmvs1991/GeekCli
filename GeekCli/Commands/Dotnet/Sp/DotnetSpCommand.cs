using GeekCliServices.Services.Dotnet.Sp;
using DotnetSpSpec = GeekCliServices.Services.Dotnet.Sp.Models.DotnetSpCommand;

namespace GeekCli.Commands.Dotnet.Sp
{
    internal sealed class DotnetSpCommand : DotnetCommandExecutorBase<DotnetSpSettings, IDotnetSpService, DotnetSpSpec>
    {
        public DotnetSpCommand(IDotnetSpService service) : base(service)
        {
        }

        protected override DotnetSpSpec MapToCommand(DotnetSpSettings settings)
        {
            return new DotnetSpSpec(settings.Name!,
                                    settings.ProjectName!,
                                    DotnetScopeHelper.Parse(settings.Scope));
        }

        protected override int ExecuteCommand(IDotnetSpService service, string processToRun, DotnetSpSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
