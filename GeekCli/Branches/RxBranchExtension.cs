using GeekCli.Commands.Rx.Common;
using GeekCli.Commands.Rx.Native;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class RxBranchExtension
    {
        public static IBranchConfigurator AddRx(this IConfigurator configurator)
        {
            return configurator.AddBranch("rx", rx =>
            {
                rx.SetDescription("React utilities and generators");

                rx.AddCommand<RxContextCommand>("context")
                  .WithDescription("Generates a new React Context with related files.");

                rx.AddBranch("native", native =>
                {
                    native.SetDescription("React Native related utilities");

                    native.AddCommand<RxNativeModuleCommand>("module")
                          .WithDescription("Scaffold a new React Native module with Screens, Core, and Modals folders.");

                    native.AddCommand<RxNativeScreenCommand>("screen")
                          .WithDescription("Creates a new screen component and style file.");

                    native.AddCommand<RxNativeComponentCommand>("component")
                          .WithDescription("Creates a new React Native component with style file.");
                });
            });
        }
    }
}
