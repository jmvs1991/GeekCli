using GeekCliServices.Services.Dotnet.Service;
using DotnetServiceSpec = GeekCliServices.Services.Dotnet.Service.Models.DotnetServiceCommand;

namespace GeekCli.Commands.Dotnet.Service
{
    internal sealed class DotnetServiceCommand : DotnetCommandExecutorBase<DotnetServiceSettings, IDotnetServiceService, DotnetServiceSpec>
    {
        public DotnetServiceCommand(IDotnetServiceService service) : base(service)
        {
        }

        protected override DotnetServiceSpec MapToCommand(DotnetServiceSettings settings)
        {
            return new DotnetServiceSpec(settings.Name!,
                                         settings.ProjectName!,
                                         DotnetScopeHelper.Parse(settings.Scope),
                                         settings.View);
        }

        protected override int ExecuteCommand(IDotnetServiceService service, string processToRun, DotnetServiceSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
