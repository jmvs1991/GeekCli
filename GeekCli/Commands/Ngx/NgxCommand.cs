using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics;

namespace GeekCli.Commands.Ngx
{
    class NgxCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            //var action = AnsiConsole.Prompt(
            //        new SelectionPrompt<string>()
            //            .Title("What Angular action would you like to perform?")
            //            .PageSize(10)
            //            .AddChoices(new[]
            //            {
            //                "Create new Angular project",
            //                "Generate component",
            //                "Generate module",
            //                "Generate service",
            //                "Back"
            //            }));

            //switch (action)
            //{
            //    case "Create new Angular project":
            //        var projectName = AnsiConsole.Ask<string>("Enter project name:");
            //        RunNgCommand($"new {projectName} --routing --style=scss --standalone=false");
            //        break;

            //    case "Generate component":
            //        var componentName = AnsiConsole.Ask<string>("Component name:");
            //        RunNgCommand($"generate component {componentName}");
            //        break;

            //    case "Generate module":
            //        var moduleName = AnsiConsole.Ask<string>("Module name:");
            //        RunNgCommand($"generate module {moduleName}");
            //        break;

            //    case "Generate service":
            //        var serviceName = AnsiConsole.Ask<string>("Service name:");
            //        RunNgCommand($"generate service {serviceName}");
            //        break;

            //    case "Back":
            //        return 0;
            //}

            return 0;
        }

        private void RunNgCommand(string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "ng",
                    Arguments = args,
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Failed to run ng command:[/] {ex.Message}");
            }
        }
    }
}
