using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class TextChatMessageContentJsonConvertorTests
    {
        private const string TextJson = "\"some text\"";

        private const string DocUrlJson =
            "[{\"type\":\"text\",\"text\":\"some content\"},{\"type\":\"doc_url\",\"doc_url\":[\"url1\"],\"file_parsing_strategy\":\"auto\"}]";

        [Fact]
        public void Serialize_Text_StringAsync()
        {
            // Arrange
            var content = new TextChatMessageContent("some text");

            // Act
            var json = JsonSerializer.Serialize(content, DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.Equal(TextJson, json);
        }

        [Fact]
        public void Serialize_DocUrl_ObjectAsync()
        {
            // Arrange
            var content = new TextChatMessageContent("some content", new[] { "url1" }, "auto");

            // Act
            var json = JsonSerializer.Serialize(content, DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.Equal(DocUrlJson, json);
        }

        [Fact]
        public void Deserialize_InvalidType_ThrowAsync()
        {
            // Arrange
            const string errJson = "{}";

            // Act
            var act = () => JsonSerializer.Deserialize<TextChatMessageContent>(
                errJson,
                DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.Throws<JsonException>(act);
        }

        [Fact]
        public void Deserialize_InvalidArray_ThrowAsync()
        {
            // Arrange
            const string errJson = "[{\"type\":\"doc_url\", \"doc_url\":[]}]";

            // Act
            var act = () => JsonSerializer.Deserialize<TextChatMessageContent>(
                errJson,
                DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.Throws<JsonException>(act);
        }

        [Fact]
        public void Deserialize_Text_SetTextOnlyAsync()
        {
            // Act
            var content = JsonSerializer.Deserialize<TextChatMessageContent>(
                TextJson,
                DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.NotNull(content);
            Assert.Equal("some text", content.Text);
            Assert.Null(content.DocUrls);
        }

        [Fact]
        public void Deserialize_DocUrl_SetUrlsAsync()
        {
            // Act
            var content = JsonSerializer.Deserialize<TextChatMessageContent>(
                DocUrlJson,
                DashScopeDefaults.SerializationOptions);

            // Assert
            Assert.NotNull(content);
            Assert.Equal("some content", content.Text);
            Assert.Equivalent(new[] { "url1" }, content.DocUrls);
        }
    }
}
