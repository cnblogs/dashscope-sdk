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
        // the [DONE] marker terminated the stream: the fixture keeps every text delta of the
        // translation and, of the audio deltas, only the first and the last one
        Assert.Equal(testCase.ResponseModel.Count, chunks.Count);
        Assert.Equivalent(testCase.ResponseModel, chunks);
        var message = string.Join(string.Empty, chunks.SelectMany(c => c.Choices.Select(x => x.Delta?.Content)));
        Assert.Equal(
            "This picture is of a woman and a dog playing on the beach. The background is waves lapping against "
            + "the sand, and the sky is a bit pale, probably at sunrise or sunset. If there's anything else you want "
            + "to know, just ask.",
            message);
        var usage = chunks.Single(c => c.Usage is not null).Usage;
        Assert.Equal(654, usage!.TotalTokens);
        Assert.Equal(188, usage.CompletionTokensDetails!.AudioTokens);
        var audio = chunks.SelectMany(c => c.Choices.Select(x => x.Delta?.Audio)).Where(a => a is not null).ToList();
        Assert.Equal(2, audio.Count); // only the first and the last audio delta are kept in the fixture
        Assert.Equal("<base64>", audio[0]!.Data); // the payload of the first one is a placeholder
        Assert.Equal(string.Empty, audio[1]!.Data); // the last one only carries the finish reason
    }

    private static bool HasEventStreamAcceptHeader(HttpRequestMessage message)
        => message.Headers.Accept.Any(h => h.MediaType == "text/event-stream");
}
