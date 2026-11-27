using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ModelResponseJsonConverterTests
{
    // the default frame the gateway opens the web search stream of qwen3.8-omni-flash with:
    // the output without the envelope of a response
    private const string BareOutputJson =
        "{\"choices\":[{\"finish_reason\":\"null\",\"message\":{\"role\":\"assistant\",\"content\":\"\"}}]}";

    private const string ResponseJson = "{\"request_id\":\"42\",\"output\":{\"choices\":[]}}";

    [Fact]
    public void Deserialize_BareOutput_ResponseHoldingTheOutput()
    {
        // Arrange
        // Act
        var response = JsonSerializer.Deserialize<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(
            BareOutputJson,
            DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(string.Empty, response.RequestId);
        Assert.Null(response.Usage);
        Assert.Equal("null", response.Output.Choices[0].FinishReason);
        Assert.Equal("assistant", response.Output.Choices[0].Message.Role);
        Assert.Empty(response.Output.Choices[0].Message.Content);
    }

    [Fact]
    public void Deserialize_ResponseWithoutOutput_OutputStaysNull()
    {
        // Arrange
        // only the bare output is mapped, a response that lost its output reads as it always did
        const string json = "{\"request_id\":\"42\",\"usage\":{\"input_tokens\":1}}";

        // Act
        var response = JsonSerializer.Deserialize<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(
            json,
            DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Null(response!.Output);
        Assert.Equal("42", response.RequestId);
        Assert.Equal(1, response.Usage!.InputTokens);
    }

    [Fact]
    public void Deserialize_Response_EnvelopeUnchanged()
    {
        // Arrange
        // Act
        var response = JsonSerializer.Deserialize<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>(
            ResponseJson,
            DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal("42", response!.RequestId);
        Assert.Empty(response.Output.Choices);
    }

    [Fact]
    public void Serialize_Response_EnvelopeJson()
    {
        // Arrange
        var response = new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
        {
            RequestId = "42",
            Output = new MultimodalOutput(new List<MultimodalChoice>())
        };

        // Act
        var json = JsonSerializer.Serialize(response, DashScopeDefaults.SerializationOptions);

        // Assert
        Assert.Equal("{\"output\":{\"choices\":[]},\"request_id\":\"42\"}", json);
    }
}
