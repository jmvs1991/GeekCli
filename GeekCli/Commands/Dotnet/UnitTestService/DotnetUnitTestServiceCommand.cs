using GeekCliServices.Services.Dotnet.UnitTestService;
using DotnetUnitTestServiceSpec = GeekCliServices.Services.Dotnet.UnitTestService.Models.DotnetUnitTestServiceCommand;

namespace GeekCli.Commands.Dotnet.UnitTestService
{
    internal sealed class DotnetUnitTestServiceCommand : DotnetCommandExecutorBase<DotnetUnitTestServiceSettings, IDotnetUnitTestServiceService, DotnetUnitTestServiceSpec>
    {
        public DotnetUnitTestServiceCommand(IDotnetUnitTestServiceService service) : base(service)
        {
        }

        protected override DotnetUnitTestServiceSpec MapToCommand(DotnetUnitTestServiceSettings settings)
        {
            return new DotnetUnitTestServiceSpec(settings.Name!,
                                                 settings.ProjectName!,
                                                 DotnetScopeHelper.Parse(settings.Scope));
        }

        protected override int ExecuteCommand(IDotnetUnitTestServiceService service, string processToRun, DotnetUnitTestServiceSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
