using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Resource.Models;

namespace GeekCliServices.Services.Dotnet.Resource
{
    public sealed class DotnetResourceService : DotnetCommandServiceBase, IDotnetResourceService
    {
        public int RunProcess(string processToRun, DotnetResourceCommand command)
        {
            var shortName = $"geek-resource{GetScopeSuffix(command.Scope, allowCorpCoCode: true)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
