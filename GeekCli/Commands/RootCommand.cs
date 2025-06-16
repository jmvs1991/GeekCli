using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands
{
    class RootCommand : CommandBase
    {
        private readonly List<(string label, string? command)> options = new List<(string label, string? command)>
            {
                //("Database", "db"),
                ("Angular", "ngx"),
                //("React Web", "rx web"),
                //("React Native", "rx native"),
                //(".NET", "dotnet"),
                //("Check Environment", "check env"),
                ("Exit", null)
            };

        public override int Execute(CommandContext context)
        {
            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to do?")
                    .PageSize(10)
                    .AddChoices(options.Select(opt => opt.label)));

            var selectedOption = options.Find(o => o.label == selected);

            if (selectedOption.command == null)
            {
                AnsiConsole.MarkupLine("[grey]Exiting...[/]");
                return 0;
            }

            return RunSubcommand(selectedOption.command);
        }
    }

}
