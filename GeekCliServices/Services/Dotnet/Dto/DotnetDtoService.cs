using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Dto.Models;

namespace GeekCliServices.Services.Dotnet.Dto
{
    public sealed class DotnetDtoService : DotnetCommandServiceBase, IDotnetDtoService
    {
        public int RunProcess(string processToRun, DotnetDtoCommand command)
        {
            var prefix = command.View ? "geek-view-dto" : "geek-dto";
            var shortName = $"{prefix}{GetScopeSuffix(command.Scope, allowCorpCoCode: false)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName)));
        }
    }
}
