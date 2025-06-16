using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeScreenSettings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the screen to create.")]
        public string Name { get; set; }

        [CommandOption("--flat")]
        [Description("If true, files are created directly without a folder.")]
        [DefaultValue(false)]
        public bool Flat { get; set; }

        [CommandOption("--schema")]
        [Description("If true, generates a .schema.tsx file for the screen.")]
        [DefaultValue(false)]
        public bool Schema { get; set; }

        [CommandOption("--wrapper")]
        [Description("If true, generates a .wrapper.tsx file for the screen.")]
        [DefaultValue(false)]
        public bool Wrapper { get; set; }
    }
}
