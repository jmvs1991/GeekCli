using GeekCliServices.Services.Dotnet.Cache;
using DotnetCacheSpec = GeekCliServices.Services.Dotnet.Cache.Models.DotnetCacheCommand;

namespace GeekCli.Commands.Dotnet.Cache
{
    internal sealed class DotnetCacheCommand : DotnetCommandExecutorBase<DotnetCacheSettings, IDotnetCacheService, DotnetCacheSpec>
    {
        public DotnetCacheCommand(IDotnetCacheService service) : base(service)
        {
        }

        protected override DotnetCacheSpec MapToCommand(DotnetCacheSettings settings)
        {
            return new DotnetCacheSpec(settings.Name!,
                                       settings.ProjectName!,
                                       DotnetScopeHelper.Parse(settings.Scope));
        }

        protected override int ExecuteCommand(IDotnetCacheService service, string processToRun, DotnetCacheSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
