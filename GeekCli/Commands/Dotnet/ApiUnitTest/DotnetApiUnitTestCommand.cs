using GeekCliServices.Services.Dotnet.ApiUnitTest;
using DotnetApiUnitTestSpec = GeekCliServices.Services.Dotnet.ApiUnitTest.Models.DotnetApiUnitTestCommand;

namespace GeekCli.Commands.Dotnet.ApiUnitTest
{
    internal sealed class DotnetApiUnitTestCommand : DotnetCommandExecutorBase<DotnetApiUnitTestSettings, IDotnetApiUnitTestService, DotnetApiUnitTestSpec>
    {
        public DotnetApiUnitTestCommand(IDotnetApiUnitTestService service) : base(service)
        {
        }

        protected override DotnetApiUnitTestSpec MapToCommand(DotnetApiUnitTestSettings settings)
        {
            return new DotnetApiUnitTestSpec(settings.Name!,
                                             settings.ProjectName!,
                                             settings.CodeField!,
                                             settings.ServiceInterface!,
                                             settings.DtoName!,
                                             settings.ResponseName!,
                                             settings.Endpoint!,
                                             DotnetScopeHelper.Parse(settings.Scope));
        }

        protected override int ExecuteCommand(IDotnetApiUnitTestService service, string processToRun, DotnetApiUnitTestSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
