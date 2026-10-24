using GeekCli.Commands.Ngx.Wizard.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeekCli.Commands.Ngx.Wizard.Serialization
{
    internal sealed class NxProjectListEntryConverter : JsonConverter<NxProjectListEntry>
    {
        public override NxProjectListEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Nx project names must be JSON strings.");
            }

            return new NxProjectListEntry(reader.GetString() ?? string.Empty);
        }

        public override void Write(Utf8JsonWriter writer, NxProjectListEntry value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}
