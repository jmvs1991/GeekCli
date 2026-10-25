using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Ai.Skills
{
    internal sealed class SkillInstallSettings : CommandSettings
    {
        [CommandArgument(0, "<NAME>")]
        [Description("Name of the bundled skill to install.")]
        public string Name { get; set; } = string.Empty;

        [CommandOption("--scope <SCOPE>")]
        [Description("Installation scope: project or user.")]
        public string Scope { get; set; } = string.Empty;

        [CommandOption("--project-path <PATH>")]
        [Description("Existing project directory (required for project scope).")]
        public string? ProjectPath { get; set; }

        [CommandOption("--overwrite")]
        [Description("Replace the existing skill installation.")]
        public bool Overwrite { get; set; }
    }
}
