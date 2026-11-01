using GeekCliServices.Services.Ai.Skills.Models;

namespace GeekCliServices.Services.Ai.Skills
{
    public interface ISkillInstallService : ICommandService<SkillInstallCommand>
    {
        IReadOnlyList<string> GetAvailableSkills();

        IReadOnlyList<string> GetAvailableCategories();

        IReadOnlyList<string> GetAvailableSkills(string category);

        string? GetInstallationPath(SkillInstallCommand command);
    }
}
