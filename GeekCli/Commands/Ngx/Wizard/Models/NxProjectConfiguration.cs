using System.Text.Json.Serialization;

namespace GeekCli.Commands.Ngx.Wizard.Models
{
    internal sealed record NxProjectConfiguration(
        [property: JsonPropertyName("targets")] Dictionary<string, NxProjectTarget>? Targets,
        [property: JsonPropertyName("projectType")] string? ProjectType,
        [property: JsonPropertyName("tags")] string[]? Tags);
}
