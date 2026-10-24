using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx.Service
{
    public sealed class NgxServiceService : NgxServiceBase, INgxServiceService
    {
        public new int RunProcess(string processToRun, NgxCommand command)
        {
            var validationError = NgxNxCommandValidator.GetServiceValidationError(command);
            if (validationError is not null)
            {
                CommandOutput.Error(validationError);
                return 1;
            }

            return base.RunProcess(processToRun, command);
        }

        protected override string BuildArgs(NgxCommand command)
        {
            if (command.Nx)
            {
                var folder = command.Folder!.Replace('\\', '/').Trim('/');
                var target = string.Join('/', new[] { folder, command.Name }
                    .Where(segment => !string.IsNullOrWhiteSpace(segment)));
                return $"/c npx nx generate @nx/angular:service \"{target}\" --project=\"{command.Project}\" --type=service";
            }

            return BuildArgs($"g s {command.Name}");
        }
    }
}
