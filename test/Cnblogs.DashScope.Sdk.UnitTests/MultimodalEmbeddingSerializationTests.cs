using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class MultimodalEmbeddingSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task GetMultimodalEmbeddings_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
            ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>> testCase,
        int vectorDimension)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.GetMultimodalEmbeddingsAsync(testCase.RequestModel);

        // Assert
        // the request equivalence also covers the wire shapes: a text goes as an object, the
        // members set on a content, multi_images, and the snake_case parameter names
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        // the vectors are too large for the typed snapshot, which keeps the index and the type of
        // every embedding: the vectors are compared by their dimension
        var expected = testCase.ResponseModel.Output.Embeddings;
        var actual = response.Output.Embeddings;
        Assert.Equal(expected.Count, actual.Count);
        for (var i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected[i].Index, actual[i].Index);
            Assert.Equal(expected[i].Type, actual[i].Type);
            Assert.Equal(vectorDimension, actual[i].Embedding.Length);
        }

        Assert.Equivalent(testCase.ResponseModel with { Output = response.Output }, response);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<MultimodalEmbeddingInput, MultimodalEmbeddingParameters>,
        ModelResponse<MultimodalEmbeddingOutput, MultimodalEmbeddingUsage>>, int> NoSseData
        => new()
        {
            // the model default unless the dimension is set in the parameters
            { Snapshots.MultimodalEmbedding.NoSse, 1152 },
            { Snapshots.MultimodalEmbedding.FusedNoSse, 64 },
            { Snapshots.MultimodalEmbedding.FusionNoSse, 2560 },
            { Snapshots.MultimodalEmbedding.VlNoSse, 256 },
            { Snapshots.MultimodalEmbedding.Qwen25VlNoSse, 512 },
            { Snapshots.MultimodalEmbedding.V1NoSse, 1024 }
        };
}
