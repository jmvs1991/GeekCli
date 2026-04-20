using GeekCliServices.Services.Dotnet.Resource;
using DotnetResourceSpec = GeekCliServices.Services.Dotnet.Resource.Models.DotnetResourceCommand;

namespace GeekCli.Commands.Dotnet.Resource
{
    internal sealed class DotnetResourceCommand : DotnetCommandExecutorBase<DotnetResourceSettings, IDotnetResourceService, DotnetResourceSpec>
    {
        public DotnetResourceCommand(IDotnetResourceService service) : base(service)
        {
        }

        protected override DotnetResourceSpec MapToCommand(DotnetResourceSettings settings)
        {
            return new DotnetResourceSpec(settings.Name!,
                                          settings.ProjectName!,
                                          DotnetScopeHelper.Parse(settings.Scope));
        }

        protected override int ExecuteCommand(IDotnetResourceService service, string processToRun, DotnetResourceSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
