using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests
{
    public class ToolChoiceJsonConverterTests
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        [Theory]
        [MemberData(nameof(Data))]
        public void TextGenerationStopConvertor_Serialize_Success(ToolChoice? choice, string json)
        {
            // Arrange
            var obj = new TestObj(choice);

            // Act
            var actual = JsonSerializer.Serialize(obj, SerializerOptions);

            // Assert
            Assert.Equal(json, actual);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void TextGenerationStopConvertor_Deserialize_Success(ToolChoice? choice, string json)
        {
            // Act
            var obj = JsonSerializer.Deserialize<TestObj>(json, SerializerOptions);

            // Assert
            Assert.Equivalent(new TestObj(choice), obj);
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

        public record TestObj(ToolChoice? Choice);

        public static TheoryData<ToolChoice?, string> Data
            => new()
            {
                { ToolChoice.AutoChoice, "{\"choice\":\"auto\"}" },
                { ToolChoice.NoneChoice, "{\"choice\":\"none\"}" },
                { ToolChoice.FunctionChoice("weather"), "{\"choice\":{\"type\":\"function\",\"function\":{\"name\":\"weather\"}}}" },
                { null, "{\"choice\":null}" }
            };

        public static TheoryData<string> InvalidJson
            => new()
            {
                "{\"choice\":{}}",
                "{\"choice\":\"other\"}",
                "{\"choice\":{\"type\":\"other\"}}",
                "{\"choice\":{\"type\":\"other\", \"function\":{\"name\": \"weather\"}}}",
                "{\"choice\":{\"type\":\"function\", \"function\": \"other\"}}"
            };
    }
}
