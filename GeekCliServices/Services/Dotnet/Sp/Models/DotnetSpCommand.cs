using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Sp.Models
{
    public sealed record DotnetSpCommand(string Name, string ProjectName, DotnetScope Scope)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
