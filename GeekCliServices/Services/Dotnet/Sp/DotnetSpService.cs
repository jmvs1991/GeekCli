using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Sp.Models;

namespace GeekCliServices.Services.Dotnet.Sp
{
    public sealed class DotnetSpService : DotnetCommandServiceBase, IDotnetSpService
    {
        public int RunProcess(string processToRun, DotnetSpCommand command)
        {
            var shortName = $"geek-sp{GetScopeSuffix(command.Scope, allowCorpCoCode: false)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
