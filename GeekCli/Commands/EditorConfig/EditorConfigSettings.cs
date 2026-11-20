using System.ComponentModel;
using GeekCliServices.Services.EditorConfig.Models;
using Spectre.Console.Cli;

namespace GeekCli.Commands.EditorConfig
{
    internal sealed class EditorConfigSettings : CommandSettings
    {
        [CommandOption("--project-path <PATH>")]
        [Description("Existing directory where .editorconfig will be created.")]
        [DefaultValue(EditorConfigCommand.DefaultProjectPath)]
        public string ProjectPath { get; init; } = EditorConfigCommand.DefaultProjectPath;

        [CommandOption("--type <TYPE>")]
        [Description("Project type: csharp, angular, or react.")]
        [DefaultValue(EditorConfigCommand.CSharp)]
        public string ProjectType { get; init; } = EditorConfigCommand.CSharp;
    }
}
