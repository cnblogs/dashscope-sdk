using Cnblogs.DashScope.Sdk.QWenMultimodal;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class QWenMultimodalCompletionTests
{
    private const string CustomModel = "custom-model";

    [Fact]
    public async Task Multimodal_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        MultimodalMessage[] messages =
        [
            new MultimodalMessage(
                "user",
                new List<MultimodalMessageContent> { new("https://cdn.example.com/image.jpg"), new("说明一下这张图片的内容") })
        ];
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = await client.GetQWenMultimodalCompletionAsync(QWenMultimodalModel.QWenVlMax, messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == "qwen-vl-max" && s.Input.Messages == messages && s.Parameters == parameters));
    }

    [Fact]
    public async Task Multimodal_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        MultimodalMessage[] messages =
        [
            new MultimodalMessage(
                "user",
                new List<MultimodalMessageContent> { new("https://cdn.example.com/image.jpg"), new("说明一下这张图片的内容") })
        ];
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = await client.GetQWenMultimodalCompletionAsync(CustomModel, messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == CustomModel && s.Input.Messages == messages && s.Parameters == parameters));
    }

    [Fact]
    public void MultimodalStream_UseEnum_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        MultimodalMessage[] messages =
        [
            new MultimodalMessage(
                "user",
                new List<MultimodalMessageContent> { new("https://cdn.example.com/image.jpg"), new("说明一下这张图片的内容") })
        ];
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = client.GetQWenMultimodalCompletionStreamAsync(QWenMultimodalModel.QWenVlPlus, messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == "qwen-vl-plus" && s.Input.Messages == messages && s.Parameters == parameters));
    }

    [Fact]
    public void Multimodal_CustomModel_Success()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        MultimodalMessage[] messages =
        [
            new MultimodalMessage(
                "user",
                new List<MultimodalMessageContent> { new("https://cdn.example.com/image.jpg"), new("说明一下这张图片的内容") })
        ];
        var parameters = new MultimodalParameters { Seed = 6666 };

        // Act
        _ = client.GetQWenMultimodalCompletionStreamAsync(CustomModel, messages, parameters);

        // Assert
        _ = client.Received().GetMultimodalGenerationStreamAsync(
            Arg.Is<ModelRequest<MultimodalInput, MultimodalParameters>>(
                s => s.Model == CustomModel && s.Input.Messages == messages && s.Parameters == parameters));
    }
}
