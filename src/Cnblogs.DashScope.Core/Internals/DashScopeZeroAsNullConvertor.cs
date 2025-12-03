using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals
{
    internal class DashScopeZeroAsNullConvertor : JsonConverter<string>
    {
        /// <inheritdoc />
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null or JsonTokenType.None)
            {
                return null;
            }

            if (reader.TokenType is JsonTokenType.String)
            {
                return reader.GetString();
            }

            if (reader.TokenType is JsonTokenType.Number)
            {
                return reader.GetInt32() == 0 ? null : throw new JsonException("Invalid number for string");
            }

            throw new JsonException("Invalid type for string");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
