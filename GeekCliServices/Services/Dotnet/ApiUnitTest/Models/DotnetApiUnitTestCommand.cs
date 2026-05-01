using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet.ApiUnitTest.Models
{
    public sealed record DotnetApiUnitTestCommand(string Name,
                                                  string ProjectName,
                                                  string CodeField,
                                                  string ServiceInterface,
                                                  string DtoName,
                                                  string ResponseName,
                                                  string Endpoint,
                                                  DotnetScope Scope)
        : DotnetProjectTemplateCommandBase(Name, ProjectName, Scope);
}
