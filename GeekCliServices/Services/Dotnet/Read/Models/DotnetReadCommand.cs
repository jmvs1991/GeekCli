using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.Read.Models
{
    public sealed record DotnetReadCommand(string Name, string DbSchema, string ContextName, DotnetScope Scope, bool View)
        : DotnetDbTemplateCommandBase(Name, DbSchema, ContextName, Scope);
}
