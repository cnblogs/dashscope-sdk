using System.Text.Json;
using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechRecognitionSpecialWordFilterJsonConverterTests
{
    [Fact]
    public void Write_Filter_SerializedAsJsonString()
    {
        // Arrange
        var parameters = new SpeechRecognitionParameters
        {
            Format = "pcm",
            SpecialWordFilter = new SpeechRecognitionSpecialWordFilter
            {
                FilterWithSigned = new SpeechRecognitionSpecialWordList { WordList = new List<string> { "测试" } },
                SystemReservedFilter = true
            }
        };

        // Act
        var json = JsonSerializer.Serialize(parameters, DashScopeDefaults.SerializationOptions);
        var node = JsonNode.Parse(json)!;

        // Assert
        // the value has to be a JSON string (the cast throws for a JSON object), which carries the filter object
        Assert.Equal(
            "{\"filter_with_signed\":{\"word_list\":[\"测试\"]},\"system_reserved_filter\":true}",
            (string)node["special_word_filter"]!);
    }

    [Fact]
    public void Read_JsonString_FilterRestored()
    {
        // Arrange
        const string json =
            @"{""format"":""pcm"",""sample_rate"":16000,""special_word_filter"":""{\""filter_with_empty\"":{\""word_list\"":[\""开始\""]},\""system_reserved_filter\"":true}""}";

        // Act
        var parameters = JsonSerializer.Deserialize<SpeechRecognitionParameters>(json, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equivalent(
            new SpeechRecognitionSpecialWordFilter
            {
                FilterWithEmpty = new SpeechRecognitionSpecialWordList { WordList = new List<string> { "开始" } },
                SystemReservedFilter = true
            },
            parameters!.SpecialWordFilter);
    }

    [Fact]
    public void Read_PlainJsonObject_ThrowsJsonException()
    {
        // Arrange
        const string json =
            @"{""format"":""pcm"",""sample_rate"":16000,""special_word_filter"":{""system_reserved_filter"":true}}";

        // Act
        var act = () => JsonSerializer.Deserialize<SpeechRecognitionParameters>(json, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Throws<JsonException>(act);
    }
}
