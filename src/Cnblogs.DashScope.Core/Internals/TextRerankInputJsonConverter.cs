using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads and writes <see cref="TextRerankInput"/>. The query is a plain string for a text and an
/// object otherwise; the documents are an array of strings when all of them are texts (the only
/// form gte-rerank-v2 accepts), or an array of objects otherwise, because qwen3-vl-rerank rejects
/// an array that mixes strings and objects.
/// </summary>
internal class TextRerankInputJsonConverter : JsonConverter<TextRerankInput>
{
    /// <inheritdoc />
    public override TextRerankInput? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Invalid type for text rerank input, must be an object");
        }

        TextRerankContent? query = null;
        var documents = new List<TextRerankContent>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }

            var name = reader.GetString();
            reader.Read();
            switch (name)
            {
                case "query":
                    query = ReadContent(ref reader, options);
                    break;
                case "documents":
                    documents = ReadContents(ref reader, options);
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        return new TextRerankInput { Query = query ?? new TextRerankContent(), Documents = documents };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TextRerankInput value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("query");
        if (IsText(value.Query))
        {
            writer.WriteStringValue(value.Query.Text);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Query, options);
        }

        writer.WritePropertyName("documents");
        writer.WriteStartArray();
        var asObjects = value.Documents.Any(d => IsText(d) == false);
        foreach (var document in value.Documents)
        {
            if (asObjects == false)
            {
                writer.WriteStringValue(document.Text);
            }
            else
            {
                JsonSerializer.Serialize(writer, document, options);
            }
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }

    private static bool IsText(TextRerankContent content)
        => content.Image == null && content.Video == null && content.Text != null;

    private static TextRerankContent? ReadContent(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => new TextRerankContent(reader.GetString()),
            JsonTokenType.Null => null,
            JsonTokenType.StartObject => JsonSerializer.Deserialize<TextRerankContent>(ref reader, options),
            _ => throw new JsonException("Invalid type for text rerank content, must be a string or an object")
        };
    }

    private static List<TextRerankContent> ReadContents(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Invalid type for text rerank documents, must be an array");
        }

        var contents = new List<TextRerankContent>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return contents;
            }

            if (ReadContent(ref reader, options) is { } content)
            {
                contents.Add(content);
            }
        }

        throw new JsonException("Wrong end for text rerank documents");
    }
}
