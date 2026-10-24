using System.Text.Json.Serialization;

namespace GeekCli.Commands.Ngx.Wizard.Models
{
    internal sealed record NxProjectTarget(
        [property: JsonPropertyName("executor")] string? Executor);
}
