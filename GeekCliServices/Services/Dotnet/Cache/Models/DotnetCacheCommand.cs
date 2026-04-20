using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Cache.Models
{
    public sealed record DotnetCacheCommand(string Name, string ProjectName, DotnetScope Scope)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
