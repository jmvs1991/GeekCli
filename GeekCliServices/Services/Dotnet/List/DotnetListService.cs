using GeekCliServices.Services.Dotnet.List.Models;

namespace GeekCliServices.Services.Dotnet.List
{
    public sealed class DotnetListService : DotnetCommandServiceBase, IDotnetListService
    {
        public int RunProcess(string processToRun, DotnetListCommand command)
        {
            return RunDotnet("new list geek");
        }
    }
}
