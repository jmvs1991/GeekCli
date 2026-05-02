using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.UnitTestService.Models;

namespace GeekCliServices.Services.Dotnet.UnitTestService
{
    public sealed class DotnetUnitTestServiceService : DotnetCommandServiceBase, IDotnetUnitTestServiceService
    {
        public int RunProcess(string processToRun, DotnetUnitTestServiceCommand command)
        {
            var shortName = $"geek-unittest-entity-service-paginationful-filterless{GetScopeSuffix(command.Scope, allowCorpCoCode: false)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
