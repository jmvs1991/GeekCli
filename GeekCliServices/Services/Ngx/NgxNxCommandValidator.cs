using GeekCliServices.Services.Ngx.Models;

namespace GeekCliServices.Services.Ngx
{
    internal static class NgxNxCommandValidator
    {
        public static string? GetServiceValidationError(NgxCommand command)
        {
            if (!command.Nx)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(command.Name)
                || string.IsNullOrWhiteSpace(command.Project)
                || string.IsNullOrWhiteSpace(command.Folder)
                || ContainsParentDirectorySegment(command.Folder))
            {
                return "Nx mode requires a service name, project, and project-relative folder without '..' segments.";
            }

            return GetWorkspaceRootValidationError();
        }

        public static string? GetValidationError(NgxCommand command, string itemName, string? destinationRoot = null)
        {
            if (!command.Nx)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(command.Project)
                || string.IsNullOrWhiteSpace(command.Folder)
                || string.IsNullOrWhiteSpace(command.Name)
                || string.IsNullOrWhiteSpace(command.Module)
                || !FolderBelongsToProject(command.Project, command.Folder, destinationRoot))
            {
                return $"Nx mode requires a project, a folder inside that project, a {itemName} name, and a module.";
            }

            return GetWorkspaceRootValidationError();
        }

        public static string GetFolderWithinProject(string project, string folder, string? destinationRoot = null)
        {
            var normalizedFolder = folder.Replace('\\', '/').Trim('/');
            if (!string.IsNullOrWhiteSpace(destinationRoot))
            {
                var normalizedRoot = destinationRoot.Replace('\\', '/').Trim('/');
                if (normalizedFolder.Equals(normalizedRoot, StringComparison.OrdinalIgnoreCase))
                {
                    return string.Empty;
                }

                if (normalizedFolder.StartsWith($"{normalizedRoot}/", StringComparison.OrdinalIgnoreCase))
                {
                    return normalizedFolder[(normalizedRoot.Length + 1)..];
                }
            }

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

        private static bool FolderBelongsToProject(string project, string folder, string? destinationRoot)
        {
            var normalizedFolder = folder.Replace('\\', '/').Trim('/');
            var projectAppRoot = $"apps/{project}/src/app";
            if (ContainsParentDirectorySegment(normalizedFolder))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(destinationRoot))
            {
                var normalizedRoot = destinationRoot.Replace('\\', '/').Trim('/');
                if (normalizedFolder.Equals(normalizedRoot, StringComparison.OrdinalIgnoreCase)
                    || normalizedFolder.StartsWith($"{normalizedRoot}/", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                return !IsWorkspaceRelativePath(normalizedFolder);
            }

            return !normalizedFolder.StartsWith("apps/", StringComparison.OrdinalIgnoreCase)
                || normalizedFolder.Equals(projectAppRoot, StringComparison.OrdinalIgnoreCase)
                || normalizedFolder.StartsWith($"{projectAppRoot}/", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsWorkspaceRelativePath(string path)
        {
            return path.StartsWith("apps/", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("libs/", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("packages/", StringComparison.OrdinalIgnoreCase)
                || path.StartsWith("projects/", StringComparison.OrdinalIgnoreCase);
        }

        private static bool ContainsParentDirectorySegment(string path)
        {
            return path.Replace('\\', '/').Split('/').Contains("..", StringComparer.Ordinal);
        }

        private static string? GetWorkspaceRootValidationError()
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "nx.json"))
                ? null
                : "Nx generation must run from the workspace root; nx.json was not found in the current directory.";
        }
    }
}
