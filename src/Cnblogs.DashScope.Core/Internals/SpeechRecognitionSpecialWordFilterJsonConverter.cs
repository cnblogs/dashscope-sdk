using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads and writes <see cref="SpeechRecognitionSpecialWordFilter"/> as a JSON string. The
/// speech recognition server rejects the plain JSON object with "parse parameters failed" and
/// ignores a string that is not JSON, the filter is only applied when the object is JSON-encoded
/// as a string.
/// </summary>
internal class SpeechRecognitionSpecialWordFilterJsonConverter : JsonConverter<SpeechRecognitionSpecialWordFilter>
{
    /// <inheritdoc />
    public override SpeechRecognitionSpecialWordFilter? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null or JsonTokenType.None)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Invalid type for special word filter, must be a JSON string");
        }

        return JsonSerializer.Deserialize<SpeechRecognitionSpecialWordFilter>(reader.GetString()!, options);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SpeechRecognitionSpecialWordFilter value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(JsonSerializer.Serialize(value, options));
    }
}
