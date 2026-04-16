using GeekCli.Branches;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace GeekCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var services = new ServiceCollection();

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
