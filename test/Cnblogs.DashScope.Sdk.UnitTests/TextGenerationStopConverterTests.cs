using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
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
            Assert.Equal(json, actual);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void TextGenerationStopConvertor_Deserialize_Success(TextGenerationStop? stop, string json)
        {
            // Act
            var obj = JsonSerializer.Deserialize<TestObj>(json, SerializerOptions);

            // Assert
            Assert.Equivalent(new TestObj(stop), obj);
        }

        [Theory]
        [MemberData(nameof(InvalidJson))]
        public void TextGenerationStopConvertor_InvalidJson_Exception(string json)
        {
            // Act
            var act = () => JsonSerializer.Deserialize<TestObj>(json, SerializerOptions);

            // Assert
            Assert.Throws<JsonException>(act);
        }

        public record TestObj(TextGenerationStop? Stop);

        public static TheoryData<TextGenerationStop?, string> Data
            => new()
            {
                { new TextGenerationStop("hello"), @"{""stop"":""hello""}" },
                {
                    new TextGenerationStop(new List<string> { "hello", "world" }),
                    "{\"stop\":[\"hello\",\"world\"]}"
                },
                { new TextGenerationStop(new[] { 12, 334 }), "{\"stop\":[12,334]}" },
                {
                    new TextGenerationStop(new List<int[]> { new[] { 12, 334 } }),
                    "{\"stop\":[[12,334]]}"
                },
                { null, "{\"stop\":null}" }
            };

        public static TheoryData<string> InvalidJson
            => new()
            {
                "{\"stop\":{}}",
                "{\"stop\":[1234,\"hello\"]}",
                "{\"stop\":[\"hello\"}}",
                "{\"stop\":[[34243,\"hello\"]]}",
                "{\"stop\":[[34243,123]}}"
            };
    }
}
