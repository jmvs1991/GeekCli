using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Db
{
    class DbSettingBase : CommandSettings
    {
        [CommandOption("--project")]
        [Description("The base name of the project (e.g., 'Booking'). Used to resolve csproj paths.")]
        public string ProjectName { get; set; }

        [CommandOption("--init")]
        [Description("Indicates whether this is an initialization migration. If true, output will go to 'Migrations/Init'.")]
        [DefaultValue(false)]
        public bool Init { get; set; }
    }
}
