using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechRecognitionSerializationTests
{
    [Fact]
    public async Task SpeechRecognition_FlashNoSse_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechRecognition.FlashNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetSpeechRecognitionAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechRecognition_FlashSse_SuccessAsync()
    {
        // Arrange
        const bool sse = true;
        var testCase = Snapshots.SpeechRecognition.FlashSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var outputs = await client.GetSpeechRecognitionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m
                => m.Headers.Accept.ToString() == "text/event-stream"
                   && m.Headers.GetValues("X-DashScope-SSE").First() == "enable"
                   && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equal(2, outputs.Count);
        Assert.False(outputs[0].Output.Sentence?.SentenceEnd);
        Assert.Equivalent(testCase.ResponseModel, outputs[^1]);
    }
}
