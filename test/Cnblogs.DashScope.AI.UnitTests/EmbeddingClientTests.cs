using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using Microsoft.Extensions.AI;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.AI.UnitTests
{
    public class EmbeddingClientTests
    {
        [Fact]
        public async Task EmbeddingClient_Text_SuccessAsync()
        {
            // Arrange
            var testCase = Snapshots.TextEmbedding.EmbeddingClientNoSse;
            var dashScopeClient = Substitute.For<IDashScopeClient>();
            dashScopeClient.Configure()
                .GetEmbeddingsAsync(
                    Arg.Any<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(testCase.ResponseModel));
            var client = dashScopeClient.AsEmbeddingGenerator(testCase.RequestModel.Model, 1024);
            var content = testCase.RequestModel.Input.Texts.ToList();
            var parameter = testCase.RequestModel.Parameters;

            // Act
            var response = await client.GenerateAsync(
                content,
                new EmbeddingGenerationOptions
                {
                    ModelId = testCase.RequestModel.Model, Dimensions = parameter?.Dimension
                });

            // Assert
            _ = dashScopeClient.Received().GetEmbeddingsAsync(
                Arg.Is<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(m
                    => m.IsEquivalent(testCase.RequestModel)),
                Arg.Any<CancellationToken>());
            Assert.Equivalent(
                testCase.ResponseModel.Output.Embeddings.Select(x => x.Embedding),
                response.Select(x => x.Vector.ToArray()));
        }
    }
}
