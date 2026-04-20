namespace GeekCliServices.Services.Dotnet.Models
{
    public abstract record DotnetDbTemplateCommandBase(string Name, string DbSchema, string ContextName, DotnetScope Scope)
        : DotnetTemplateCommandBase(Name);
}
