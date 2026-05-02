using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.ApiUnitTest.Models;

namespace GeekCliServices.Services.Dotnet.ApiUnitTest
{
    public sealed class DotnetApiUnitTestService : DotnetCommandServiceBase, IDotnetApiUnitTestService
    {
        public int RunProcess(string processToRun, DotnetApiUnitTestCommand command)
        {
            var shortName = $"geek-unittest-api-paginationful-filterless{GetScopeSuffix(command.Scope, allowCorpCoCode: false)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName),
                                               DotnetTemplateOption.CodeField(command.CodeField),
                                               DotnetTemplateOption.ServiceInterface(command.ServiceInterface),
                                               DotnetTemplateOption.DtoName(command.DtoName),
                                               DotnetTemplateOption.ResponseName(command.ResponseName),
                                               DotnetTemplateOption.ContextTestBase(command.ContextTestBase),
                                               DotnetTemplateOption.Endpoint(command.Endpoint)));
        }
    }
}
