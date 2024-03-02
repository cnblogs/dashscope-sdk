using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using Cnblogs.DashScope.Sdk.Wanx;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class WanxApiTests
{
    private static readonly ImageSynthesisParameters Parameters = new()
    {
        N = 4,
        Seed = 42,
        Size = "1024*1024",
        Style = ImageStyles.Anime
    };

    [Fact]
    public async Task WanxImageSynthesis_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        client.Configure().CreateImageSynthesisTaskAsync(
                Arg.Any<ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Snapshots.ImageSynthesis.CreateTask.ResponseModel);

        // Act
        _ = await client.CreateWanxImageSynthesisTaskAsync(
            WanxModel.WanxV1,
            Cases.Prompt,
            Cases.PromptAlter,
            Parameters);

        // Assert
        _ = await client.Received().CreateImageSynthesisTaskAsync(
            Arg.Is<ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>>(
                s => s.Model == "wanx-v1"
                     && s.Input.Prompt == Cases.Prompt
                     && s.Input.NegativePrompt == Cases.PromptAlter
                     && s.Parameters == Parameters));
    }

    [Fact]
    public async Task WanxImageSynthesis_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        client.Configure().CreateImageSynthesisTaskAsync(
                Arg.Any<ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>>(),
                Arg.Any<CancellationToken>())
            .Returns(Snapshots.ImageSynthesis.CreateTask.ResponseModel);

        // Act
        _ = await client.CreateWanxImageSynthesisTaskAsync(
            Cases.CustomModelName,
            Cases.Prompt,
            Cases.PromptAlter,
            Parameters);

        // Assert
        _ = await client.Received().CreateImageSynthesisTaskAsync(
            Arg.Is<ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>>(
                s => s.Model == Cases.CustomModelName
                     && s.Input.Prompt == Cases.Prompt
                     && s.Input.NegativePrompt == Cases.PromptAlter
                     && s.Parameters == Parameters));
    }

    [Fact]
    public async Task WanxImageSynthesis_GetTask_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetWanxImageSynthesisTaskAsync(Cases.Uuid);

        // Assert
        _ = await client.Received().GetTaskAsync<ImageSynthesisOutput, ImageSynthesisUsage>(Cases.Uuid);
    }

    [Fact]
    public async Task WanxImageGeneration_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        client.Configure().CreateImageGenerationTaskAsync(
                Arg.Any<ModelRequest<ImageGenerationInput>>(),
                Arg.Any<CancellationToken>())
            .Returns(Snapshots.ImageGeneration.CreateTaskNoSse.ResponseModel);

        // Act
        _ = await client.CreateWanxImageGenerationTaskAsync(
            WanxStyleRepaintModel.WanxStyleRepaintingV1,
            new ImageGenerationInput() { ImageUrl = Cases.ImageUrl, StyleIndex = 3 });

        // Assert
        _ = await client.Received().CreateImageGenerationTaskAsync(
            Arg.Is<ModelRequest<ImageGenerationInput>>(
                s => s.Model == "wanx-style-repaint-v1"
                     && s.Input.ImageUrl == Cases.ImageUrl
                     && s.Input.StyleIndex == 3));
    }

    [Fact]
    public async Task WanxImageGeneration_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();
        client.Configure().CreateImageGenerationTaskAsync(
                Arg.Any<ModelRequest<ImageGenerationInput>>(),
                Arg.Any<CancellationToken>())
            .Returns(Snapshots.ImageGeneration.CreateTaskNoSse.ResponseModel);

        // Act
        _ = await client.CreateWanxImageGenerationTaskAsync(
            Cases.CustomModelName,
            new ImageGenerationInput() { ImageUrl = Cases.ImageUrl, StyleIndex = 3 });

        // Assert
        _ = await client.Received().CreateImageGenerationTaskAsync(
            Arg.Is<ModelRequest<ImageGenerationInput>>(
                s => s.Model == Cases.CustomModelName
                     && s.Input.ImageUrl == Cases.ImageUrl
                     && s.Input.StyleIndex == 3));
    }

    [Fact]
    public async Task WanxImageGeneration_GetTask_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetWanxImageGenerationTaskAsync(Cases.Uuid);

        // Assert
        _ = await client.Received().GetTaskAsync<ImageGenerationOutput, ImageGenerationUsage>(Cases.Uuid);
    }
}
