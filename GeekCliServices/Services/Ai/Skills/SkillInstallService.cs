using GeekCliServices.Services.Ai.Skills.Models;
using GeekCliServices.ExtensionMethods;

namespace GeekCliServices.Services.Ai.Skills
{
    public sealed class SkillInstallService : ISkillInstallService
    {
        private static string CatalogRoot => Path.Combine(AppContext.BaseDirectory, "Skills");

        public IReadOnlyList<string> GetAvailableSkills()
        {
            return Directory.Exists(CatalogRoot)
                ? Directory.GetDirectories(CatalogRoot).Select(Path.GetFileName).Where(name => name is not null).Cast<string>().OrderBy(name => name, StringComparer.OrdinalIgnoreCase).ToArray()
                : Array.Empty<string>();
        }

        public string? GetInstallationPath(SkillInstallCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                return null;
            }

            string? root = ResolveScopeRoot(command);

            return root is null ? null : Path.Combine(root.GetSkillDestinationRoot(), command.Name);
        }

        public int RunProcess(string processToRun, SkillInstallCommand command)
        {
            if (command.All)
            {
                if (!string.IsNullOrWhiteSpace(command.Name) || command.Overwrite || command.UpdateAll)
                {
                    CommandOutput.Error("Bulk installation cannot be combined with a skill name, --overwrite, or --update-all.");
                    return 1;
                }

                return InstallMissingSkills(command);
            }

            if (command.UpdateAll)
            {
                if (!string.IsNullOrWhiteSpace(command.Name) || command.All || command.Overwrite)
                {
                    CommandOutput.Error("--update-all cannot be combined with --all, a skill name, or --overwrite.");
                    return 1;
                }

                return UpdateInstalledSkills(command);
            }

            if (string.IsNullOrWhiteSpace(command.Name) || command.Name is "." or ".." ||
                command.Name.Contains('/') || command.Name.Contains('\\') ||
                command.Name != Path.GetFileName(command.Name) ||
                command.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                CommandOutput.Error("The skill name is invalid.");
                return 1;
            }

            if (!Directory.Exists(CatalogRoot) || Directory.GetDirectories(CatalogRoot).Length == 0)
            {
                CommandOutput.Error("No skills are currently available in the bundled catalog.");
                return 1;
            }

            string catalogPath = Path.Combine(CatalogRoot, command.Name);
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

        private int InstallMissingSkills(SkillInstallCommand command)
        {
            string? root = ResolveScopeRoot(command);
            if (root is null)
            {
                CommandOutput.Error("Project scope requires an existing --project-path; user scope does not accept --project-path.");
                return 1;
            }

            string[] skills = Directory.Exists(CatalogRoot)
                ? Directory.GetDirectories(CatalogRoot).OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToArray()
                : Array.Empty<string>();
            string destinationRoot = root.GetSkillDestinationRoot();
            int installed = 0;
            int skipped = 0;
            int errors = 0;

            foreach (string catalogPath in skills)
            {
                string name = Path.GetFileName(catalogPath);
                string destination = Path.Combine(destinationRoot, name);
                if (Directory.Exists(destination))
                {
                    skipped++;
                    continue;
                }

                string stagingPath = Path.Combine(destinationRoot, $".{name}.{Guid.NewGuid():N}.tmp");
                try
                {
                    Directory.CreateDirectory(destinationRoot);
                    CopyDirectory(catalogPath, stagingPath);
                    Directory.Move(stagingPath, destination);
                    installed++;
                    CommandOutput.Info($"Installed skill '{name}' to '{destination}'.");
                }
                catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
                {
                    errors++;
                    CommandOutput.Error($"Could not install skill '{name}': {exception.Message}");
                }
                finally
                {
                    try
                    {
                        if (Directory.Exists(stagingPath))
                        {
                            Directory.Delete(stagingPath, recursive: true);
                        }
                    }
                    catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
                    {
                        errors++;
                        CommandOutput.Error($"Could not clean up the staging directory for skill '{name}': {exception.Message}");
                    }
                }
            }

            CommandOutput.Info($"Bulk installation summary: {installed} installed, {skipped} skipped, {errors} errors.");
            if (skills.Length == 0 || installed == 0 && errors == 0)
            {
                CommandOutput.Info("There are no missing skills to install.");
            }

            return errors == 0 ? 0 : 1;
        }

        private int UpdateInstalledSkills(SkillInstallCommand command)
        {
            string? root = ResolveScopeRoot(command);
            if (root is null)
            {
                CommandOutput.Error("Project scope requires an existing --project-path; user scope does not accept --project-path.");
                return 1;
            }

            string destinationRoot = root.GetSkillDestinationRoot();
            string[] installed = Directory.Exists(destinationRoot)
                ? Directory.GetDirectories(destinationRoot).OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToArray()
                : Array.Empty<string>();
            int updated = 0;
            int skipped = 0;
            int errors = 0;

            foreach (string destination in installed)
            {
                string name = Path.GetFileName(destination);
                string catalogPath = Path.Combine(CatalogRoot, name);
                if (!Directory.Exists(catalogPath))
                {
                    skipped++;
                    continue;
                }

                string stagingPath = Path.Combine(destinationRoot, $".{name}.{Guid.NewGuid():N}.tmp");
                try
                {
                    CopyDirectory(catalogPath, stagingPath);
                    Directory.Delete(destination, recursive: true);
                    Directory.Move(stagingPath, destination);
                    updated++;
                    CommandOutput.Info($"Updated skill '{name}' at '{destination}'.");
                }
                catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
                {
                    errors++;
                    CommandOutput.Error($"Could not update skill '{name}': {exception.Message}");
                }
                finally
                {
                    try
                    {
                        if (Directory.Exists(stagingPath))
                        {
                            Directory.Delete(stagingPath, recursive: true);
                        }
                    }
                    catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
                    {
                        errors++;
                        CommandOutput.Error($"Could not clean up the staging directory for skill '{name}': {exception.Message}");
                    }
                }
            }

            CommandOutput.Info($"Bulk update summary: {updated} updated, {skipped} skipped, {errors} errors.");
            if (updated == 0 && errors == 0)
            {
                CommandOutput.Info("There are no installed catalog skills to update.");
            }

            return errors == 0 ? 0 : 1;
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

        private static string? ResolveScopeRoot(SkillInstallCommand command)
        {
            return command.Scope?.ToLowerInvariant() switch
            {
                "project" when !string.IsNullOrWhiteSpace(command.ProjectPath) && Directory.Exists(command.ProjectPath) => Path.GetFullPath(command.ProjectPath),
                "user" when string.IsNullOrWhiteSpace(command.ProjectPath) => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                _ => null
            };
        }

    }
}
