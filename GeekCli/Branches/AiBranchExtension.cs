using GeekCli.Commands.Ai.Skills;
using GeekCli.Commands.Ai.Agents;
using GeekCli.Commands.Ai.Wizard;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class AiBranchExtension
    {
        public static IBranchConfigurator AddAI(this IConfigurator configurator)
        {
            return configurator.AddBranch("ai", ai =>
            {
                ai.SetDescription("AI skills and agent utilities");
                ai.SetDefaultCommand<AiWizardCommand>();
                ai.AddBranch("skill", skill =>
                {
                    skill.AddCommand<SkillInstallCliCommand>("install")
                         .WithDescription("Installs a bundled AI skill.");
                });
                ai.AddBranch("agent", agent =>
                {
                    agent.AddCommand<AgentInstallCliCommand>("install")
                         .WithDescription("Installs a bundled AI subagent.");
                });
            });
        }
    }
}
