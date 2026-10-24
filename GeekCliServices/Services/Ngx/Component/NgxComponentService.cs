using GeekCliServices.Services.Ngx.Models;

using System.Text.RegularExpressions;

namespace GeekCliServices.Services.Ngx.Component
{
    public sealed class NgxComponentService : NgxServiceBase, INgxComponentService
    {
        public new int RunProcess(string processToRun, NgxCommand command)
        {
            if (command.Nx && (string.IsNullOrWhiteSpace(command.Project)
                || string.IsNullOrWhiteSpace(command.Folder)
                || string.IsNullOrWhiteSpace(command.Name)
                || string.IsNullOrWhiteSpace(command.Module)
                || !FolderBelongsToProject(command.Project, command.Folder)))
            {
                CommandOutput.Error("Nx mode requires a project, a folder inside that project, a component name, and a module.");
                return 1;
            }

            if (command.Nx && !File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "nx.json")))
            {
                CommandOutput.Error("Nx generation must run from the workspace root; nx.json was not found in the current directory.");
                return 1;
            }

            return base.RunProcess(processToRun, command);
        }

        protected override string BuildArgs(NgxCommand command)
        {
            if (command.Nx)
            {
                var folder = ToPascalCase(GetFolderWithinProject(command.Project!, command.Folder!));
                var component = ToKebabCase(command.Name);
                var target = string.Join('/', new[] { $"apps/{command.Project}/src/app", folder, component }
                    .Where(segment => !string.IsNullOrWhiteSpace(segment)));
                var selector = $"ngx-{component}-component";
                return $"/c npx nx generate @nx/angular:component {target} --standalone=false --module={command.Module} --prefix=ngx --type=component --selector={selector}";
            }

            return BuildArgs($"g c {command.Name} --prefix ngx");
        }

        private static string ToPascalCase(string value)
        {
            return string.Join('/', value.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(segment => string.Concat(GetWords(segment).Select(word => char.ToUpperInvariant(word[0]) + word[1..].ToLowerInvariant()))));
        }

        private static bool FolderBelongsToProject(string project, string folder)
        {
            var normalizedFolder = folder.Replace('\\', '/').Trim('/');
            var projectAppRoot = $"apps/{project}/src/app";
            return !normalizedFolder.StartsWith("apps/", StringComparison.OrdinalIgnoreCase)
                || normalizedFolder.Equals(projectAppRoot, StringComparison.OrdinalIgnoreCase)
                || normalizedFolder.StartsWith($"{projectAppRoot}/", StringComparison.OrdinalIgnoreCase);
        }

        private static string GetFolderWithinProject(string project, string folder)
        {
            var normalizedFolder = folder.Replace('\\', '/').Trim('/');
            var projectAppRoot = $"apps/{project}/src/app";
            if (normalizedFolder.Equals(projectAppRoot, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            if (normalizedFolder.StartsWith($"{projectAppRoot}/", StringComparison.OrdinalIgnoreCase))
            {
                return normalizedFolder[(projectAppRoot.Length + 1)..];
            }

            return normalizedFolder;
        }

        private static string ToKebabCase(string value)
        {
            return string.Join("-", GetWords(value).Select(word => word.ToLowerInvariant()));
        }

        private static string[] GetWords(string value)
        {
            var words = Regex.Replace(value.Trim(), "([a-z0-9])([A-Z])", "$1 $2");
            words = Regex.Replace(words, "([A-Z])([A-Z][a-z])", "$1 $2");
            words = Regex.Replace(words, "[^A-Za-z0-9]+", " ");
            return words.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
