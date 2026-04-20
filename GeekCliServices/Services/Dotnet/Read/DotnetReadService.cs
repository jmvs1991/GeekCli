using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Read.Models;

namespace GeekCliServices.Services.Dotnet.Read
{
    public sealed class DotnetReadService : DotnetCommandServiceBase, IDotnetReadService
    {
        public int RunProcess(string processToRun, DotnetReadCommand command)
        {
            var source = command.View ? "view" : "entity";
            var shortName = $"geek-read-paginationful-filterless-{source}{GetScopeSuffix(command.Scope, allowCorpCoCode: true)}";

            return RunDotnet(BuildTemplateArgs(shortName,
                                               command.Name,
                                               DotnetTemplateOption.DbSchema(command.DbSchema),
                                               DotnetTemplateOption.ContextName(command.ContextName)));
        }
    }
}
