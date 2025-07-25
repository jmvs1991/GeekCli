﻿using Spectre.Console.Cli;
using System.ComponentModel;

namespace GeekCli.Commands.Rx
{
    class RxSettingBase : CommandSettings
    {
        [CommandArgument(0, "<name>")]
        [Description("The name of the file to create.")]
        public string Name { get; set; }

        [CommandOption("--flat")]
        [Description("If true, files will be created in the current directory without a folder.")]
        [DefaultValue(false)]
        public bool Flat { get; set; }
    }
}
