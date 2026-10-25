using GeekCliServices.Services.Ai.Agents.Models;

namespace GeekCliServices.Services.Ai.Agents
{
    public sealed class AgentInstallService : IAgentInstallService
    {
        public IReadOnlyList<string> GetAvailableAgents()
        {
            string catalogRoot = GetCatalogRoot();
            return Directory.Exists(catalogRoot)
                ? Directory.GetFiles(catalogRoot, "*.toml")
                    .Select(Path.GetFileNameWithoutExtension)
                    .Where(name => name is not null)
                    .Cast<string>()
                    .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                    .ToArray()
                : Array.Empty<string>();
        }

        public string? GetInstallationPath(AgentInstallCommand command)
        {
            string? root = command.Scope?.ToLowerInvariant() switch
            {
                "project" when !string.IsNullOrWhiteSpace(command.ProjectPath) && Directory.Exists(command.ProjectPath) => Path.GetFullPath(command.ProjectPath),
                "user" when string.IsNullOrWhiteSpace(command.ProjectPath) => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                _ => null
            };

            return root is null ? null : Path.Combine(root, ".codex", "agents", $"{command.Name}.toml");
        }

        public int RunProcess(string processToRun, AgentInstallCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name) || command.Name is "." or ".." ||
                command.Name.Contains('/') || command.Name.Contains('\\') ||
                command.Name != Path.GetFileName(command.Name) ||
                command.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                CommandOutput.Error("The subagent name is invalid.");
                return 1;
            }

            string catalogPath = Path.Combine(GetCatalogRoot(), $"{command.Name}.toml");
            if (!File.Exists(catalogPath))
            {
                if (GetAvailableAgents().Count == 0)
                {
                    CommandOutput.Error("No subagents are currently available in the bundled catalog.");
                }
                else
                {
                    CommandOutput.Error($"Subagent '{command.Name}' is not available in the bundled catalog.");
                }

                return 1;
            }

            string? destination = GetInstallationPath(command);
            if (destination is null)
            {
                CommandOutput.Error("Project scope requires an existing --project-path; user scope does not accept --project-path.");
                return 1;
            }

            if (File.Exists(destination) && !command.Overwrite)
            {
                CommandOutput.Error($"Subagent already exists at '{destination}'. Use --overwrite to replace it.");
                return 1;
            }

            string? stagingPath = null;
            try
            {
                string parent = Path.GetDirectoryName(destination)!;
                Directory.CreateDirectory(parent);
                stagingPath = Path.Combine(parent, $".{command.Name}.{Guid.NewGuid():N}.tmp");
                File.Copy(catalogPath, stagingPath);
                File.Move(stagingPath, destination, command.Overwrite);
                CommandOutput.Info($"Installed subagent '{command.Name}' to '{destination}'.");
                return 0;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                CommandOutput.Error($"Could not install subagent '{command.Name}': {exception.Message}");
                return 1;
            }
            finally
            {
                if (stagingPath is not null && File.Exists(stagingPath))
                {
                    File.Delete(stagingPath);
                }
            }
        }

        private static string GetCatalogRoot()
        {
            return Path.Combine(AppContext.BaseDirectory, "Agents");
        }
    }
}
