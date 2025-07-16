using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeScreenSettings : RxSettingBase
    {
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
