using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.Llama2;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class Llama2TextGenerationApiTests
{
    [Fact]
    public async Task Llama2_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetLlama2TextCompletionAsync(Llama2Model.Chat13Bv2, Cases.TextMessages, ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(s
                => s.Input.Messages == Cases.TextMessages
                   && s.Model == "llama2-13b-chat-v2"
                   && s.Parameters != null
                   && s.Parameters.ResultFormat == ResultFormats.Message));
    }

    [Fact]
    public async Task Llama2_UseInvalidEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        var act = async () => await client.GetLlama2TextCompletionAsync(
            (Llama2Model)(-1),
            Cases.TextMessages,
            ResultFormats.Message);

        // Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public async Task Llama2_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetLlama2TextCompletionAsync(Cases.CustomModelName, Cases.TextMessages, ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(s
                => s.Input.Messages == Cases.TextMessages
                   && s.Model == Cases.CustomModelName
                   && s.Parameters != null
                   && s.Parameters.ResultFormat == ResultFormats.Message));
    }
}
