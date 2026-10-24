using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx.Service
{
    public sealed class NgxServiceService : NgxServiceBase, INgxServiceService
    {
        public new int RunProcess(string processToRun, NgxCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                CommandOutput.Error("A service name is required.");
                return 1;
            }

            return base.RunProcess(processToRun, command);
        }

        protected override string BuildArgs(NgxCommand command)
        {
            return BuildArgs($"g s {command.Name}");
        }
    }
}
