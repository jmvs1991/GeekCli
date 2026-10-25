using GeekCliServices.Services.Ai.Agents;
using GeekCliServices.Services.Ai.Agents.Models;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Ai.Agents
{
    internal sealed class AgentInstallCliCommand : Command<AgentInstallSettings>
    {
        private readonly IAgentInstallService _service;

        public AgentInstallCliCommand(IAgentInstallService service)
        {
            _service = service;
        }

        protected override int Execute(CommandContext context, AgentInstallSettings settings, CancellationToken cancellationToken)
        {
            return _service.RunProcess(string.Empty,
                new AgentInstallCommand(settings.Name, settings.Scope, settings.ProjectPath, settings.Overwrite));
        }
    }
}
