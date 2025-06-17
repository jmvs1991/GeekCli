using GeekCli.Commands.Db;
using GeekCli.Commands.Rx.Common;
using GeekCli.Commands.Rx.Native;
using Spectre.Console.Cli;

namespace GeekCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandApp();

            app.Configure(config =>
            {
                config.AddBranch("rx", rx =>
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

                config.AddBranch("db", db =>
                {
                    db.SetDescription("Database utilities");

                    db.AddBranch("migration", migration =>
                    {
                        migration.AddCommand<DbAddMigrationCommand>("add")
                                 .WithDescription("Generates a new React Context with related files.");

                        migration.AddCommand<DbRemoveMigrationCommand>("remove")
                                 .WithDescription("Generates a new React Context with related files.");
                    });
                });
            });

            return app.Run(args);
        }
    }
}
