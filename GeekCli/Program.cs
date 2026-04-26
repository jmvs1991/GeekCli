using GeekCli.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

if (args.Length > 0 && string.Equals(args[0], "mcp", StringComparison.OrdinalIgnoreCase))
{
    var builder = Host.CreateApplicationBuilder(args.Skip(1).ToArray());
    await builder.RunGeekCliMcpServerAsync();
}
else
{
    new ServiceCollection().RunGeekCli(args);
}
