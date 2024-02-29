using Cnblogs.DashScope.Sdk.Llama2;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class Llama2TextGenerationTests
{
    private static readonly List<ChatMessage> Messages =
        [new("system", "you are a helpful assistant"), new("user", "hello")];

    [Fact]
    public async Task Llama2_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetLlama2TextCompletionAsync(Llama2Model.Chat13Bv2, Messages, ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == Messages
                     && s.Model == "llama2-13b-chat-v2"
                     && s.Parameters != null
                     && s.Parameters.ResultFormat == ResultFormats.Message));
    }

    [Fact]
    public async Task Llama2_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetLlama2TextCompletionAsync("custom-model", Messages, ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == Messages
                     && s.Model == "custom-model"
                     && s.Parameters != null
                     && s.Parameters.ResultFormat == ResultFormats.Message));
    }
}
