using GeekCliServices.Services.Ai.Skills;
using GeekCliServices.Services.Ai.Skills.Models;
using Spectre.Console.Cli;

namespace GeekCli.Commands.Ai.Skills
{
    internal sealed class SkillInstallCliCommand : Command<SkillInstallSettings>
    {
        private readonly ISkillInstallService _service;

        public SkillInstallCliCommand(ISkillInstallService service)
        {
            _service = service;
        }

        protected override int Execute(CommandContext context, SkillInstallSettings settings, CancellationToken cancellationToken)
        {
            return _service.RunProcess(string.Empty,
                new SkillInstallCommand(settings.Name, settings.Scope, settings.ProjectPath, settings.Overwrite, settings.All, settings.UpdateAll));
        }
    }
}
