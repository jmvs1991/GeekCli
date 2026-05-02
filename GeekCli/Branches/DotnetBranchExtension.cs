using GeekCli.Commands.Dotnet.Cache;
using GeekCli.Commands.Dotnet.Controller;
using GeekCli.Commands.Dotnet.Dto;
using GeekCli.Commands.Dotnet.List;
using GeekCli.Commands.Dotnet.Read;
using GeekCli.Commands.Dotnet.Resource;
using GeekCli.Commands.Dotnet.Service;
using GeekCli.Commands.Dotnet.Sp;
using GeekCli.Commands.Dotnet.ApiUnitTest;
using GeekCli.Commands.Dotnet.UnitTestService;
using GeekCli.Commands.Dotnet.Wizard;
using GeekCli.Commands.Dotnet.Write;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class DotnetBranchExtension
    {
        public static IBranchConfigurator AddDotnet(this IConfigurator configurator)
        {
            return configurator.AddBranch("dotnet", dotnet =>
            {
                dotnet.SetDescription(".NET template pack utilities");
                dotnet.SetDefaultCommand<DotnetWizardCommand>();

                dotnet.AddCommand<DotnetListCommand>("list")
                      .WithDescription("Lists the installed Geek .NET templates.");

                dotnet.AddCommand<DotnetDtoCommand>("dto")
                      .WithDescription("Generates a DTO or View DTO template.");

                dotnet.AddCommand<DotnetResourceCommand>("resource")
                      .WithDescription("Generates a resource template.");

                dotnet.AddCommand<DotnetCacheCommand>("cache")
                      .WithDescription("Generates a cache template.");

                dotnet.AddCommand<DotnetSpCommand>("sp")
                      .WithDescription("Generates stored procedure model templates.");

                dotnet.AddCommand<DotnetReadCommand>("read")
                      .WithDescription("Generates a paginated, filterless read repository template.");

                dotnet.AddCommand<DotnetWriteCommand>("write")
                      .WithDescription("Generates a write repository template.");

                dotnet.AddCommand<DotnetControllerCommand>("controller")
                      .WithDescription("Generates an API controller template.");

                dotnet.AddCommand<DotnetServiceCommand>("service")
                      .WithDescription("Generates an entity or view service template.");

                dotnet.AddCommand<DotnetUnitTestServiceCommand>("unittest-service")
                      .WithDescription("Generates a service unit test template.");

                dotnet.AddCommand<DotnetApiUnitTestCommand>("unittest-api")
                      .WithDescription("Generates an API unit test template.");
            });
        }
    }
}
