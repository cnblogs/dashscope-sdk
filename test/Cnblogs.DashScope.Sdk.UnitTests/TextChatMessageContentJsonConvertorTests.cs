using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextChatMessageContentJsonConvertorTests
{
    private const string TextJson = "\"some text\"";

    private const string DocUrlJson =
        "[{\"type\":\"text\",\"text\":\"some content\"},{\"type\":\"doc_url\",\"doc_url\":[\"url1\"],\"file_parsing_strategy\":\"auto\"}]";

    private const string CacheControlJson =
        "[{\"type\":\"text\",\"text\":\"some text\",\"cache_control\":{\"type\":\"ephemeral\"}}]";

    private const string DocUrlWithCacheControlJson =
        "[{\"type\":\"text\",\"text\":\"some content\",\"cache_control\":{\"type\":\"ephemeral\"}},{\"type\":\"doc_url\",\"doc_url\":[\"url1\"],\"file_parsing_strategy\":\"auto\"}]";

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
    public void Serialize_CacheControl_TextArrayAsync()
    {
        // Arrange
        var content = new TextChatMessageContent("some text", new CacheControlOptions());

        // Act
        var json = JsonSerializer.Serialize(content, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal(CacheControlJson, json);
    }

    [Fact]
    public void Serialize_DocUrlAndCacheControl_TextArrayAsync()
    {
        // Arrange
        var content = new TextChatMessageContent("some content", new[] { "url1" }, "auto", new CacheControlOptions());

        // Act
        var json = JsonSerializer.Serialize(content, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal(DocUrlWithCacheControlJson, json);
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

    [Fact]
    public void Deserialize_CacheControl_SetCacheControlAsync()
    {
        // Act
        var content = JsonSerializer.Deserialize<TextChatMessageContent>(
            CacheControlJson,
            DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.NotNull(content);
        Assert.Equal("some text", content.Text);
        Assert.NotNull(content.CacheControl);
        Assert.Equal("ephemeral", content.CacheControl.Type);
        Assert.Null(content.DocUrls);
    }

    [Fact]
    public void Deserialize_TextArrayWithoutDocUrl_SetTextAsync()
    {
        // Arrange
        const string json = "[{\"type\":\"text\",\"text\":\"some text\"}]";

        // Act
        var content = JsonSerializer.Deserialize<TextChatMessageContent>(
            json,
            DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.NotNull(content);
        Assert.Equal("some text", content.Text);
        Assert.Null(content.DocUrls);
        Assert.Null(content.CacheControl);
    }
}
