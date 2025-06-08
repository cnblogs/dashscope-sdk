using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.QWen;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class QWenTextGenerationApiTests
{
    private const string CustomModel = "custom-model";

    private static readonly TextGenerationParameters IncrementalOutputParameters = new()
    {
        EnableSearch = true,
        Seed = 1234,
        IncrementalOutput = true
    };

    [Fact]
    public async Task QWenCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, Seed = 1234 };

        // Act
        await client.GetQWenCompletionAsync(QWenLlm.QWenMax, Cases.Prompt, parameters);

        // Assert
        await client.Received()
            .GetTextCompletionAsync(
                Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                    s => s.Input.Prompt == Cases.Prompt && s.Parameters == parameters && s.Model == "qwen-max"));
    }

    [Fact]
    public async Task QWenCompletion_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, Seed = 1234 };

        // Act
        await client.GetQWenCompletionAsync(CustomModel, Cases.Prompt, parameters);

        // Assert
        await client.Received()
            .GetTextCompletionAsync(
                Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                    s => s.Input.Prompt == Cases.Prompt && s.Parameters == parameters && s.Model == CustomModel));
    }

    [Fact]
    public void QWenCompletionStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = client.GetQWenCompletionStreamAsync(QWenLlm.QWenPlus, Cases.Prompt, IncrementalOutputParameters);

        // Assert
        _ = client.Received()
            .GetTextCompletionStreamAsync(
                Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                    s => s.Input.Prompt == Cases.Prompt
                         && s.Parameters == IncrementalOutputParameters
                         && s.Model == "qwen-plus"));
    }

    [Fact]
    public void QWenCompletionStream_UseCustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = client.GetQWenCompletionStreamAsync(CustomModel, Cases.Prompt, IncrementalOutputParameters);

        // Assert
        _ = client.Received()
            .GetTextCompletionStreamAsync(
                Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                    s => s.Input.Prompt == Cases.Prompt
                         && s.Parameters == IncrementalOutputParameters
                         && s.Model == CustomModel));
    }

    [Fact]
    public async Task QWenChatCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax1201, Cases.TextMessages, parameters);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                s => s.Input.Messages == Cases.TextMessages && s.Parameters == parameters && s.Model == "qwen-max-1201"));
    }

    [Fact]
    public async Task QWenChatCompletion_UseInvalidEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        var act = async () => await client.GetQWenChatCompletionAsync((QWenLlm)(-1), Cases.TextMessages, parameters);

        // Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public async Task QWenChatCompletion_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        await client.GetQWenChatCompletionAsync(CustomModel, Cases.TextMessages, parameters);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                s => s.Input.Messages == Cases.TextMessages && s.Parameters == parameters && s.Model == CustomModel));
    }

    [Fact]
    public void QWenChatCompletionStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        _ = client.GetQWenChatStreamAsync(QWenLlm.QWenMaxLongContext, Cases.TextMessages, parameters);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                s => s.Input.Messages == Cases.TextMessages
                     && s.Parameters == parameters
                     && s.Model == "qwen-max-longcontext"));
    }

    [Fact]
    public void QWenChatCompletionStream_CustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        _ = client.GetQWenChatStreamAsync(CustomModel, Cases.TextMessages, parameters);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                s => s.Input.Messages == Cases.TextMessages && s.Parameters == parameters && s.Model == CustomModel));
    }
}
