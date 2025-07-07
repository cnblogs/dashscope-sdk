using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

internal class ByteArrayLiteralConvertor : JsonConverter<byte[]>
{
    /// <inheritdoc />
    public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            reader.Read(); // read out start of array
            var list = new List<byte>(8); // should fit most tokens
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                list.Add(reader.GetByte());
                reader.Read();
            }

            return list.ToArray();
        }

        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        return reader.GetBytesFromBase64();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var b in value)
        {
            writer.WriteNumberValue(b);
        }

        writer.WriteEndArray();
    }
}
