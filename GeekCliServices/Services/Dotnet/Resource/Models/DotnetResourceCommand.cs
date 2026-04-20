using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Resource.Models
{
    public sealed record DotnetResourceCommand(string Name, string ProjectName, DotnetScope Scope)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
