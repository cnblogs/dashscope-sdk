using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

internal class TextChatMessageContentConvertor : JsonConverter<TextChatMessageContent>
{
    /// <inheritdoc />
    public override TextChatMessageContent? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var s = reader.GetString();
            return s == null ? null : new TextChatMessageContent(s);
        }

        if (reader.TokenType == JsonTokenType.StartArray)
        {
            var contents = JsonSerializer.Deserialize<List<TextChatMessageContentInternal>>(ref reader, options);
            if (contents == null)
            {
                // impossible
                return null;
            }

            var textContent = contents.FirstOrDefault(x => string.IsNullOrEmpty(x.Text) == false)
                              ?? throw new JsonException("No text found in content array");
            var docUrlContent = contents.FirstOrDefault(x => x.DocUrl != null);
            return new TextChatMessageContent(
                textContent.Text!,
                docUrlContent?.DocUrl,
                docUrlContent?.FileParsingStrategy,
                textContent.CacheControl);
        }

        throw new JsonException("Unknown type for TextChatMessageContent");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TextChatMessageContent value, JsonSerializerOptions options)
    {
        if (value.DocUrls != null)
        {
            JsonSerializer.Serialize(
                writer,
                new List<TextChatMessageContentInternal>()
                {
                    new() { Type = "text", Text = value.Text, CacheControl = value.CacheControl },
                    new()
                    {
                        Type = "doc_url",
                        DocUrl = value.DocUrls.ToList(),
                        FileParsingStrategy = value.FileParsingStrategy
                    }
                },
                options);
        }
        else if (value.CacheControl != null)
        {
            JsonSerializer.Serialize(
                writer,
                new List<TextChatMessageContentInternal>()
                {
                    new() { Type = "text", Text = value.Text, CacheControl = value.CacheControl }
                },
                options);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Text, options);
        }
    }
}
