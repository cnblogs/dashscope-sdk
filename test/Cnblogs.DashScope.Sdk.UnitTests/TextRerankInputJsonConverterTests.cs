using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextRerankInputJsonConverterTests
{
    private const string Image = "https://www.example.com/image.png";
    private const string Video = "https://www.example.com/video.mp4";

    [Fact]
    public void TextRerankInputConvertor_SerializeTextDocuments_WritesStrings()
    {
        // Arrange
        var input = new TextRerankInput
        {
            Query = "query",
            Documents = new List<TextRerankContent> { "first", new TextRerankContent(Text: "second") }
        };

        // Act
        var json = JsonSerializer.Serialize(input, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal("{\"query\":\"query\",\"documents\":[\"first\",\"second\"]}", json);
    }

    [Fact]
    public void TextRerankInputConvertor_SerializeMultimodalDocuments_WritesObjects()
    {
        // Arrange
        var input = new TextRerankInput
        {
            Query = "query",
            Documents = new List<TextRerankContent>
            {
                "text",
                new TextRerankContent(Image: Image),
                new TextRerankContent(Video: Video)
            }
        };

        // Act
        var json = JsonSerializer.Serialize(input, DashScopeDefaults.SerializationOptions);

        // Assert
        // the text document switches to the object form along with the others
        Assert.Equal(
            "{\"query\":\"query\",\"documents\":[{\"text\":\"text\"},{\"image\":\"" + Image
            + "\"},{\"video\":\"" + Video + "\"}]}",
            json);
    }

    [Fact]
    public void TextRerankInputConvertor_SerializeImageQuery_WritesObject()
    {
        // Arrange
        var input = new TextRerankInput { Query = new TextRerankContent(Image: Image), Documents = new() { "text" } };

        // Act
        var json = JsonSerializer.Serialize(input, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal("{\"query\":{\"image\":\"" + Image + "\"},\"documents\":[\"text\"]}", json);
    }

    [Fact]
    public void TextRerankInputConvertor_SerializeDocumentWithTextAndImage_WritesBothMembers()
    {
        // Arrange
        var input = new TextRerankInput
        {
            Query = "query",
            Documents = new List<TextRerankContent> { new(Text: "text", Image: Image) }
        };

        // Act
        var json = JsonSerializer.Serialize(input, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal("{\"query\":\"query\",\"documents\":[{\"text\":\"text\",\"image\":\"" + Image + "\"}]}", json);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void TextRerankInputConvertor_Deserialize_Success(string json, TextRerankInput expected)
    {
        // Act
        var actual = JsonSerializer.Deserialize<TextRerankInput>(json, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equivalent(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void TextRerankInputConvertor_RoundTrip_Success(string json, TextRerankInput expected)
    {
        // Act
        var actual = JsonSerializer.Serialize(
            JsonSerializer.Deserialize<TextRerankInput>(json, DashScopeDefaults.SerializationOptions),
            DashScopeDefaults.SerializationOptions);

        // Assert
        // the reader accepts the forms it never writes (an object text query, mixed documents)
        Assert.Equal(
            JsonSerializer.Serialize(expected, DashScopeDefaults.SerializationOptions),
            actual);
    }

    [Theory]
    [InlineData("{\"query\":1,\"documents\":[\"text\"]}")]
    [InlineData("{\"query\":\"query\",\"documents\":\"text\"}")]
    [InlineData("{\"query\":\"query\",\"documents\":[1]}")]
    [InlineData("[]")]
    public void TextRerankInputConvertor_InvalidJson_Exception(string json)
    {
        // Act
        var act = () => JsonSerializer.Deserialize<TextRerankInput>(json, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Throws<JsonException>(act);
    }

    public static TheoryData<string, TextRerankInput> Data
        => new()
        {
            {
                "{\"query\":\"query\",\"documents\":[\"first\",\"second\"]}",
                new TextRerankInput { Query = "query", Documents = new() { "first", "second" } }
            },
            {
                "{\"query\":{\"text\":\"query\"},\"documents\":[{\"text\":\"first\"},{\"image\":\"" + Image + "\"}]}",
                new TextRerankInput
                {
                    Query = "query",
                    Documents = new() { "first", new TextRerankContent(Image: Image) }
                }
            },
            {
                "{\"query\":{\"image\":\"" + Image + "\"},\"documents\":[\"first\",{\"video\":\"" + Video + "\"}]}",
                new TextRerankInput
                {
                    Query = new TextRerankContent(Image: Image),
                    Documents = new() { "first", new TextRerankContent(Video: Video) }
                }
            }
        };
}
