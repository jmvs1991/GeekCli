using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Ngx
{
    abstract class NgxSettingsBase : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the file to create.")]
        public string Name { get; set; }
    }
}
