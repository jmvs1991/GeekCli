using GeekCli.Branches;
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
                config.AddRx();

                config.AddDB();

                config.AddNgx();
            });

            return app.Run(args);
        }
    }
}
