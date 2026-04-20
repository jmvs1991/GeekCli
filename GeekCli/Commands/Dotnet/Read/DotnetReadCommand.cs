using GeekCliServices.Services.Dotnet.Read;
using DotnetReadSpec = GeekCliServices.Services.Dotnet.Read.Models.DotnetReadCommand;

namespace GeekCli.Commands.Dotnet.Read
{
    internal sealed class DotnetReadCommand : DotnetCommandExecutorBase<DotnetReadSettings, IDotnetReadService, DotnetReadSpec>
    {
        public DotnetReadCommand(IDotnetReadService service) : base(service)
        {
        }

        protected override DotnetReadSpec MapToCommand(DotnetReadSettings settings)
        {
            return new DotnetReadSpec(settings.Name!,
                                      settings.DbSchema!,
                                      settings.ContextName!,
                                      DotnetScopeHelper.Parse(settings.Scope),
                                      settings.View);
        }

        protected override int ExecuteCommand(IDotnetReadService service, string processToRun, DotnetReadSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
