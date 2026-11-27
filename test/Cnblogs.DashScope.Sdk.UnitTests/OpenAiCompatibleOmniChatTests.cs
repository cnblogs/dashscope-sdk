using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class OpenAiCompatibleOmniChatTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task OpenAiCompatibleGetChatCompletionAsync_OmniCapturedRequest_MatchSnapshotAsync(
        RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion> testCase)
    {
        // Arrange
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

    [Theory]
    [MemberData(nameof(SseData))]
    public async Task OpenAiCompatibleGetChatCompletionStreamAsync_OmniCapturedRequest_MatchSnapshotAsync(
        RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>> testCase)
    {
        // Arrange
        const bool sse = true;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => m.Headers.Accept.Any(h => h.MediaType == "text/event-stream")
                && Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        // the [DONE] marker terminated the stream
        Assert.Equal(testCase.ResponseModel.Count, chunks.Count);
        Assert.Equivalent(testCase.ResponseModel, chunks);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionAsync_OmniAudioOutput_ReturnTextOnlyAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniAudioOutputNoSse;
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var completion = await client.OpenAiCompatibleGetChatCompletionAsync(testCase.RequestModel);

        // Assert
        // the audio is only delivered by the streaming method, the speech was generated and billed anyway
        var message = completion.Choices[0].Message!;
        Assert.Null(message.Audio);
        Assert.Equal(string.Empty, message.ReasoningContent);
        Assert.False(string.IsNullOrEmpty(message.Content));
        Assert.True(completion.Usage!.CompletionTokensDetails!.AudioTokens > 0);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionStreamAsync_OmniAudioOutput_SpeakEveryPartOfTheTextAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniAudioOutputSse;
        const bool sse = true;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        var deltas = chunks.SelectMany(c => c.Choices).Select(c => c.Delta).ToList();
        var audio = deltas.Where(d => d?.Audio != null).Select(d => d!.Audio!).ToList();
        // the fixture keeps the first and the last audio chunk of every run, two runs here
        Assert.Equal(3, audio.Count);
        Assert.All(audio, a => Assert.Equal("<base64>", a.Data));
        Assert.Single(audio.Select(a => a.Id).Distinct());
        // the audio never comes before the text started, and the text goes on after a run of audio
        var firstAudio = deltas.FindIndex(d => d?.Audio != null);
        Assert.Contains(deltas.Take(firstAudio), d => string.IsNullOrEmpty(d?.Content) == false);
        Assert.Contains(deltas.Skip(firstAudio), d => string.IsNullOrEmpty(d?.Content) == false);
        // the finish reason rides on a text chunk at the very end
        var last = chunks.SelectMany(c => c.Choices).Single(c => c.FinishReason != null);
        Assert.Equal("stop", last.FinishReason);
        Assert.Equal(string.Empty, last.Delta!.Content);
        Assert.Null(last.Delta.Audio);
        var usage = chunks.Single(c => c.Usage != null).Usage!;
        Assert.Equal(139, usage.CompletionTokensDetails!.AudioTokens);
        Assert.Equal(33, usage.CompletionTokensDetails.TextTokens);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionStreamAsync_OmniThinking_StreamsReasoningBeforeTextAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniAudioInputSse;
        const bool sse = true;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        var deltas = chunks.SelectMany(c => c.Choices).Select(c => c.Delta).ToList();
        var firstText = deltas.FindIndex(d => string.IsNullOrEmpty(d?.Content) == false);
        var reasoning = string.Concat(deltas.Take(firstText).Select(d => d!.ReasoningContent));
        var text = string.Concat(deltas.Skip(firstText).Select(d => d!.Content));
        Assert.Contains("海滩", reasoning);
        // the reasoning is over when the text starts
        Assert.All(deltas.Skip(firstText), d => Assert.True(string.IsNullOrEmpty(d!.ReasoningContent)));
        Assert.Contains("海滩", text);
        var usage = chunks.Single(c => c.Usage != null).Usage!;
        Assert.Equal(150, usage.CompletionTokensDetails!.ReasoningTokens);
        // the audio of the request is billed by its duration: 114 tokens for about 16 seconds
        Assert.Equal(114, usage.PromptTokensDetails!.AudioTokens);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionAsync_OmniMixedInput_ReturnReasoningAndTokenDetailsAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniMixedInputNoSse;
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var completion = await client.OpenAiCompatibleGetChatCompletionAsync(testCase.RequestModel);

        // Assert
        var message = completion.Choices[0].Message!;
        Assert.False(string.IsNullOrEmpty(message.ReasoningContent));
        Assert.Contains("图片", message.Content);
        Assert.Contains("视频", message.Content);
        Assert.Contains("音频", message.Content);
        // every modality of the request is billed on its own
        var details = completion.Usage!.PromptTokensDetails!;
        Assert.Equal((114, 2503, 1190), (details.AudioTokens, details.ImageTokens, details.VideoTokens));
        Assert.True(completion.Usage.CompletionTokensDetails!.ReasoningTokens > 0);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionAsync_OmniJsonObject_ReturnValidJsonAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniJsonObjectNoSse;
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var completion = await client.OpenAiCompatibleGetChatCompletionAsync(testCase.RequestModel);

        // Assert
        var json = JsonNode.Parse(completion.Choices[0].Message!.Content!);
        Assert.Equal("zh", json!["language"]!.GetValue<string>());
        Assert.Contains("海滩", json["asr"]!.GetValue<string>());
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionAsync_OmniToolResult_AnswerFromTheToolOutputAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniToolResultNoSse;
        const bool sse = false;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var completion = await client.OpenAiCompatibleGetChatCompletionAsync(testCase.RequestModel);

        // Assert
        // the request carries the call of the assistant and the tool output answering it
        var messages = testCase.RequestModel.Messages;
        var call = Assert.Single(messages[1].ToolCalls!);
        Assert.Equal(call.Id, messages[2].ToolCallId);
        Assert.Equal("tool", messages[2].Role);
        var content = completion.Choices[0].Message!.Content!;
        Assert.Contains("晴", content);
        Assert.Contains("12", content);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionStreamAsync_OmniToolCall_AddUpTheCallAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniToolCallSse;
        const bool sse = true;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        // the call arrives in pieces: the first one carries the id and the name, the others the arguments
        var pieces = chunks.SelectMany(c => c.Choices)
            .SelectMany(c => c.Delta?.ToolCalls ?? Enumerable.Empty<ToolCall>())
            .ToList();
        var name = string.Concat(pieces.Select(p => p.Function.Name));
        var arguments = string.Concat(pieces.Select(p => p.Function.Arguments));
        Assert.Equal("call_33ce5ce049474336a746100c", pieces[0].Id);
        Assert.All(pieces.Skip(1), p => Assert.Equal(string.Empty, p.Id));
        Assert.Equal("get_weather", name);
        Assert.Equal("{\"city\": \"北京\"}", arguments);
        Assert.Equal("tool_calls", chunks.SelectMany(c => c.Choices).Single(c => c.FinishReason != null).FinishReason);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionStreamAsync_OmniSearch_HandOutTheOpeningFrameWithoutDeltaAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniSearchSse;
        const bool sse = true;
        var (client, _) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var chunks = await client.OpenAiCompatibleGetChatCompletionStreamAsync(testCase.RequestModel).ToListAsync();

        // Assert
        // the agent search opens the stream with a frame that announces the assistant only:
        // the string "null" is the finish reason the server sends while it is still running
        var opening = Assert.Single(chunks[0].Choices);
        Assert.Null(opening.Delta);
        Assert.Equal("null", opening.FinishReason);
        var answer = string.Concat(chunks.Skip(1).SelectMany(c => c.Choices).Select(c => c.Delta?.Content));
        Assert.Contains("2026", answer);
        var last = chunks.SelectMany(c => c.Choices).Single(c => c.FinishReason is not (null or "null"));
        Assert.Equal("stop", last.FinishReason);
    }

    [Fact]
    public async Task OpenAiCompatibleGetChatCompletionAsync_StreamOptions_LeaveTheOptionsOutAsync()
    {
        // Arrange
        var testCase = Snapshots.OpenAiCompatibleChat.OmniAudioOutputNoSse;
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);
        var input = new OpenAiCompatibleChatRequest
        {
            Model = testCase.RequestModel.Model,
            Messages = testCase.RequestModel.Messages,
            // the service rejects a non-streaming request that carries the streaming options
            StreamOptions = testCase.RequestModel.StreamOptions ?? new OpenAiCompatibleChatStreamOptions(IncludeUsage: true)
        };

        // Act
        await client.OpenAiCompatibleGetChatCompletionAsync(input);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => LeavesTheStreamOptionsOut(m)),
            Arg.Any<CancellationToken>());
    }

    public static TheoryData<RequestSnapshot<OpenAiCompatibleChatRequest, OpenAiCompatibleChatCompletion>> NoSseData
        => new()
        {
            Snapshots.OpenAiCompatibleChat.OmniAudioOutputNoSse,
            Snapshots.OpenAiCompatibleChat.OmniMixedInputNoSse,
            Snapshots.OpenAiCompatibleChat.OmniToolResultNoSse,
            Snapshots.OpenAiCompatibleChat.OmniMultichannelNoSse,
            Snapshots.OpenAiCompatibleChat.OmniJsonObjectNoSse
        };

    public static TheoryData<RequestSnapshot<OpenAiCompatibleChatRequest, List<OpenAiCompatibleChatChunk>>> SseData
        => new()
        {
            Snapshots.OpenAiCompatibleChat.OmniAudioOutputSse,
            Snapshots.OpenAiCompatibleChat.OmniQwen3AudioOutputSse,
            Snapshots.OpenAiCompatibleChat.OmniAudioInputSse,
            Snapshots.OpenAiCompatibleChat.OmniVideoInputSse,
            Snapshots.OpenAiCompatibleChat.OmniToolCallSse,
            Snapshots.OpenAiCompatibleChat.OmniSearchSse
        };

    private static bool LeavesTheStreamOptionsOut(HttpRequestMessage message)
    {
#pragma warning disable VSTHRD002
        var payload = JsonNode.Parse(message.Content!.ReadAsStringAsync().Result)!;
#pragma warning restore VSTHRD002
        return payload["stream"]!.GetValue<bool>() == false
               && payload["stream_options"] is null;
    }
}
