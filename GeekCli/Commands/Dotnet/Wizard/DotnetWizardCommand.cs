using GeekCliServices.Services.Dotnet.Cache;
using GeekCliServices.Services.Dotnet.Cache.Models;
using GeekCliServices.Services.Dotnet.Controller;
using GeekCliServices.Services.Dotnet.Controller.Models;
using GeekCliServices.Services.Dotnet.Dto;
using GeekCliServices.Services.Dotnet.Dto.Models;
using GeekCliServices.Services.Dotnet.List;
using GeekCliServices.Services.Dotnet.List.Models;
using GeekCliServices.Services.Dotnet.Models;
using GeekCliServices.Services.Dotnet.Read;
using GeekCliServices.Services.Dotnet.Read.Models;
using GeekCliServices.Services.Dotnet.Resource;
using GeekCliServices.Services.Dotnet.Resource.Models;
using GeekCliServices.Services.Dotnet.Service;
using GeekCliServices.Services.Dotnet.Service.Models;
using GeekCliServices.Services.Dotnet.Sp;
using GeekCliServices.Services.Dotnet.Sp.Models;
using GeekCliServices.Services.Dotnet.ApiUnitTest;
using GeekCliServices.Services.Dotnet.ApiUnitTest.Models;
using GeekCliServices.Services.Dotnet.Write;
using GeekCliServices.Services.Dotnet.Write.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Dotnet.Wizard
{
    internal sealed class DotnetWizardCommand : Command, IDotnetWizard
    {
        private const string ProcessToRun = "dotnet";
        private const string BackAction = "Back";
        private const string ListTemplatesAction = "List installed Geek templates";
        private const string CreateDtoAction = "Create a DTO or View DTO";
        private const string CreateResourceAction = "Create a resource";
        private const string CreateCacheAction = "Create a cache";
        private const string CreateSpAction = "Create stored procedure models";
        private const string CreateReadAction = "Create a read repository";
        private const string CreateWriteAction = "Create a write repository";
        private const string CreateControllerAction = "Create an API controller";
        private const string CreateServiceAction = "Create a service";
        private const string CreateUnitTestAction = "Create an API unit test";

        private readonly IDotnetListService _dotnetListService;
        private readonly IDotnetDtoService _dotnetDtoService;
        private readonly IDotnetResourceService _dotnetResourceService;
        private readonly IDotnetCacheService _dotnetCacheService;
        private readonly IDotnetSpService _dotnetSpService;
        private readonly IDotnetReadService _dotnetReadService;
        private readonly IDotnetWriteService _dotnetWriteService;
        private readonly IDotnetControllerService _dotnetControllerService;
        private readonly IDotnetServiceService _dotnetServiceService;
        private readonly IDotnetApiUnitTestService _dotnetApiUnitTestService;

        public DotnetWizardCommand(IDotnetListService dotnetListService,
                                   IDotnetDtoService dotnetDtoService,
                                   IDotnetResourceService dotnetResourceService,
                                   IDotnetCacheService dotnetCacheService,
                                   IDotnetSpService dotnetSpService,
                                   IDotnetReadService dotnetReadService,
                                   IDotnetWriteService dotnetWriteService,
                                   IDotnetControllerService dotnetControllerService,
                                   IDotnetServiceService dotnetServiceService,
                                   IDotnetApiUnitTestService dotnetApiUnitTestService)
        {
            _dotnetListService = dotnetListService;
            _dotnetDtoService = dotnetDtoService;
            _dotnetResourceService = dotnetResourceService;
            _dotnetCacheService = dotnetCacheService;
            _dotnetSpService = dotnetSpService;
            _dotnetReadService = dotnetReadService;
            _dotnetWriteService = dotnetWriteService;
            _dotnetControllerService = dotnetControllerService;
            _dotnetServiceService = dotnetServiceService;
            _dotnetApiUnitTestService = dotnetApiUnitTestService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            ShowHeader();

            var action = AskAction(showBackOption);

            if (showBackOption && action == BackAction)
            {
                return 0;
            }

            return action switch
            {
                ListTemplatesAction => RunListTemplates(),
                CreateDtoAction => RunCreateDto(),
                CreateResourceAction => RunCreateResource(),
                CreateCacheAction => RunCreateCache(),
                CreateSpAction => RunCreateSp(),
                CreateReadAction => RunCreateRead(),
                CreateWriteAction => RunCreateWrite(),
                CreateControllerAction => RunCreateController(),
                CreateServiceAction => RunCreateService(),
                CreateUnitTestAction => RunCreateUnitTest(),
                _ => 1
            };
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green].NET Template Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Use this wizard to run the Geek .NET item templates with guided prompts.[/]");
            AnsiConsole.WriteLine();
        }

        private static string AskAction(bool showBackOption)
        {
            var choices = new List<string>
            {
                ListTemplatesAction,
                CreateDtoAction,
                CreateResourceAction,
                CreateCacheAction,
                CreateSpAction,
                CreateReadAction,
                CreateWriteAction,
                CreateControllerAction,
                CreateServiceAction,
                CreateUnitTestAction
            };

            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the .NET template task you want to run")
                    .PageSize(12)
                    .AddChoices(choices));
        }

        private static string AskName(string example)
        {
            return AnsiConsole.Ask<string>($"Template [green]name[/] ([grey]example: {example}[/])?");
        }

        private static string AskProjectName(string example = "Billing")
        {
            return AnsiConsole.Ask<string>($"[green]Project name[/] ([grey]example: {example}[/])?");
        }

        private static string AskDbSchema(string example = "Sales")
        {
            return AnsiConsole.Ask<string>($"Database [green]schema[/] ([grey]example: {example}[/])?");
        }

        private static string AskContextName(string example = "BillingContext")
        {
            return AnsiConsole.Ask<string>($"[green]DbContext name[/] ([grey]example: {example}[/])?");
        }

        private static string AskCodeField(string example = "CustomerCode")
        {
            return AnsiConsole.Ask<string>($"[green]Code field[/] ([grey]example: {example}[/])?");
        }

        private static string AskServiceInterface(string example = "ICustomerService")
        {
            return AnsiConsole.Ask<string>($"[green]Service interface[/] ([grey]example: {example}[/])?");
        }

        private static string AskDtoName(string example = "CustomerDTO")
        {
            return AnsiConsole.Ask<string>($"[green]DTO name[/] ([grey]example: {example}[/])?");
        }

        private static string AskResponseName(string example = "CustomerResponse")
        {
            return AnsiConsole.Ask<string>($"[green]Response name[/] ([grey]example: {example}[/])?");
        }

        private static string AskEndpoint(string example = "Customer")
        {
            return AnsiConsole.Ask<string>($"[green]Endpoint[/] ([grey]example: {example}[/])?");
        }

        private static DotnetScope AskScope(bool allowCorpCoCode)
        {
            var choices = new List<string>
            {
                "basic",
                "corp",
                "corp-co"
            };

            if (allowCorpCoCode)
            {
                choices.Add("corp-co-code");
            }

            var scope = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose the template [green]scope[/]")
                    .AddChoices(choices));

            return DotnetScopeHelper.Parse(scope);
        }

        private static void ShowSummary(params string[] lines)
        {
            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, lines))
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private static bool ConfirmExecution()
        {
            return AnsiConsole.Confirm("Run this [green]command[/] now?", true);
        }

        private int RunListTemplates()
        {
            ShowSummary($"[grey]Action:[/] [green]{ListTemplatesAction}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetListService.RunProcess(ProcessToRun, new DotnetListCommand());
        }

        private int RunCreateDto()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var view = AnsiConsole.Confirm("Generate a [green]View DTO[/]?", false);
            var scope = AskScope(allowCorpCoCode: false);

            ShowSummary($"[grey]Action:[/] [green]{CreateDtoAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]View:[/] [green]{(view ? "Yes" : "No")}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetDtoService.RunProcess(ProcessToRun, new DotnetDtoCommand(name, projectName, scope, view));
        }

        private int RunCreateResource()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var scope = AskScope(allowCorpCoCode: true);

            ShowSummary($"[grey]Action:[/] [green]{CreateResourceAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetResourceService.RunProcess(ProcessToRun, new DotnetResourceCommand(name, projectName, scope));
        }

        private int RunCreateCache()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var scope = AskScope(allowCorpCoCode: false);

            ShowSummary($"[grey]Action:[/] [green]{CreateCacheAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetCacheService.RunProcess(ProcessToRun, new DotnetCacheCommand(name, projectName, scope));
        }

        private int RunCreateSp()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var scope = AskScope(allowCorpCoCode: false);

            ShowSummary($"[grey]Action:[/] [green]{CreateSpAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetSpService.RunProcess(ProcessToRun, new DotnetSpCommand(name, projectName, scope));
        }

        private int RunCreateRead()
        {
            var name = AskName("Customer");
            var dbSchema = AskDbSchema();
            var contextName = AskContextName();
            var view = AnsiConsole.Confirm("Generate a [green]view repository[/]?", false);
            var scope = AskScope(allowCorpCoCode: true);

            ShowSummary($"[grey]Action:[/] [green]{CreateReadAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Schema:[/] [green]{dbSchema}[/]",
                        $"[grey]Context:[/] [green]{contextName}[/]",
                        $"[grey]View:[/] [green]{(view ? "Yes" : "No")}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetReadService.RunProcess(ProcessToRun, new DotnetReadCommand(name, dbSchema, contextName, scope, view));
        }

        private int RunCreateWrite()
        {
            var name = AskName("Customer");
            var dbSchema = AskDbSchema();
            var contextName = AskContextName();

            ShowSummary($"[grey]Action:[/] [green]{CreateWriteAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Schema:[/] [green]{dbSchema}[/]",
                        $"[grey]Context:[/] [green]{contextName}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetWriteService.RunProcess(ProcessToRun, new DotnetWriteCommand(name, dbSchema, contextName));
        }

        private int RunCreateController()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var codeField = AskCodeField();
            var view = AnsiConsole.Confirm("Generate a [green]view controller[/]?", false);

            ShowSummary($"[grey]Action:[/] [green]{CreateControllerAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]Code field:[/] [green]{codeField}[/]",
                        $"[grey]View:[/] [green]{(view ? "Yes" : "No")}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetControllerService.RunProcess(ProcessToRun, new DotnetControllerCommand(name, projectName, codeField, view));
        }

        private int RunCreateService()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var view = AnsiConsole.Confirm("Generate a [green]view service[/]?", false);
            var scope = AskScope(allowCorpCoCode: true);

            ShowSummary($"[grey]Action:[/] [green]{CreateServiceAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]View:[/] [green]{(view ? "Yes" : "No")}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetServiceService.RunProcess(ProcessToRun, new DotnetServiceCommand(name, projectName, scope, view));
        }

        private int RunCreateUnitTest()
        {
            var name = AskName("Customer");
            var projectName = AskProjectName();
            var codeField = AskCodeField();
            var serviceInterface = AskServiceInterface();
            var dtoName = AskDtoName();
            var responseName = AskResponseName();
            var endpoint = AskEndpoint();
            var scope = AskScope(allowCorpCoCode: false);

            ShowSummary($"[grey]Action:[/] [green]{CreateUnitTestAction}[/]",
                        $"[grey]Name:[/] [green]{name}[/]",
                        $"[grey]Project:[/] [green]{projectName}[/]",
                        $"[grey]Code field:[/] [green]{codeField}[/]",
                        $"[grey]Service interface:[/] [green]{serviceInterface}[/]",
                        $"[grey]DTO name:[/] [green]{dtoName}[/]",
                        $"[grey]Response name:[/] [green]{responseName}[/]",
                        $"[grey]Endpoint:[/] [green]{endpoint}[/]",
                        $"[grey]Scope:[/] [green]{scope}[/]");

            if (!ConfirmExecution())
            {
                return 0;
            }

            return _dotnetApiUnitTestService.RunProcess(ProcessToRun,
                new DotnetApiUnitTestCommand(name,
                                             projectName,
                                             codeField,
                                             serviceInterface,
                                             dtoName,
                                             responseName,
                                             endpoint,
                                             scope));
        }
    }
}
