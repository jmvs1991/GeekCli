using GeekCliServices.Services.EditorConfig.Models;

namespace GeekCliServices.Services.EditorConfig
{
    public sealed class EditorConfigService : IEditorConfigService
    {
        public int RunProcess(string processToRun, EditorConfigCommand command)
        {
            var template = command.ProjectType?.ToLowerInvariant() switch
            {
                EditorConfigCommand.CSharp => "CSharp",
                EditorConfigCommand.Angular => "Angular",
                EditorConfigCommand.React => "React",
                _ => null
            };
            if (template is null)
            {
                CommandOutput.Error("Project type must be csharp, angular, or react.");
                return 1;
            }

            try
            {
                var projectPath = Path.GetFullPath(command.ProjectPath);
                if (!Directory.Exists(projectPath))
                {
                    CommandOutput.Error($"Project directory does not exist: {projectPath}");
                    return 1;
                }

                var filePath = Path.Combine(projectPath, ".editorconfig");
                var templatePath = Path.Combine(AppContext.BaseDirectory, "Templates", "EditorConfig", template, "template.editorconfig");
                try
                {
                    File.Copy(templatePath, filePath, overwrite: false);
                }
                catch (IOException) when (File.Exists(filePath))
                {
                    CommandOutput.Info($"Skipped existing file: {filePath}");
                    return 0;
                }

                CommandOutput.Info($"Created: {filePath}");
                return 0;
            }
            catch (Exception exception) when (exception is ArgumentException or IOException or UnauthorizedAccessException or NotSupportedException)
            {
                CommandOutput.Error($"Could not create .editorconfig: {exception.Message}");
                return 1;
            }
        }
    }
}
