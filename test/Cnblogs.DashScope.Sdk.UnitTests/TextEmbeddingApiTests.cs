using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.TextEmbedding;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class TextEmbeddingApiTests
{
    [Fact]
    public async Task GetEmbeddings_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var texts = new[] { "hello" };
        var parameters = new TextEmbeddingParameters { TextType = TextTypes.Query };

        // Act
        _ = client.GetTextEmbeddingsAsync(TextEmbeddingModel.TextEmbeddingV2, texts, parameters);

        // Assert
        await client.Received().GetEmbeddingsAsync(
            Arg.Is<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(
                s => s.Input.Texts == texts && s.Model == "text-embedding-v2" && s.Parameters == parameters));
    }

    [Fact]
    public async Task GetEmbeddings_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var texts = new[] { "hello" };
        var parameters = new TextEmbeddingParameters { TextType = TextTypes.Query };

        // Act
        _ = client.GetTextEmbeddingsAsync(Cases.CustomModelName, texts, parameters);

        // Assert
        await client.Received().GetEmbeddingsAsync(
            Arg.Is<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>>(
                s => s.Input.Texts == texts && s.Model == Cases.CustomModelName && s.Parameters == parameters));
    }
}
