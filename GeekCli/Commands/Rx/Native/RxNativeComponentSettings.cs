using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeComponentSettings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the component to create.")]
        public string Name { get; set; }

        [CommandOption("--flat")]
        [Description("If true, creates files directly without a folder.")]
        [DefaultValue(false)]
        public bool Flat { get; set; }
    }
}
