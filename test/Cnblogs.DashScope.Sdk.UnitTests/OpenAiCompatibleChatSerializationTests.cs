using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleChatSerializationTests
{
    [Fact]
    public async Task OpenAiCompatibleChat_NoSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.ChatNoSse;
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var completion = await client.OpenAiCompatibleGetChatCompletionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, completion);
    }

    [Fact]
    public async Task OpenAiCompatibleChat_LivetranslateSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.LivetranslateSse;
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))
                && HasEventStreamAcceptHeader(m)),
            Arg.Any<CancellationToken>());
        // the [DONE] marker terminated the stream, the trimmed fixtures cover the role chunk,
        // the content deltas, the audio deltas and the usage chunk
        Assert.Equal(testCase.ResponseModel.Count, chunks.Count);
        Assert.Equivalent(testCase.ResponseModel, chunks);
        var message = string.Join(string.Empty, chunks.SelectMany(c => c.Choices.Select(x => x.Delta?.Content)));
        Assert.StartsWith("This picture", message);
        var usage = chunks.Single(c => c.Usage is not null).Usage;
        Assert.Equal(661, usage!.TotalTokens);
        Assert.Equal(195, usage.CompletionTokensDetails!.AudioTokens);
        var audio = chunks.SelectMany(c => c.Choices.Select(x => x.Delta?.Audio)).Where(a => a is not null).ToList();
        Assert.Equal(2, audio.Count); // the base64 payloads are placeholders in the fixtures
    }

    private static bool HasEventStreamAcceptHeader(HttpRequestMessage message)
        => message.Headers.Accept.Any(h => h.MediaType == "text/event-stream");
}
