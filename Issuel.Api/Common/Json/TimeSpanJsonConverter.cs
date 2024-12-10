using System.Text.Json;
using System.Text.Json.Serialization;

namespace Issuel.Api.Common.Json;

/// <summary>
/// Конвертер для типа TimeSpan?
/// </summary>
public class TimeSpanJsonConverter : JsonConverter<TimeSpan?>
{
    /// <summary>
    /// Прочитать TimeSpan?
    /// </summary>
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;

            case JsonTokenType.String:
            {
                var value = reader.GetString();
                if (TimeSpan.TryParse(value, out var result))
                    return result;

                throw new JsonException("Invalid TimeSpan format.");
            }
            default:
                throw new JsonException("Unexpected token type.");
        }
    }

    /// <summary>
    /// Записать TimeSpan?
    /// </summary>
    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(@"hh\:mm\:ss"));
        else
            writer.WriteNullValue();
    }
}