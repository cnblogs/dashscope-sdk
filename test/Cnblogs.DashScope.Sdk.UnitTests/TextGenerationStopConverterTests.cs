using System.Text.Json;
using FluentAssertions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextGenerationStopConverterTests
{
    private static readonly JsonSerializerOptions SerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

    [Theory]
    [MemberData(nameof(Data))]
    public void TextGenerationStopConvertor_Serialize_Success(TextGenerationStop? stop, string json)
    {
        // Arrange
        var obj = new TestObj(stop);

        // Act
        var actual = JsonSerializer.Serialize(obj, SerializerOptions);

        // Assert
        actual.Should().Be(json);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void TextGenerationStopConvertor_Deserialize_Success(TextGenerationStop? stop, string json)
    {
        // Act
        var obj = JsonSerializer.Deserialize<TestObj>(json, SerializerOptions);

        // Assert
        obj.Should().BeEquivalentTo(new TestObj(stop));
    }

    public record TestObj(TextGenerationStop? Stop);

    public static TheoryData<TextGenerationStop?, string> Data
        => new()
        {
            { new TextGenerationStop("hello"), """{"stop":"hello"}""" },
            { new TextGenerationStop(["hello"]), """{"stop":["hello"]}""" },
            { new TextGenerationStop([12, 334]), """{"stop":[12,334]}""" },
            { new TextGenerationStop([[12, 334]]), """{"stop":[[12,334]]}""" },
            { null, """{"stop":null}""" }
        };
}
