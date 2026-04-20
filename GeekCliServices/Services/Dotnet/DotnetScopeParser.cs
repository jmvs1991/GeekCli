using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet
{
    public static class DotnetScopeParser
    {
        public static DotnetScope Parse(string? scope)
        {
            var normalized = scope?.Trim().ToLowerInvariant() ?? "basic";

            return normalized switch
            {
                "basic" => DotnetScope.Basic,
                "corp" => DotnetScope.Corp,
                "corp-co" => DotnetScope.CorpCo,
                "corp-co-code" => DotnetScope.CorpCoCode,
                _ => throw new ArgumentOutOfRangeException(nameof(scope), scope, "Unsupported scope.")
            };
        }
    }
}
