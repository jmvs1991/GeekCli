using GeekCli.Branches;
using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Ngx.Component;
using GeekCliServices.Services.Ngx.Page;
using GeekCliServices.Services.Rx.Common.Context;
using GeekCliServices.Services.Rx.Native.Component;
using GeekCliServices.Services.Rx.Native.Module;
using GeekCliServices.Services.Rx.Native.Screen;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace GeekCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAddMigrationService, AddMigrationService>();
            services.AddSingleton<IRemoveMigrationService, RemoveMigrationService>();
            services.AddSingleton<IRollbackMigrationService, RollbackMigrationService>();
            services.AddSingleton<INgxComponentService, NgxComponentService>();
            services.AddSingleton<INgxPageService, NgxPageService>();
            services.AddSingleton<IRxContextService, RxContextService>();
            services.AddSingleton<IRxNativeComponentService, RxNativeComponentService>();
            services.AddSingleton<IRxNativeModuleService, RxNativeModuleService>();
            services.AddSingleton<IRxNativeScreenService, RxNativeScreenService>();

            var registrar = new TypeRegistrar(services);

            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddRx();

                config.AddDB();

                config.AddNgx();
            });

            return app.Run(args);
        }
    }
}
