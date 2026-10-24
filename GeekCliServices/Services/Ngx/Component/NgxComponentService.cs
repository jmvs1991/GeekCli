using GeekCliServices.Services.Ngx.Models;

using GeekCliServices.ExtensionMethods;

namespace GeekCliServices.Services.Ngx.Component
{
    public sealed class NgxComponentService : NgxServiceBase, INgxComponentService
    {
        public new int RunProcess(string processToRun, NgxCommand command)
        {
            var validationError = NgxNxCommandValidator.GetValidationError(command, "component");
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
                var component = command.Name.ToKebabCase();
                var targetFolder = folder;
                var target = string.Join('/', new[] { targetFolder, component }
                    .Where(segment => !string.IsNullOrWhiteSpace(segment)));
                var selector = $"ngx-{component}-component";
                var module = Path.GetRelativePath(Path.GetFullPath(targetFolder), Path.GetFullPath(command.Module!)).Replace('\\', '/');
                if (module.EndsWith(".ts", StringComparison.OrdinalIgnoreCase))
                {
                    module = module[..^3];
                }

                return $"/c npx nx generate @nx/angular:component {target} --standalone=false --module={module} --prefix=ngx --type=component --selector={selector}";
            }

            return BuildArgs($"g c {command.Name} --prefix ngx");
        }
    }
}
