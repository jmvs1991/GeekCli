using System.Globalization;

namespace GeekCliServices.Services
{
    public abstract class ScaffoldingServiceBase<TCommand>
    {
        protected readonly string _basePath = Directory.GetCurrentDirectory();

        protected readonly TextInfo _textInfo = CultureInfo.InvariantCulture.TextInfo;

        protected void CreateDirectory(string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                CommandOutput.Info($"Created folder: {targetPath}");
            }
            else
            {
                CommandOutput.Info($"Skipped (already exists): {targetPath}");
            }
        }

        protected void CreateSubfolder(string parent, string name)
        {
            string path = Path.Combine(parent, name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                CommandOutput.Info($"Created: {name}");
            }

            string keepFilePath = Path.Combine(path, ".keep");
            File.WriteAllText(keepFilePath, string.Empty);
            CommandOutput.Info($"Added: {name}/.keep");
        }

        protected void CreateFile(string path, string filename, string content)
        {
            string filePath = Path.Combine(path, filename);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, content);
                CommandOutput.Info($"Generated: {filename}");
            }
            else
            {
                CommandOutput.Info($"Skipped (already exists): {filename}");
            }
        }

        public abstract int RunProcess(string processToRun, TCommand command);

        protected abstract void Execute(string targetPath, string name, TCommand command);
    }
}
