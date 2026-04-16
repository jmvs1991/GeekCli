using GeekCliServices.Services.Rx.Models;
using Spectre.Console;

namespace GeekCliServices.Services.Rx
{
    public abstract class RxServiceBase<TCommand> : ScaffoldingServiceBase<TCommand> where TCommand : RxCommand
    {
        public override int RunProcess(string processToRun, TCommand command)
        {
            string name = _textInfo.ToTitleCase(command.Name.ToLower());
            string targetPath = "";

            if (!command.Flat)
            {
                targetPath = _basePath;
                CreateDirectory(targetPath);
            }
            else
            {
                targetPath = Path.Combine(_basePath, command.Name);
            }

            Execute(targetPath, name, command);

            AnsiConsole.MarkupLine($"[bold green]✔ Files created successfully in:[/] [blue]{targetPath}[/]");

            return 0;
        }
    }
}
