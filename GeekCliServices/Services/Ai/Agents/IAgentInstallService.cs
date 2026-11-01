using GeekCliServices.Services.Ai.Agents.Models;

namespace GeekCliServices.Services.Ai.Agents
{
    public interface IAgentInstallService : ICommandService<AgentInstallCommand>
    {
        IReadOnlyList<string> GetAvailableCategories();
        IReadOnlyList<string> GetAvailableAgents(string category);
        string? GetInstallationPath(AgentInstallCommand command);
    }
}
