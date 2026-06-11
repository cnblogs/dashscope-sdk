using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.ImageSynthesis;

public class EditImageSample : ImageSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Edit image sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var lenna = File.OpenRead("Lenna.jpg");
        var ossLink = await client.UploadTemporaryFileAsync("qwen-image-2.0-pro", lenna, "lenna.jpg");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages = new List<MultimodalMessage>
        {
            MultimodalMessage.User(
            [
                MultimodalMessageContent.ImageContent(ossLink),
                MultimodalMessageContent.TextContent("请帮我增强这张照片的清晰度")
            ])
        };

        Console.WriteLine("Generating...");
        var response = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-image-2.0-pro",
                Input = new MultimodalInput()
                {
                    Messages = messages
                },
                Parameters = new MultimodalParameters()
                {
                    NegativePrompt = "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。",
                    N = 1,
                    PromptExtend = true,
                    Watermark = false,
                    Size = "2048*2048"
                }
            });
        var url = response.Output.Choices[0].Message.Content[0].Image;
        Console.WriteLine($"Generated image url: {url}");
    }
}
