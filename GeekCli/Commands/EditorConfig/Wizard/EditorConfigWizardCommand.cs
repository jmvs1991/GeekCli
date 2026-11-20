using GeekCliServices.Services.EditorConfig;
using GeekCliServices.Services.EditorConfig.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.EditorConfig.Wizard
{
    internal sealed class EditorConfigWizardCommand : Command, IEditorConfigWizard
    {
        private const string BackOption = "Back";
        private readonly IEditorConfigService _service;

        public EditorConfigWizardCommand(IEditorConfigService service)
        {
            _service = service;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            var prompt = new SelectionPrompt<string>()
                .Title("Choose the project type for .editorconfig")
                .AddChoices(EditorConfigCommand.CSharp, EditorConfigCommand.Angular, EditorConfigCommand.React);

            if (showBackOption)
            {
                prompt.AddChoice(BackOption);
            }

            var projectType = AnsiConsole.Prompt(prompt);
            if (projectType == BackOption)
            {
                return 0;
            }

            var projectPath = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Project directory[/]?")
                    .DefaultValue(EditorConfigCommand.DefaultProjectPath));

            AnsiConsole.Write(new Panel(
                $"[grey]Project type:[/] [green]{projectType}[/]{Environment.NewLine}" +
                $"[grey]Directory:[/] [green]{Markup.Escape(projectPath)}[/]")
                .Header("Add .editorconfig")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));

            if (!AnsiConsole.Confirm("Add .editorconfig now?", true))
            {
                return 0;
            }

            return _service.RunProcess("editorconfig", new EditorConfigCommand(projectPath, projectType));
        }
    }
}
