using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Rx
{
    abstract class RxCommandBase<S> : CommandBase<S> where S : RxSettingBase
    {
        public override int Execute(CommandContext context, S settings)
        {
            string name = _textInfo.ToTitleCase(settings.Name.ToLower());
            string targetPath = "";

            if (!settings.Flat)
            {
                targetPath = _basePath;
                CreateDirectory(targetPath);
            }
            else
            {
                targetPath = Path.Combine(_basePath, settings.Name);
            }

            Execute(targetPath, name, settings);

            AnsiConsole.MarkupLine($"[bold green]✔ Files created successfully in:[/] [blue]{targetPath}[/]");
            
            return 0;
        }

        protected abstract void Execute(string targetPath, string name, S settings);
    }
}
