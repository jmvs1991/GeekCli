using GeekCliServices.Services.EditorConfig;
using GeekCliServices.Services.EditorConfig.Models;
using Spectre.Console.Cli;

namespace GeekCli.Commands.EditorConfig
{
    internal sealed class AddEditorConfigCommand : Command<EditorConfigSettings>
    {
        private readonly IEditorConfigService _service;

        public AddEditorConfigCommand(IEditorConfigService service)
        {
            _service = service;
        }

        protected override int Execute(CommandContext context, EditorConfigSettings settings, CancellationToken cancellationToken)
        {
            return _service.RunProcess("editorconfig", new EditorConfigCommand(settings.ProjectPath, settings.ProjectType));
        }
    }
}
