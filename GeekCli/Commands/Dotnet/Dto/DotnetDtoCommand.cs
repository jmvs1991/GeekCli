using GeekCliServices.Services.Dotnet.Dto;
using DotnetDtoSpec = GeekCliServices.Services.Dotnet.Dto.Models.DotnetDtoCommand;

namespace GeekCli.Commands.Dotnet.Dto
{
    internal sealed class DotnetDtoCommand : DotnetCommandExecutorBase<DotnetDtoSettings, IDotnetDtoService, DotnetDtoSpec>
    {
        public DotnetDtoCommand(IDotnetDtoService service) : base(service)
        {
        }

        protected override DotnetDtoSpec MapToCommand(DotnetDtoSettings settings)
        {
            return new DotnetDtoSpec(settings.Name!,
                                     settings.ProjectName!,
                                     DotnetScopeHelper.Parse(settings.Scope),
                                     settings.View);
        }

        protected override int ExecuteCommand(IDotnetDtoService service, string processToRun, DotnetDtoSpec command)
        {
            return service.RunProcess(processToRun, command);
        }
    }
}
