using GeekCliServices.Services.Ai.Agents.Models;

namespace GeekCliServices.Services.Ai.Agents
{
    public interface IAgentInstallService : ICommandService<AgentInstallCommand>
    {
        IReadOnlyList<string> GetAvailableAgents();
        string? GetInstallationPath(AgentInstallCommand command);
    }
}
