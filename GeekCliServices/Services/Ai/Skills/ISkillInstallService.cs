using GeekCliServices.Services.Ai.Skills.Models;

namespace GeekCliServices.Services.Ai.Skills
{
    public interface ISkillInstallService : ICommandService<SkillInstallCommand>
    {
        IReadOnlyList<string> GetAvailableSkills();

        string? GetInstallationPath(SkillInstallCommand command);
    }
}
