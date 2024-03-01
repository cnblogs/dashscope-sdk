using Cnblogs.DashScope.Sdk.QWenMultimodal;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class QWenMultimodalApiTests
{
    private static readonly List<MultimodalMessage> Messages =
    [
        new MultimodalMessage(
            "user",
            new List<MultimodalMessageContent> { new("https://cdn.example.com/image.jpg"), new("说明一下这张图片的内容") })
    ];

    [Fact]
    public async Task Multimodal_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = await client.GetQWenMultimodalCompletionAsync(QWenMultimodalModel.QWenVlMax, Messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == "qwen-vl-max" && s.Input.Messages == Messages && s.Parameters == parameters));
    }

    [Fact]
    public async Task Multimodal_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = await client.GetQWenMultimodalCompletionAsync(Cases.CustomModelName, Messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == Cases.CustomModelName && s.Input.Messages == Messages && s.Parameters == parameters));
    }

    [Fact]
    public void MultimodalStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = client.GetQWenMultimodalCompletionStreamAsync(QWenMultimodalModel.QWenVlPlus, Messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == "qwen-vl-plus" && s.Input.Messages == Messages && s.Parameters == parameters));
    }

    [Fact]
    public void Multimodal_CustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = client.GetQWenMultimodalCompletionStreamAsync(Cases.CustomModelName, Messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == Cases.CustomModelName && s.Input.Messages == Messages && s.Parameters == parameters));
    }
}
