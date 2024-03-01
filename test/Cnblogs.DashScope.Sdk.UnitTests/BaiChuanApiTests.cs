using Cnblogs.DashScope.Sdk.BaiChuan;
using Cnblogs.DashScope.Sdk.UnitTests.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class BaiChuanApiTests
{
    [Fact]
    public async Task BaiChuanTextGeneration_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetBaiChuanTextCompletionAsync(BaiChuanLlm.BaiChuan7B, Cases.Prompt);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Model == "baichuan-7b-v1" && s.Input.Prompt == Cases.Prompt && s.Parameters == null));
    }

    [Fact]
    public async Task BaiChuanTextGeneration_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetBaiChuanTextCompletionAsync(BaiChuanLlm.BaiChuan7B, Cases.Prompt);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Model == "baichuan-7b-v1" && s.Input.Prompt == Cases.Prompt && s.Parameters == null));
    }

    [Fact]
    public async Task BaiChuan2TextGeneration_UseEnum_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetBaiChuanTextCompletionAsync(
            BaiChuan2Llm.BaiChuan2_13BChatV1,
            Cases.TextMessages,
            ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Model == "baichuan2-13b-chat-v1"
                     && s.Input.Messages == Cases.TextMessages
                     && s.Parameters != null
                     && s.Parameters.ResultFormat == ResultFormats.Message));
    }

    [Fact]
    public async Task BaiChuan2TextGeneration_CustomModel_SuccessAsync()
    {
        // Arrange
        var client = Substitute.For<IDashScopeClient>();

        // Act
        _ = await client.GetBaiChuanTextCompletionAsync(
            Cases.CustomModelName,
            Cases.TextMessages,
            ResultFormats.Message);

        // Assert
        _ = await client.Received().GetTextCompletionAsync(
            Arg.Is<ModelRequest<TextGenerationInput, TextGenerationParameters>>(
                s => s.Model == Cases.CustomModelName
                     && s.Input.Messages == Cases.TextMessages
                     && s.Parameters != null
                     && s.Parameters.ResultFormat == ResultFormats.Message));
    }
}
