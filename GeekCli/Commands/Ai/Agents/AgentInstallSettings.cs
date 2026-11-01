using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Ai.Agents
{
    internal sealed class AgentInstallSettings : CommandSettings
    {
        [CommandArgument(0, "<CATEGORY>")]
        [Description("Category containing the bundled subagent.")]
        public string Category { get; set; } = string.Empty;

        [CommandArgument(1, "<NAME>")]
        [Description("Name of the bundled subagent to install.")]
        public string Name { get; set; } = string.Empty;

        [CommandOption("--scope <SCOPE>")]
        [Description("Installation scope: project or user.")]
        public string Scope { get; set; } = string.Empty;

        [CommandOption("--project-path <PATH>")]
        [Description("Existing project directory (required for project scope).")]
        public string? ProjectPath { get; set; }

        [CommandOption("--overwrite")]
        [Description("Replace the existing subagent installation.")]
        public bool Overwrite { get; set; }
    }
}
