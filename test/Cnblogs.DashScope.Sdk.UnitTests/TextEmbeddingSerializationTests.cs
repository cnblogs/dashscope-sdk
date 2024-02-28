using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using FluentAssertions;
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
        response.Output.Embeddings[0].Embedding.Should().NotBeEmpty(); // embedding array is too large
        response.Should().BeEquivalentTo(
            testCase.ResponseModel,
            o => o.Excluding(x => x.Output.Embeddings[0].Embedding));
    }
}
