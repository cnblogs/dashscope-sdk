using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals
{
    internal class MultimodalMessageVideoContentJsonConverter : JsonConverter<MultimodalMessageVideoContent>
    {
        /// <inheritdoc />
        public override MultimodalMessageVideoContent? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => ReadFromString(reader.GetString()),
                JsonTokenType.Null => null,
                JsonTokenType.StartArray => ReadFromArray(ref reader, options),
                _ => throw new JsonException("Invalid type in stop array, must be string or string array")
            };
        }

        /// <inheritdoc />
        public override void Write(
            Utf8JsonWriter writer,
            MultimodalMessageVideoContent value,
            JsonSerializerOptions options)
        {
            if (value.Type == MultimodalMessageVideoContentType.Video)
            {
                JsonSerializer.Serialize(writer, value.Urls.FirstOrDefault() ?? string.Empty, options);
            }
            else if (value.Type == MultimodalMessageVideoContentType.FrameSequence)
            {
                JsonSerializer.Serialize(writer, value.Urls, options);
            }
            else
            {
                throw new JsonException("Invalid video content type, must be Video or FrameSequence");
            }
        }

        private static MultimodalMessageVideoContent? ReadFromArray(
            ref Utf8JsonReader reader,
            JsonSerializerOptions options)
        {
            var list = JsonSerializer.Deserialize<List<string>>(ref reader, options);
            if (list is null)
            {
                return null;
            }

            return MultimodalMessageVideoContent.FrameSequence(list);
        }

        private static MultimodalMessageVideoContent? ReadFromString(string? url)
        {
            if (url == null)
            {
                return null;
            }

            return MultimodalMessageVideoContent.Video(url);
        }
    }
}
