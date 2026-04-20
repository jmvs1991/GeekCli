using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Dto.Models
{
    public sealed record DotnetDtoCommand(string Name, string ProjectName, DotnetScope Scope, bool View)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
