using GeekCliServices.Services.Ai.Skills;
using GeekCliServices.Services.Ai.Skills.Models;
using GeekCliServices.ExtensionMethods;
using GeekCliServices.Services.Ai.Agents;
using GeekCliServices.Services.Ai.Agents.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Ai.Wizard
{
    internal sealed class AiWizardCommand : Command, IAiWizard
    {
        private const string BackAction = "Back";
        private const string ProjectScope = "Project";
        private const string UserScope = "User";
        private const string SkillsOption = "Install skills";
        private const string AllSkillsOption = "Install all missing skills";
        private const string UpdateSkillsOption = "Update all installed catalog skills";
        private const string AgentsOption = "Install subagents";

        private readonly ISkillInstallService _skillInstallService;
        private readonly IAgentInstallService _agentInstallService;

        public AiWizardCommand(ISkillInstallService skillInstallService, IAgentInstallService agentInstallService)
        {
            _skillInstallService = skillInstallService;
            _agentInstallService = agentInstallService;
        }

        protected override int Execute(CommandContext context, CancellationToken cancellationToken)
        {
            return RunWizard();
        }

        public int RunWizard(bool showBackOption = false)
        {
            AnsiConsole.Write(new Rule("[green]AI Wizard[/]").RuleStyle("grey"));
            AnsiConsole.MarkupLine("[grey]Install a bundled skill or subagent for the current project or your user profile.[/]");
            AnsiConsole.WriteLine();

            var operations = new List<string> { SkillsOption, AllSkillsOption, UpdateSkillsOption, AgentsOption };
            if (showBackOption)
            {
                operations.Add(BackAction);
            }

            string operation = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose what to install")
                .AddChoices(operations));
            if (operation == BackAction)
            {
                return 0;
            }

            return operation switch
            {
                AgentsOption => RunAgentWizard(showBackOption),
                AllSkillsOption => RunBulkSkillWizard(showBackOption),
                UpdateSkillsOption => RunUpdateSkillWizard(showBackOption),
                _ => RunSkillWizard(showBackOption)
            };
        }

        private int RunUpdateSkillWizard(bool showBackOption)
        {
            var scopes = new List<string> { ProjectScope, UserScope };
            if (showBackOption)
            {
                scopes.Add(BackAction);
            }

            string selectedScope = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose the installation scope")
                .AddChoices(scopes));
            if (selectedScope == BackAction)
            {
                return RunWizard(showBackOption);
            }

            string? projectPath = selectedScope == ProjectScope
                ? AnsiConsole.Ask<string>("Existing project [green]path[/]?")
                : null;
            var command = new SkillInstallCommand(null, selectedScope.ToLowerInvariant(), projectPath, Overwrite: false, UpdateAll: true);
            string? destination = _skillInstallService.GetInstallationPath(command with { Name = "_" });
            if (destination is null)
            {
                return _skillInstallService.RunProcess(string.Empty, command);
            }

            string destinationRoot = Path.GetDirectoryName(destination)!;
            HashSet<string> available = _skillInstallService.GetAvailableSkills().ToHashSet(StringComparer.OrdinalIgnoreCase);
            int installed = Directory.Exists(destinationRoot)
                ? Directory.GetDirectories(destinationRoot).Count(path => available.Contains(Path.GetFileName(path)))
                : 0;
            AnsiConsole.Write(new Panel($"[grey]Scope:[/] [green]{command.Scope}[/]{Environment.NewLine}[grey]Destination:[/] [green]{destinationRoot}[/]{Environment.NewLine}[grey]Skills to update:[/] [green]{installed}[/]{Environment.NewLine}[yellow]Updating replaces complete skill folders, including local changes.[/]")
                .Header("Ready to update installed skills")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
            return AnsiConsole.Confirm("Run this [green]update[/] now?", false)
                ? _skillInstallService.RunProcess(string.Empty, command)
                : 0;
        }

        private int RunBulkSkillWizard(bool showBackOption)
        {
            var scopes = new List<string> { ProjectScope, UserScope };
            if (showBackOption)
            {
                scopes.Add(BackAction);
            }

            string selectedScope = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose the installation scope")
                .AddChoices(scopes));
            if (selectedScope == BackAction)
            {
                return RunWizard(showBackOption);
            }

            string? projectPath = selectedScope == ProjectScope
                ? AnsiConsole.Ask<string>("Existing project [green]path[/]?")
                : null;
            var command = new SkillInstallCommand(null, selectedScope.ToLowerInvariant(), projectPath, Overwrite: false, All: true);
            if (selectedScope == ProjectScope && !Directory.Exists(projectPath))
            {
                return _skillInstallService.RunProcess(string.Empty, command);
            }

            IReadOnlyList<string> skills = _skillInstallService.GetAvailableSkills();
            int missing = skills.Count(skill =>
            {
                string? destination = _skillInstallService.GetInstallationPath(command with { Name = skill });
                return destination is not null && !Directory.Exists(destination);
            });
            string root = selectedScope == ProjectScope ? Path.GetFullPath(projectPath!) : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string destinationRoot = root.GetSkillDestinationRoot();
            AnsiConsole.Write(new Panel($"[grey]Scope:[/] [green]{command.Scope}[/]{Environment.NewLine}[grey]Destination:[/] [green]{destinationRoot}[/]{Environment.NewLine}[grey]Skills to install:[/] [green]{missing}[/]")
                .Header("Ready to install missing skills")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
            return AnsiConsole.Confirm("Run this [green]installation[/] now?", true)
                ? _skillInstallService.RunProcess(string.Empty, command)
                : 0;
        }

        private int RunSkillWizard(bool showBackOption)
        {
            IReadOnlyList<string> skills = _skillInstallService.GetAvailableSkills();
            if (skills.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No skills are currently available in the bundled catalog.[/]");
                if (showBackOption)
                {
                    AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Choose an action")
                        .AddChoices(new[] { BackAction }));
                }

                return 0;
            }

            var choices = skills.ToList();
            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            string selectedSkill = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose a skill to install")
                .PageSize(10)
                .AddChoices(choices));

            if (selectedSkill == BackAction)
            {
                return 0;
            }

            var scopeChoices = new List<string> { ProjectScope, UserScope };
            if (showBackOption)
            {
                scopeChoices.Add(BackAction);
            }

            string selectedScope = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose the installation scope")
                .AddChoices(scopeChoices));

            if (selectedScope == BackAction)
            {
                return RunWizard(showBackOption);
            }

            string? projectPath = selectedScope == ProjectScope
                ? AnsiConsole.Ask<string>("Existing project [green]path[/]?")
                : null;
            var command = new SkillInstallCommand(
                selectedSkill,
                selectedScope.ToLowerInvariant(),
                projectPath,
                Overwrite: false);

            string? destination = _skillInstallService.GetInstallationPath(command);
            if (destination is null)
            {
                return _skillInstallService.RunProcess(string.Empty, command);
            }

            if (Directory.Exists(destination) && !AnsiConsole.Confirm($"Replace the existing skill at [green]{destination}[/]?", false))
            {
                return 0;
            }

            command = command with { Overwrite = Directory.Exists(destination) };
            ShowSummary(command, destination);
            if (!AnsiConsole.Confirm("Run this [green]installation[/] now?", true))
            {
                return 0;
            }

            return _skillInstallService.RunProcess(string.Empty, command);
        }

        private int RunAgentWizard(bool showBackOption)
        {
            IReadOnlyList<string> agents = _agentInstallService.GetAvailableAgents();
            if (agents.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No subagents are currently available in the bundled catalog.[/]");
                if (showBackOption)
                {
                    AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Choose an action")
                        .AddChoices(new[] { BackAction }));
                }

                return 0;
            }

            var choices = agents.ToList();
            if (showBackOption)
            {
                choices.Add(BackAction);
            }

            string selectedAgent = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose a subagent to install")
                .PageSize(10)
                .AddChoices(choices));
            if (selectedAgent == BackAction)
            {
                return 0;
            }

            var scopes = new List<string> { ProjectScope, UserScope };
            if (showBackOption)
            {
                scopes.Add(BackAction);
            }

            string selectedScope = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose the installation scope")
                .AddChoices(scopes));
            if (selectedScope == BackAction)
            {
                return RunWizard(showBackOption);
            }

            string? projectPath = selectedScope == ProjectScope
                ? AnsiConsole.Ask<string>("Existing project [green]path[/]?")
                : null;
            var command = new AgentInstallCommand(selectedAgent, selectedScope.ToLowerInvariant(), projectPath, Overwrite: false);
            string? destination = _agentInstallService.GetInstallationPath(command);
            if (destination is null)
            {
                return _agentInstallService.RunProcess(string.Empty, command);
            }

            bool overwrite = File.Exists(destination);
            if (overwrite && !AnsiConsole.Confirm($"Replace the existing subagent at [green]{destination}[/]?", false))
            {
                return 0;
            }

            command = command with { Overwrite = overwrite };
            var summary = new List<string>
            {
                $"[grey]Subagent:[/] [green]{command.Name}[/]",
                $"[grey]Scope:[/] [green]{command.Scope}[/]",
                $"[grey]Destination:[/] [green]{destination}[/]",
                $"[grey]Replace existing:[/] [green]{(command.Overwrite ? "Yes" : "No")}[/]"
            };
            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summary))
                .Header("Ready to install")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
            if (!AnsiConsole.Confirm("Run this [green]installation[/] now?", true))
            {
                return 0;
            }

            return _agentInstallService.RunProcess(string.Empty, command);
        }

        private static void ShowSummary(SkillInstallCommand command, string destination)
        {
            var summary = new List<string>
            {
                $"[grey]Skill:[/] [green]{command.Name}[/]",
                $"[grey]Scope:[/] [green]{command.Scope}[/]",
                $"[grey]Destination:[/] [green]{destination}[/]",
                $"[grey]Replace existing:[/] [green]{(command.Overwrite ? "Yes" : "No")}[/]"
            };

            AnsiConsole.Write(new Panel(string.Join(Environment.NewLine, summary))
                .Header("Ready to install")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey)));
        }
    }
}
