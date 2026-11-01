namespace GeekCliServices.Services.Ai.Agents.Models
{
    public sealed record AgentInstallCommand(string Category, string Name, string Scope, string? ProjectPath, bool Overwrite);
}
