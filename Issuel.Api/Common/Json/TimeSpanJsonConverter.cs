using System.Text.Json;
using System.Text.Json.Serialization;

namespace Issuel.Api.Common.Json;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (TimeSpan.TryParse(value, out var result))
                return result;

            throw new JsonException("Invalid TimeSpan format.");
        }

        throw new JsonException("Unexpected token type.");
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(@"hh\:mm\:ss"));
        else
            writer.WriteNullValue();
    }
}