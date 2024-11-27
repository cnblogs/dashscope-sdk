using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.AI;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ChatClientTests
{
    [Fact]
    public async Task ChatClient_TextCompletion_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleChatClientMessage;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetTextCompletionAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.CompleteAsync(
            content,
            new ChatOptions()
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                ToolMode = ChatToolMode.Auto
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                m => IsEquivalent(m, testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        response.Message.Text.Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageChatClientIncremental;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var returnThis = new[] { testCase.ResponseModel }.ToAsyncEnumerable();
        dashScopeClient
            .Configure()
            .GetTextCompletionStreamAsync(
                Arg.Any<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(returnThis);
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.CompleteStreamingAsync(
            content,
            new ChatOptions()
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
                StopSequences = ["你好"],
                ToolMode = ChatToolMode.Auto
            });
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionStreamAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                m => IsEquivalent(m, testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        text.ToString().Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }

    [Fact]
    public async Task ChatClient_ImageRecognition_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.MultimodalGeneration.VlChatClientNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                [new ImageContent(contents[0].Image!), new TextContent(contents[1].Text)])
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.CompleteAsync(
            messages,
            new ChatOptions
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
            });

        // Assert
        await dashScopeClient.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => IsEquivalent(m, testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        response.Choices[0].Text.Should()
            .BeEquivalentTo(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text);
    }

    [Fact]
    public async Task ChatClient_ImageRecognitionStream_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.MultimodalGeneration.VlChatClientSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        dashScopeClient.Configure()
            .GetMultimodalGenerationStreamAsync(
                Arg.Any<ModelRequest<MultimodalInput, IMultimodalParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(new[] { testCase.ResponseModel }.ToAsyncEnumerable());
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var contents = testCase.RequestModel.Input.Messages.Last().Content;
        var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                [new ImageContent(contents[0].Image!), new TextContent(contents[1].Text)])
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.CompleteStreamingAsync(
            messages,
            new ChatOptions()
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                PresencePenalty = parameter?.PresencePenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
                TopK = parameter?.TopK,
                TopP = parameter?.TopP,
            });
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => IsEquivalent(m, testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        text.ToString().Should().Be(testCase.ResponseModel.Output.Choices.First().Message.Content[0].Text);
    }

    private bool IsEquivalent<T>(T left, T right)
    {
        try
        {
            left.Should().BeEquivalentTo(right);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
}
