using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// JSON convertor for <see cref="TextGenerationStop"/>.
/// </summary>
internal class TextGenerationStopConvertor : JsonConverter<TextGenerationStop>
{
    /// <inheritdoc />
    public override TextGenerationStop? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString()!,
            JsonTokenType.Null => null,
            JsonTokenType.StartArray => ReadArray(ref reader),
            _ => throw new JsonException(
                "Invalid token for TextGenerationStop, valid type is int, string, int array, string array")
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TextGenerationStop value, JsonSerializerOptions options)
    {
        if (value.StopString != null)
        {
            JsonSerializer.Serialize(writer, value.StopString, options);
            return;
        }

        if (value.StopStrings != null)
        {
            JsonSerializer.Serialize(writer, value.StopStrings, options);
            return;
        }

        if (value.StopToken != null)
        {
            JsonSerializer.Serialize(writer, value.StopToken, options);
            return;
        }

        if (value.StopTokens != null)
        {
            JsonSerializer.Serialize(writer, value.StopTokens, options);
            return;
        }

        throw new JsonException(
            $"Invalid {nameof(TextGenerationStop)} value, must be one of int, string, int array or string array");
    }

    private static TextGenerationStop ReadArray(ref Utf8JsonReader reader)
    {
        List<int>? intList = null;
        List<int[]>? tokenList = null;
        List<string>? stringList = null;

        // determine array type
        reader.Read();
        var type = reader.TokenType switch
        {
            JsonTokenType.StartArray => DeserializationArrayType.Tokens,
            JsonTokenType.String => DeserializationArrayType.Strings,
            JsonTokenType.Number => DeserializationArrayType.Token,
            _ => throw new JsonException("Invalid type in stop array, must be string or int array")
        };
        do
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.EndArray:
                    return type switch
                    {
                        DeserializationArrayType.Strings => stringList ?? [],
                        DeserializationArrayType.Token => intList?.ToArray() ?? [],
                        DeserializationArrayType.Tokens => tokenList ?? [],
                        _ => throw new JsonException("Impossible deserialization type")
                    };
                case JsonTokenType.StartArray when type is DeserializationArrayType.Tokens:
                    tokenList ??= [];
                    tokenList.Add(ReadTokenId(ref reader));
                    break;
                case JsonTokenType.Number when type is DeserializationArrayType.Token:
                    intList ??= [];
                    intList.Add(reader.GetInt32());
                    break;
                case JsonTokenType.String when type is DeserializationArrayType.Strings:
                    stringList ??= [];
                    stringList.Add(reader.GetString()!);
                    break;
                default:
                    throw new JsonException("Mixed content in stop array, can only be int array or string array");
            }
        }
        while (reader.Read());

        throw new JsonException("Wrong end for stop array");
    }

    private static int[] ReadTokenId(ref Utf8JsonReader reader)
    {
        var tokenId = new List<int>();
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.EndArray:
                    return tokenId.ToArray();
                case JsonTokenType.Number when reader.TryGetInt32(out var token):
                    tokenId.Add(token);
                    break;
                default:
                    throw new JsonException("Invalid token when deserializing token id");
            }
        }

        throw new JsonException("Wrong end for token id array");
    }

    private enum DeserializationArrayType
    {
        Token,
        Tokens,
        Strings
    }
}
