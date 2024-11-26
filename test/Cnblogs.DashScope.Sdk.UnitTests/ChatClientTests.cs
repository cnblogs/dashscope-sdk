using System.Text;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.AI;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ChatClientTests
{
    [Fact]
    public async Task ChatClient_TextCompletion_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessage;
        var (dashScopeClient, handler) = await Sut.GetTestClientAsync(sse, testCase);
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = await client.CompleteAsync(
            content,
            new ChatOptions()
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
            });

        // Assert
        handler.Received().MockSend(
            Arg.Any<HttpRequestMessage>(),
            Arg.Any<CancellationToken>());
        response.Message.Text.Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }

    [Fact]
    public async Task ChatClient_TextCompletionStream_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageIncremental;
        var (dashScopeClient, handler) = await Sut.GetTestClientAsync(sse, testCase);
        var client = dashScopeClient.AsChatClient(testCase.RequestModel.Model);
        var content = testCase.RequestModel.Input.Messages!.First().Content;
        var parameter = testCase.RequestModel.Parameters;

        // Act
        var response = client.CompleteStreamingAsync(
            content,
            new ChatOptions()
            {
                FrequencyPenalty = parameter?.RepetitionPenalty,
                ModelId = testCase.RequestModel.Model,
                MaxOutputTokens = parameter?.MaxTokens,
                Seed = (long?)parameter?.Seed,
                Temperature = parameter?.Temperature,
            });
        var text = new StringBuilder();
        await foreach (var update in response)
        {
            text.Append(update.Text);
        }

        // Assert
        handler.Received().MockSend(
            Arg.Any<HttpRequestMessage>(),
            Arg.Any<CancellationToken>());
        text.ToString().Should().Be(testCase.ResponseModel.Output.Choices?.First().Message.Content);
    }
}
