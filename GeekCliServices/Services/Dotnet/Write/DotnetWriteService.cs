using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Write.Models;

namespace GeekCliServices.Services.Dotnet.Write
{
    public sealed class DotnetWriteService : DotnetCommandServiceBase, IDotnetWriteService
    {
        public int RunProcess(string processToRun, DotnetWriteCommand command)
        {
            return RunDotnet(BuildTemplateArgs("geek-write-storeprocedureful-transactionless",
                                               command.Name,
                                               DotnetTemplateOption.DbSchema(command.DbSchema),
                                               DotnetTemplateOption.ContextName(command.ContextName)));
        }
    }
}
