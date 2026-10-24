using GeekCliServices.Services.Ngx.Models;

using GeekCliServices.ExtensionMethods;

namespace GeekCliServices.Services.Ngx.Page
{
    public sealed class NgxPageService : NgxServiceBase, INgxPageService
    {
        public new int RunProcess(string processToRun, NgxCommand command)
        {
            var validationError = NgxNxCommandValidator.GetValidationError(command, "page");
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
                var projectFolder = $"apps/{command.Project}/src/app";
                var folder = NgxNxCommandValidator.GetFolderWithinProject(command.Project!, command.Folder!);
                var page = command.Name.ToKebabCase();
                var target = string.Join('/', new[] { projectFolder, folder.ToPascalCase(), page }
                    .Where(segment => !string.IsNullOrWhiteSpace(segment)));
                var selector = $"ngx-{page}-page";
                return $"/c npx nx generate @nx/angular:component {target} --standalone=false --module={command.Module} --prefix=ngx --type=page --selector={selector}";
            }

            return BuildArgs($"g c {command.Name} --type page");
        }

    }
}
