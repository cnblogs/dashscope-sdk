using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads a string property that the service may send as either a JSON string or a JSON number,
/// e.g. the SR field of video synthesis usage: wan models send 720 while PixVerse models
/// send "720" / "4K" / "".
/// </summary>
internal class DashScopeStringConvertor : JsonConverter<string>
{
    /// <inheritdoc />
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null or JsonTokenType.None => null,
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => reader.GetDecimal().ToString(CultureInfo.InvariantCulture),
            _ => throw new JsonException("Invalid type for string")
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
