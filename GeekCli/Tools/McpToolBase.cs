using GeekCliServices.Services;
using GeekCliServices.Services.Dotnet;
using GeekCliServices.Services.Dotnet.Models;

namespace GeekCli.Tools
{
    public abstract class McpToolBase
    {
        protected McpToolResult Capture(Func<int> action)
        {
            var output = new BufferingCommandOutputSink();

            using (CommandOutput.Push(output))
            {
                var exitCode = action();
                return new McpToolResult(exitCode, output.ToString());
            }
        }

        protected DotnetScope ParseScope(string scope)
        {
            return DotnetScopeParser.Parse(scope);
        }
    }

    public sealed record McpToolResult(int ExitCode, string Output);
}
