using GeekCliServices.Services.Ai.Skills.Models;

namespace GeekCliServices.Services.Ai.Skills
{
    public sealed class SkillInstallService : ISkillInstallService
    {
        public IReadOnlyList<string> GetAvailableSkills()
        {
            string catalogRoot = Path.Combine(AppContext.BaseDirectory, "Skills");
            return Directory.Exists(catalogRoot)
                ? Directory.GetDirectories(catalogRoot).Select(Path.GetFileName).Where(name => name is not null).Cast<string>().OrderBy(name => name, StringComparer.OrdinalIgnoreCase).ToArray()
                : Array.Empty<string>();
        }

        public string? GetInstallationPath(SkillInstallCommand command)
        {
            string? root = command.Scope?.ToLowerInvariant() switch
            {
                "project" when !string.IsNullOrWhiteSpace(command.ProjectPath) && Directory.Exists(command.ProjectPath) => Path.GetFullPath(command.ProjectPath),
                "user" when string.IsNullOrWhiteSpace(command.ProjectPath) => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                _ => null
            };

            return root is null ? null : Path.Combine(root, ".agents", "skills", command.Name);
        }

        public int RunProcess(string processToRun, SkillInstallCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name) || command.Name is "." or ".." ||
                command.Name.Contains('/') || command.Name.Contains('\\') ||
                command.Name != Path.GetFileName(command.Name) ||
                command.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                CommandOutput.Error("The skill name is invalid.");
                return 1;
            }

            string catalogRoot = Path.Combine(AppContext.BaseDirectory, "Skills");
            if (!Directory.Exists(catalogRoot) || Directory.GetDirectories(catalogRoot).Length == 0)
            {
                CommandOutput.Error("No skills are currently available in the bundled catalog.");
                return 1;
            }

            string catalogPath = Path.Combine(catalogRoot, command.Name);
            if (!Directory.Exists(catalogPath))
            {
                CommandOutput.Error($"Skill '{command.Name}' is not available in the bundled catalog.");
                return 1;
            }

            string? destination = GetInstallationPath(command);
            if (destination is null)
            {
                CommandOutput.Error("Project scope requires an existing --project-path; user scope does not accept --project-path.");
                return 1;
            }

            if (Directory.Exists(destination) && !command.Overwrite)
            {
                CommandOutput.Error($"Skill already exists at '{destination}'. Use --overwrite to replace it.");
                return 1;
            }

            try
            {
                string parent = Path.GetDirectoryName(destination)!;
                Directory.CreateDirectory(parent);
                string stagingPath = Path.Combine(parent, $".{command.Name}.{Guid.NewGuid():N}.tmp");
                CopyDirectory(catalogPath, stagingPath);
                if (Directory.Exists(destination))
                {
                    Directory.Delete(destination, recursive: true);
                }

                Directory.Move(stagingPath, destination);
                CommandOutput.Info($"Installed skill '{command.Name}' to '{destination}'.");
                return 0;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                CommandOutput.Error($"Could not install skill '{command.Name}': {exception.Message}");
                return 1;
            }
        }

        private static void CopyDirectory(string source, string destination)
        {
            Directory.CreateDirectory(destination);
            foreach (string file in Directory.GetFiles(source))
            {
                File.Copy(file, Path.Combine(destination, Path.GetFileName(file)));
            }

            foreach (string directory in Directory.GetDirectories(source))
            {
                CopyDirectory(directory, Path.Combine(destination, Path.GetFileName(directory)));
            }
        }
    }
}
