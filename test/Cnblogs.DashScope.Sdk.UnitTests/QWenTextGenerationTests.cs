using Cnblogs.DashScope.Sdk.QWen;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class QWenTextGenerationTests
{
    private const string CustomModel = "custom-model";

    [Fact]
    public async Task QWenCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        const string prompt = "hello";
        var parameters = new TextGenerationParameters() { EnableSearch = true, Seed = 1234 };

        // Act
        await client.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt, parameters);

        // Assert
        await client.Received()
            .GetTextCompletionAsync(
                Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                    s => s.Input.Prompt == prompt && s.Parameters == parameters && s.Model == "qwen-max"));
    }

    [Fact]
    public async Task QWenCompletion_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        const string prompt = "hello";
        var parameters = new TextGenerationParameters { EnableSearch = true, Seed = 1234 };

        // Act
        await client.GetQWenCompletionAsync(CustomModel, prompt, parameters);

        // Assert
        await client.Received()
            .GetTextCompletionAsync(
                Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                    s => s.Input.Prompt == prompt && s.Parameters == parameters && s.Model == CustomModel));
    }

    [Fact]
    public void QWenCompletionStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        const string prompt = "hello";
        var parameters = new TextGenerationParameters
        {
            EnableSearch = true,
            Seed = 1234,
            IncrementalOutput = true
        };

        // Act
        _ = client.GetQWenCompletionStreamAsync(QWenLlm.QWenPlus, prompt, parameters);

        // Assert
        _ = client.Received()
            .GetTextCompletionStreamAsync(
                Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                    s => s.Input.Prompt == prompt && s.Parameters == parameters && s.Model == "qwen-plus"));
    }

    [Fact]
    public void QWenCompletionStream_UseCustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        const string prompt = "hello";
        var parameters = new TextGenerationParameters
        {
            EnableSearch = true,
            Seed = 1234,
            IncrementalOutput = true
        };

        // Act
        _ = client.GetQWenCompletionStreamAsync(CustomModel, prompt, parameters);

        // Assert
        _ = client.Received()
            .GetTextCompletionStreamAsync(
                Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                    s => s.Input.Prompt == prompt && s.Parameters == parameters && s.Model == CustomModel));
    }

    [Fact]
    public async Task QWenChatCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var messages = new List<ChatMessage>() { new("system", "you are a helpful assistant"), new("user", "hello") };
        var parameters = new TextGenerationParameters() { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        await client.GetQWenChatCompletionAsync(QWenLlm.QWenMax1201, messages, parameters);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == messages && s.Parameters == parameters && s.Model == "qwen-max-1201"));
    }

    [Fact]
    public async Task QWenChatCompletion_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var messages = new List<ChatMessage> { new("system", "you are a helpful assistant"), new("user", "hello") };
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        await client.GetQWenChatCompletionAsync(CustomModel, messages, parameters);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == messages && s.Parameters == parameters && s.Model == CustomModel));
    }

    [Fact]
    public void QWenChatCompletionStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var messages = new List<ChatMessage> { new("system", "you are a helpful assistant"), new("user", "hello") };
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        _ = client.GetQWenChatStreamAsync(QWenLlm.QWenMaxLongContext, messages, parameters);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == messages && s.Parameters == parameters && s.Model == "qwen-max-longcontext"));
    }

    [Fact]
    public void QWenChatCompletionStream_CustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var messages = new List<ChatMessage> { new("system", "you are a helpful assistant"), new("user", "hello") };
        var parameters = new TextGenerationParameters { EnableSearch = true, ResultFormat = ResultFormats.Message };

        // Act
        _ = client.GetQWenChatStreamAsync(CustomModel, messages, parameters);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Input.Messages == messages && s.Parameters == parameters && s.Model == CustomModel));
    }
}
