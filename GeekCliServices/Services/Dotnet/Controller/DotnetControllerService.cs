using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Controller.Models;

namespace GeekCliServices.Services.Dotnet.Controller
{
    public sealed class DotnetControllerService : DotnetCommandServiceBase, IDotnetControllerService
    {
        public int RunProcess(string processToRun, DotnetControllerCommand command)
        {
            var shortName = command.View
                ? "geek-view-api-paginationful-filterless"
                : "geek-api-paginationful-filterless";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.ProjectName(command.ProjectName),
                                               DotnetTemplateOption.CodeField(command.CodeField)));
        }
    }
}
