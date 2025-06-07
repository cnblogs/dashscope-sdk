using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using FluentAssertions;
using Microsoft.Extensions.AI;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.AI.UnitTests;

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
        var response = await client.GetResponseAsync(
            content,
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
                ToolMode = ChatToolMode.Auto
            });

        // Assert
        _ = dashScopeClient.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, ITextGenerationParameters>>(
                m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        response.Messages[0].Text.Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_SuccessAsync()
    {
        // Arrange
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
        var response = client.GetStreamingResponseAsync(
            content,
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
                StopSequences = new List<string> { "你好" },
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
                m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        text.ToString().Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }

    [Fact]
    public async Task ChatClient_ImageRecognition_SuccessAsync()
    {
        // Arrange
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
                new List<AIContent>
                {
                    new DataContent(contents[0].Image!, "image/png"), new TextContent(contents[1].Text)
                }),
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.GetResponseAsync(
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
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        response.Messages[0].Text.Should()
            .BeEquivalentTo(testCase.ResponseModel.Output.Choices[0].Message.Content[0].Text);
    }

    [Fact]
    public async Task ChatClient_ImageRecognitionStream_SuccessAsync()
    {
        // Arrange
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
                new List<AIContent>
                {
                    new DataContent(contents[0].Image!, "image/png"), new TextContent(contents[1].Text)
                })
        };
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.GetStreamingResponseAsync(
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
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        _ = dashScopeClient.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, IMultimodalParameters>>(m => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        text.ToString().Should().Be(testCase.ResponseModel.Output.Choices.First().Message.Content[0].Text);
    }
}
