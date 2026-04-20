using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Service.Models
{
    public sealed record DotnetServiceCommand(string Name, string ProjectName, DotnetScope Scope, bool View)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
