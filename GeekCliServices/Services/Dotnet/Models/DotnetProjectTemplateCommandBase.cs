namespace GeekCliServices.Services.Dotnet.Models
{
    public abstract record DotnetProjectTemplateCommandBase(string Name, string ProjectName, DotnetScope Scope)
        : DotnetTemplateCommandBase(Name);
}
