using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Rx.Native
{
    class RxNativeModuleSettings : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the module to create.")]
        public string Name { get; set; }
    }
}
