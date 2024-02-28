using System.Text;
using FluentAssertions;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextGenerationSerializationTests
{
    [Fact]
    public async Task SingleCompletion_TextFormatNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextGeneration.TextFormat.SinglePrompt;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Fact]
    public async Task SingleCompletion_TextFormatSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.TextFormat.SinglePromptIncremental;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Text));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        outputs.SkipLast(1).Should().AllSatisfy(x => x.Output.FinishReason.Should().Be("null"));
        outputs.Last().Should().BeEquivalentTo(testCase.ResponseModel, o => o.Excluding(y => y.Output.Text));
        message.ToString().Should().Be(testCase.ResponseModel.Output.Text);
    }

    [Fact]
    public async Task SingleCompletion_MessageFormatNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessage;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetTextCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        response.Should().BeEquivalentTo(testCase.ResponseModel);
    }

    [Fact]
    public async Task SingleCompletion_MessageFormatSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.MessageFormat.SingleMessageIncremental;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Choices![0].Message.Content));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        outputs.SkipLast(1).Should().AllSatisfy(x => x.Output.Choices![0].FinishReason.Should().Be("null"));
        outputs.Last().Should().BeEquivalentTo(testCase.ResponseModel, o => o.Excluding(y => y.Output.Choices![0].Message.Content));
        message.ToString().Should().Be(testCase.ResponseModel.Output.Choices![0].Message.Content);
    }

    [Fact]
    public async Task ConversationCompletion_MessageFormatSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.TextGeneration.MessageFormat.ConversationMessageIncremental;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var message = new StringBuilder();
        var outputs = await client.GetTextCompletionStreamAsync(testCase.RequestModel).ToListAsync();
        outputs.ForEach(x => message.Append(x.Output.Choices![0].Message.Content));

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        outputs.SkipLast(1).Should().AllSatisfy(x => x.Output.Choices![0].FinishReason.Should().Be("null"));
        outputs.Last().Should().BeEquivalentTo(testCase.ResponseModel, o => o.Excluding(y => y.Output.Choices![0].Message.Content));
        message.ToString().Should().Be(testCase.ResponseModel.Output.Choices![0].Message.Content);
    }
}
