using System.Text.Json.Serialization;

namespace GeekCli.Commands.Ngx.Wizard.Models
{
    internal sealed record NxProjectConfiguration(
        [property: JsonPropertyName("targets")] Dictionary<string, NxProjectTarget>? Targets);
}
