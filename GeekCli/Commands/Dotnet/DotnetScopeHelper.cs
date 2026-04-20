using GeekCliServices.Services.Dotnet;
using GeekCliServices.Services.Dotnet.Models;

namespace GeekCli.Commands.Dotnet
{
    internal static class DotnetScopeHelper
    {
        public static string Normalize(string? scope)
        {
            return scope is null ? "basic" : scope.Trim().ToLowerInvariant();
        }

        public static DotnetScope Parse(string? scope)
        {
            return DotnetScopeParser.Parse(scope);
        }
    }
}
