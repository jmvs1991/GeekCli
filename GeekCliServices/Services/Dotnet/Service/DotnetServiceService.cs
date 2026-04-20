using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Service.Models;

namespace GeekCliServices.Services.Dotnet.Service
{
    public sealed class DotnetServiceService : DotnetCommandServiceBase, IDotnetServiceService
    {
        public int RunProcess(string processToRun, DotnetServiceCommand command)
        {
            var source = command.View ? "view" : "entity";
            var shortName = $"geek-{source}-service-paginationful-filterless{GetScopeSuffix(command.Scope, allowCorpCoCode: true)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
