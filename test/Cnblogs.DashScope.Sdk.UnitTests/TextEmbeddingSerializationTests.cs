using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;

using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextEmbeddingSerializationTests
{
    [Fact]
    public async Task TextEmbedding_MultipleTexts_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextEmbedding.NoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetEmbeddingsAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.NotEmpty(response.Output.Embeddings[0].Embedding); // embedding array is too large
        response = response with { Output = new TextEmbeddingOutput(response.Output.Embeddings) };
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task BatchTextEmbedding_ReturnTask_SuccessAsync()
    {
        // Arrange
        const bool sse = false;
        var testCase = Snapshots.TextEmbedding.BatchNoSse;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.BatchGetEmbeddingsAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }
}
