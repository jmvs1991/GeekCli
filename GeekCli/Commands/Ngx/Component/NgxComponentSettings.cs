namespace GeekCli.Commands.Ngx.Component
{
    class NgxComponentSettings : NgxSettingsBase
    {
        [Spectre.Console.Cli.CommandOption("--nx")]
        [System.ComponentModel.Description("Generate the component in an Nx workspace.")]
        public bool Nx { get; set; }

        [Spectre.Console.Cli.CommandOption("--project <PROJECT>")]
        [System.ComponentModel.Description("The Nx Angular project name.")]
        public string? Project { get; set; }

        [Spectre.Console.Cli.CommandOption("--folder <FOLDER>")]
        [System.ComponentModel.Description("Workspace-relative destination folder under the selected application's src/app or library sourceRoot. The path is used as provided.")]
        public string? Folder { get; set; }

        [Spectre.Console.Cli.CommandOption("--module <MODULE>")]
        [System.ComponentModel.Description("Workspace-relative path to the Angular NgModule file that declares the component, including .ts.")]
        public string? Module { get; set; }
    }
}
