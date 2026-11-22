using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechSynthesisSerializationTests
{
    [Fact]
    public async Task SpeechSynthesis_NoSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.SpeechSynthesis.NoSse;
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetSpeechSynthesisAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechSynthesis_Sse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.SpeechSynthesis.Sse;
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetSpeechSynthesisStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.All(outputs.SkipLast(1), x => Assert.Equal("null", x.Output.FinishReason));
        var begin = Assert.Single(outputs.Where(x => x.Output.Type == "sentence-begin"));
        Assert.Equal("我家的后面有一个很大的花园。", begin.Output.OriginalText);
        Assert.Equal("我家的后面有一个很大的花园。", begin.Output.NormalizedText);
        Assert.Equal(0, begin.Output.Sentence!.Index);
        Assert.Contains(outputs, x => x.Output.Type == "sentence-synthesis" && x.Output.Audio!.Data!.Length > 0);
        var sentenceEnd = Assert.Single(outputs.Where(x => x.Output.Type == "sentence-end"));
        var words = sentenceEnd.Output.Sentence!.Words!;
        Assert.Equal(13, words.Length);
        Assert.Equal(new SpeechSynthesizerOutputWord("我", 0, 1, 80, 240), words[0]);
        Assert.Equal(new SpeechSynthesizerOutputWord("园。", 12, 13, 2320, 2560), words[^1]);
        Assert.Equivalent(testCase.ResponseModel, outputs.Last());
    }
}
