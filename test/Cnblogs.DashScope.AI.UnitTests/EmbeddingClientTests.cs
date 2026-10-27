using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using Microsoft.Extensions.AI;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.AI.UnitTests;

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

    [Fact]
    public async Task EmbeddingClient_TextWithoutModelIdInOptions_UsesModelIdFromConstructorAsync()
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
            new EmbeddingGenerationOptions { Dimensions = parameter?.Dimension });

        // Assert
        _ = dashScopeClient.Received().GetEmbeddingsAsync(
            Arg.Is<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(m
                => m.IsEquivalent(testCase.RequestModel)),
            Arg.Any<CancellationToken>());
        Assert.All(response, x => Assert.Equal(testCase.RequestModel.Model, x.ModelId));
    }

    [Fact]
    public async Task EmbeddingClient_TextWithModelIdInOptions_UsesModelIdFromOptionsAsync()
    {
        // Arrange
        var testCase = Snapshots.TextEmbedding.EmbeddingClientNoSse;
        var dashScopeClient = Substitute.For<IDashScopeClient>();
        var actualRequests = new List<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>();
        dashScopeClient.Configure()
            .GetEmbeddingsAsync(
                Arg.Do<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(actualRequests.Add),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(testCase.ResponseModel));
        var client = dashScopeClient.AsEmbeddingGenerator(testCase.RequestModel.Model, 1024);

        // Act
        var response = await client.GenerateAsync(
            ["你好"],
            new EmbeddingGenerationOptions { ModelId = "text-embedding-v4" });

        // Assert
        Assert.Equal("text-embedding-v4", Assert.Single(actualRequests).Model);
        Assert.All(response, x => Assert.Equal("text-embedding-v4", x.ModelId));
    }
}
