using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Controller.Models
{
    public sealed record DotnetControllerCommand(string Name, string ProjectName, string CodeField, bool View)
        : DotnetTemplateCommandBase(Name);
}
