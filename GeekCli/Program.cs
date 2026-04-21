using GeekCli.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GeekCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length > 0 && string.Equals(args[0], "mcp", StringComparison.OrdinalIgnoreCase))
            {
                var builder = Host.CreateApplicationBuilder(args.Skip(1).ToArray());
                return builder.RunGeekCliMcpServerAsync().GetAwaiter().GetResult();
            }

            return new ServiceCollection().RunGeekCli(args);
        }
    }
}
