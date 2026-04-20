using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Cache.Models;

namespace GeekCliServices.Services.Dotnet.Cache
{
    public sealed class DotnetCacheService : DotnetCommandServiceBase, IDotnetCacheService
    {
        public int RunProcess(string processToRun, DotnetCacheCommand command)
        {
            var shortName = $"geek-cache{GetScopeSuffix(command.Scope, allowCorpCoCode: false)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
