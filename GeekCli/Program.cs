using GeekCli.Branches;
using GeekCli.Commands.Db.Wizard;
using GeekCli.Commands.Dotnet.Wizard;
using GeekCli.Commands.Ngx.Wizard;
using GeekCli.Commands.Rx.Wizard;
using GeekCli.Commands.Wizard;
using GeekCli.Infrastructure.DependencyInjection;
using GeekCliServices.Services.Dotnet.Cache;
using GeekCliServices.Services.Dotnet.Controller;
using GeekCliServices.Services.Dotnet.Dto;
using GeekCliServices.Services.Dotnet.List;
using GeekCliServices.Services.Dotnet.Read;
using GeekCliServices.Services.Dotnet.Resource;
using GeekCliServices.Services.Dotnet.Service;
using GeekCliServices.Services.Dotnet.Sp;
using GeekCliServices.Services.Dotnet.Write;
using GeekCliServices.Services.Db.Migrations.Add;
using GeekCliServices.Services.Db.Migrations.Remove;
using GeekCliServices.Services.Db.Migrations.Rollback;
using GeekCliServices.Services.Db.Scaffold;
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
            services.AddSingleton<IDotnetListService, DotnetListService>();
            services.AddSingleton<IDotnetDtoService, DotnetDtoService>();
            services.AddSingleton<IDotnetResourceService, DotnetResourceService>();
            services.AddSingleton<IDotnetCacheService, DotnetCacheService>();
            services.AddSingleton<IDotnetSpService, DotnetSpService>();
            services.AddSingleton<IDotnetReadService, DotnetReadService>();
            services.AddSingleton<IDotnetWriteService, DotnetWriteService>();
            services.AddSingleton<IDotnetControllerService, DotnetControllerService>();
            services.AddSingleton<IDotnetServiceService, DotnetServiceService>();
            services.AddSingleton<IAddMigrationService, AddMigrationService>();
            services.AddSingleton<IRemoveMigrationService, RemoveMigrationService>();
            services.AddSingleton<IRollbackMigrationService, RollbackMigrationService>();
            services.AddSingleton<IDbScaffoldService, DbScaffoldService>();
            services.AddSingleton<INgxComponentService, NgxComponentService>();
            services.AddSingleton<INgxPageService, NgxPageService>();
            services.AddSingleton<IRxContextService, RxContextService>();
            services.AddSingleton<IRxNativeComponentService, RxNativeComponentService>();
            services.AddSingleton<IRxNativeModuleService, RxNativeModuleService>();
            services.AddSingleton<IRxNativeScreenService, RxNativeScreenService>();
            services.AddSingleton<IDbWizard>(provider => provider.GetRequiredService<DbWizardCommand>());
            services.AddSingleton<IDotnetWizard>(provider => provider.GetRequiredService<DotnetWizardCommand>());
            services.AddSingleton<INgxWizard>(provider => provider.GetRequiredService<NgxWizardCommand>());
            services.AddSingleton<IRxWizard>(provider => provider.GetRequiredService<RxWizardCommand>());

            var registrar = new TypeRegistrar(services);

            var app = new CommandApp(registrar);
            app.SetDefaultCommand<GeneralWizardCommand>();

            app.Configure(config =>
            {
                config.AddRx();

                config.AddDB();

                config.AddNgx();

                config.AddDotnet();
            });

            return app.Run(args);
        }
    }
}
