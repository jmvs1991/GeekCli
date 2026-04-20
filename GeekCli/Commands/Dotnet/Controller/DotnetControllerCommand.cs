using GeekCliServices.Services.Dotnet.Controller;
using DotnetControllerSpec = GeekCliServices.Services.Dotnet.Controller.Models.DotnetControllerCommand;

namespace GeekCli.Commands.Dotnet.Controller
{
    internal sealed class DotnetControllerCommand : DotnetCommandExecutorBase<DotnetControllerSettings, IDotnetControllerService, DotnetControllerSpec>
    {
        public DotnetControllerCommand(IDotnetControllerService service) : base(service)
        {
        }

        protected override DotnetControllerSpec MapToCommand(DotnetControllerSettings settings)
        {
            return new DotnetControllerSpec(settings.Name!,
                                            settings.ProjectName!,
                                            settings.CodeField!,
                                            settings.View);
        }

        protected override int ExecuteCommand(IDotnetControllerService service, string processToRun, DotnetControllerSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
