using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class VoiceCustomizationSerializationTests
{
    [Theory]
    [MemberData(nameof(NoSseData))]
    public async Task VoiceCustomization_NoSse_SuccessAsync(
        RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
            ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>> testCase)
    {
        // Arrange
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CustomizeVoiceAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        Assert.Equivalent(testCase.ResponseModel, response);
    }

    [Fact]
    public async Task VoiceCustomization_DesignCreateNoSse_SuccessAsync()
    {
        // Arrange
        var testCase = Snapshots.VoiceCustomization.DesignCreateNoSse;
        const bool sse = false;
        var (client, handler) = await Sut.GetTestClientAsync(sse, testCase);

        // Act
        var response = await client.CustomizeVoiceAsync(testCase.RequestModel);

        // Assert
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m => Checkers.IsJsonEquivalent(m.Content!, testCase.GetRequestJson(sse))),
            Arg.Any<CancellationToken>());
        // PreviewAudio.Data is a placeholder in the snapshot, the base64 wav stays in the
        // raw fixture; assert the decoded RIFF header instead of the literal value.
        var expected = testCase.ResponseModel with
        {
            Output = testCase.ResponseModel.Output with
            {
                PreviewAudio = response.Output.PreviewAudio
            }
        };
        Assert.Equivalent(expected, response);
        var preview = Convert.FromBase64String(response.Output.PreviewAudio!.Data!);
        Assert.Equal(Encoding.UTF8.GetBytes("RIFF"), preview[..4]);
        Assert.Equal(Encoding.UTF8.GetBytes("WAVE"), preview[8..12]);
    }

    public static TheoryData<RequestSnapshot<ModelRequest<VoiceCustomizationInput, IVoiceCustomizationParameters>,
        ModelResponse<VoiceCustomizationOutput, VoiceCustomizationUsage>>> NoSseData
        => new()
        {
            Snapshots.VoiceCustomization.CloneCreateNoSse,
            Snapshots.VoiceCustomization.ListNoSse,
            Snapshots.VoiceCustomization.QueryNoSse,
            Snapshots.VoiceCustomization.QueryDesignNoSse,
            Snapshots.VoiceCustomization.UpdateNoSse,
            Snapshots.VoiceCustomization.DeleteNoSse
        };
}
