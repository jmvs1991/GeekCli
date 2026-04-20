using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Write.Models
{
    public sealed record DotnetWriteCommand(string Name, string DbSchema, string ContextName)
        : DotnetTemplateCommandBase(Name);
}
