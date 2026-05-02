using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.UnitTestService.Models
{
    public sealed record DotnetUnitTestServiceCommand(string Name, string ProjectName, DotnetScope Scope)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
