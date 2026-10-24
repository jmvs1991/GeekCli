using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Ngx.Service
{
    class NgxServiceSettings : NgxSettingsBase
    {
        [CommandOption("--nx")]
        [Description("Generate the service in an Nx workspace.")]
        public bool Nx { get; set; }

        [CommandOption("--project <PROJECT>")]
        [Description("The Nx Angular project name.")]
        public string? Project { get; set; }

        [CommandOption("--folder <FOLDER>")]
        [Description("The project-relative destination folder.")]
        public string? Folder { get; set; }
    }
}
