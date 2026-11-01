namespace GeekCliServices.Services.Ai.Skills.Models
{
    public sealed record SkillInstallCommand(string? Category, string? Name, string Scope, string? ProjectPath, bool Overwrite, bool All = false, bool UpdateAll = false);
}
