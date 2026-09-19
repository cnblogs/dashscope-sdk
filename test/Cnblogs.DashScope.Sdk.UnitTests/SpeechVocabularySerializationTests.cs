using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class SpeechVocabularySerializationTests
{
    [Fact]
    public async Task SpeechVocabulary_Create_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechVocabulary.Create;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CreateSpeechVocabularyAsync(
            "paraformer-v2",
            "test",
            new[] { new SpeechVocabularyItem("阿里巴巴", 4, "zh") });

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechVocabulary_List_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechVocabulary.List;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.ListSpeechVocabulariesAsync("test", 0, 10);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechVocabulary_Query_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechVocabulary.Query;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetSpeechVocabularyAsync("test1234567890abcdef");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechVocabulary_Update_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechVocabulary.Update;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.UpdateSpeechVocabularyAsync(
            "test1234567890abcdef",
            new[] { new SpeechVocabularyItem("通义千问", 5, "zh") });

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task SpeechVocabulary_Delete_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.SpeechVocabulary.Delete;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.DeleteSpeechVocabularyAsync("test1234567890abcdef");

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }
}
