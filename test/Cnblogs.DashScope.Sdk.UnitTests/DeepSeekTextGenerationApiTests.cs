﻿using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.DeepSeek;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DeepSeekTextGenerationApiTests
{
    [Fact]
    public async Task TextCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        await client.GetDeepSeekChatCompletionAsync(DeepSeekLlm.DeepSeekR1, [TextChatMessage.User("你好")]);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                x => x.Model == "deepseek-r1" && x.Input.Messages!.First().Content == "你好" && x.Parameters == null));
    }

    [Fact]
    public async Task TextCompletion_UseCustomModel_SuccessAsync()
    {
        // Arrange
        const string customModel = "deepseek-v3";
        var client = Substitute.For<IDashScopeClient>();

        // Act
        await client.GetDeepSeekChatCompletionAsync(customModel, [TextChatMessage.User("你好")]);

        // Assert
        await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                x => x.Model == customModel && x.Input.Messages!.First().Content == "你好" && x.Parameters == null));
    }

    [Fact]
    public void StreamCompletion_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = client.GetDeepSeekChatCompletionStreamAsync(DeepSeekLlm.DeepSeekV3, [TextChatMessage.User("你好")]);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                x => x.Model == "deepseek-v3"
                     && x.Input.Messages!.First().Content == "你好"
                     && x.Parameters!.IncrementalOutput == true));
    }

    [Fact]
    public void StreamCompletion_CustomModel_SuccessAsync()
    {
        // Arrange
        const string customModel = "deepseek-v3";
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = client.GetDeepSeekChatCompletionStreamAsync(customModel, [TextChatMessage.User("你好")]);

        // Assert
        _ = client.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                x => x.Model == customModel
                     && x.Input.Messages!.First().Content == "你好"
                     && x.Parameters!.IncrementalOutput == true));
    }
}
