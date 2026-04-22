using GeekCliServices.Services.Rx.Common.Context;
using GeekCliServices.Services.Rx.Models;
using GeekCliServices.Services.Rx.Native.Component;
using GeekCliServices.Services.Rx.Native.Module;
using GeekCliServices.Services.Rx.Native.Screen;
using GeekCliServices.Services.Rx.Native.Screen.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Rx.Wizard
{
    internal sealed class RxWizardCommand : Command, IRxWizard
    {
        private const string BackAction = "Back";
        private const string CreateContextAction = "Create a React context";
        private const string CreateNativeModuleAction = "Create a React Native module";
        private const string CreateNativeScreenAction = "Create a React Native screen";
        private const string CreateNativeComponentAction = "Create a React Native component";

        private readonly IRxContextService _rxContextService;
        private readonly IRxNativeModuleService _rxNativeModuleService;
        private readonly IRxNativeScreenService _rxNativeScreenService;
        private readonly IRxNativeComponentService _rxNativeComponentService;

        public RxWizardCommand(IRxContextService rxContextService,
                               IRxNativeModuleService rxNativeModuleService,
                               IRxNativeScreenService rxNativeScreenService,
                               IRxNativeComponentService rxNativeComponentService)
        {
            _rxContextService = rxContextService;
            _rxNativeModuleService = rxNativeModuleService;
            _rxNativeScreenService = rxNativeScreenService;
            _rxNativeComponentService = rxNativeComponentService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            ShowHeader();

            var choices = new List<string>
            {
                CreateContextAction,
                CreateNativeModuleAction,
                CreateNativeScreenAction,
                CreateNativeComponentAction
            };

            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose what you want to generate")
                    .PageSize(10)
                    .AddChoices(choices));

            if (showBackOption && action == BackAction)
            {
                return 0;
            }

            return action switch
            {
                CreateContextAction => RunCreateContext(),
                CreateNativeModuleAction => RunCreateNativeModule(),
                CreateNativeScreenAction => RunCreateNativeScreen(),
                CreateNativeComponentAction => RunCreateNativeComponent(),
                _ => 1
            };
        }

        private static void ShowHeader()
        {
            AnsiConsole.Write(new Rule("[green]React Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]This wizard helps you scaffold React and React Native building blocks using the existing templates.[/]");
            AnsiConsole.WriteLine();
        }

        private static string AskName()
        {
            return AnsiConsole.Ask<string>("React [green]name[/] ([grey]example: user-profile[/])?");
        }

        private static bool AskFlat()
        {
            return AnsiConsole.Confirm("Create it as [green]flat[/] ([grey]no nested folder[/])?", false);
        }

        private static void ShowSummary(string action, string name, bool flat, bool? schema = null, bool? wrapper = null)
        {
            var summary = new List<string>
            {
                $"[grey]Action:[/] [green]{action}[/]",
                $"[grey]Name:[/] [green]{name}[/]",
                $"[grey]Flat:[/] [green]{(flat ? "Yes" : "No")}[/]"
            };

            if (schema.HasValue)
            {
                summary.Add($"[grey]Schema file:[/] [green]{(schema.Value ? "Yes" : "No")}[/]");
            }

            if (wrapper.HasValue)
            {
                summary.Add($"[grey]Wrapper file:[/] [green]{(wrapper.Value ? "Yes" : "No")}[/]");
            }

            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summary))
                .Header("Ready to run")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }

        private int RunCreateContext()
        {
            var name = AskName();
            var flat = AskFlat();
            ShowSummary(CreateContextAction, name, flat);

            var command = new RxCommand(name, flat);
            return _rxContextService.RunProcess(string.Empty, command);
        }

        private int RunCreateNativeModule()
        {
            var name = AskName();
            var flat = AskFlat();
            ShowSummary(CreateNativeModuleAction, name, flat);

            var command = new RxCommand(name, flat);
            return _rxNativeModuleService.RunProcess(string.Empty, command);
        }

        private int RunCreateNativeScreen()
        {
            var name = AskName();
            var flat = AskFlat();
            var schema = AnsiConsole.Confirm("Generate a [green]schema[/] file?", false);
            var wrapper = AnsiConsole.Confirm("Generate a [green]wrapper[/] file?", false);
            ShowSummary(CreateNativeScreenAction, name, flat, schema, wrapper);

            var command = new RxScreenCommand(name, flat, schema, wrapper);
            return _rxNativeScreenService.RunProcess(string.Empty, command);
        }

        private int RunCreateNativeComponent()
        {
            var name = AskName();
            var flat = AskFlat();
            ShowSummary(CreateNativeComponentAction, name, flat);

            var command = new RxCommand(name, flat);
            return _rxNativeComponentService.RunProcess(string.Empty, command);
        }
    }
}
