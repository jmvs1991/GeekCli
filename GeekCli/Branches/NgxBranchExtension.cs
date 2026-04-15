using GeekCli.Commands.Ngx.Component;
using GeekCli.Commands.Ngx.Page;
using Spectre.Console.Cli;

namespace GeekCli.Branches
{
    internal static class NgxBranchExtension
    {
        public static IBranchConfigurator AddNgx(this IConfigurator configurator)
        {
            return configurator.AddBranch("ngx", ngx =>
            {
                ngx.SetDescription("Angular utilities");

                ngx.AddCommand<NgxPageCommand>("page")
                   .WithDescription("Generates a new Angular page with related files.");

                ngx.AddCommand<NgxComponentCommand>("component")
                   .WithDescription("Generates a new Angular component with related files.");
            });
        }
    }
}
