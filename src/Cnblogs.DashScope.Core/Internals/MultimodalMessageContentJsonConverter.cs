using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Reads the content of a multimodal message. The service sends it as an array of contents, except
/// for the frame that opens the web search stream of qwen3.8-omni-flash on the shared domain, whose
/// content is a plain string: an empty string is an empty content, any other string is a text
/// content. The content of a request is always an array, writing it stays with the default
/// serialization.
/// </summary>
internal class MultimodalMessageContentJsonConverter : JsonConverter<IReadOnlyList<MultimodalMessageContent>>
{
    /// <inheritdoc />
    public override IReadOnlyList<MultimodalMessageContent>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            return JsonSerializer.Deserialize<List<MultimodalMessageContent>>(ref reader, options);
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var text = reader.GetString();
            if (string.IsNullOrEmpty(text))
            {
                return new List<MultimodalMessageContent>();
            }

            return new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent(text) };
        }

        throw new JsonException("Unknown type for the content of a multimodal message");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, IReadOnlyList<MultimodalMessageContent> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
