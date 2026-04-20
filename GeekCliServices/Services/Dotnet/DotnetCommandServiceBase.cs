using GeekCliServices.Services.Dotnet.Models;

namespace GeekCliServices.Services.Dotnet
{
    public abstract class DotnetCommandServiceBase : ExternalProcessServiceBase<string>
    {
        protected int RunDotnet(string arguments)
        {
            return RunProcess("dotnet", arguments);
        }

        protected override string BuildArgs(string command)
        {
            return command;
        }

        protected static string BuildTemplateArgs(string shortName, string name, params DotnetTemplateOption[] options)
        {
            var parts = new List<string>
            {
                "new",
                shortName,
                "-n",
                Quote(name)
            };

            foreach (var option in options)
            {
                parts.Add(option.Option);
                parts.Add(Quote(option.Value));
            }

            return string.Join(" ", parts);
        }

        protected static string GetScopeSuffix(DotnetScope scope, bool allowCorpCoCode)
        {
            return scope switch
            {
                DotnetScope.Basic => string.Empty,
                DotnetScope.Corp => "-corp",
                DotnetScope.CorpCo => "-corp-co",
                DotnetScope.CorpCoCode when allowCorpCoCode => "-corp-co-code",
                _ => throw new InvalidOperationException($"Unsupported scope '{scope}'.")
            };
        }

        protected static string Quote(string value)
        {
            return $"\"{value.Replace("\"", "\\\"")}\"";
        }
    }
}
