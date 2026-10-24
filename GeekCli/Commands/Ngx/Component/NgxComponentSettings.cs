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
        [System.ComponentModel.Description("The folder under src/app where the component will be generated.")]
        public string? Folder { get; set; }

        [Spectre.Console.Cli.CommandOption("--module <MODULE>")]
        [System.ComponentModel.Description("The Angular module file that declares the component.")]
        public string? Module { get; set; }
    }
}
