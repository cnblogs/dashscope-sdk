using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.ImageSynthesis;

public class MultiImageEditSample : ImageSynthesisSample
{
    /// <inheritdoc />
    public override string Description => "Edit image with multiple input images";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // qwen-image-2.0/3.0 series accept 1-3 input images, qwen-image-2.1 series accept up to 10.
        // The output aspect ratio follows the last input image.
        var messages = new List<MultimodalMessage>
        {
            MultimodalMessage.User(
            [
                MultimodalMessageContent.ImageContent(
                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/rdsgaa/image+%2815%29.png"),
                MultimodalMessageContent.ImageContent(
                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/qokhtl/image+%2816%29.png"),
                MultimodalMessageContent.TextContent(
                    "使用图一的城市照片作为底图。请勿更改照片中的真实建筑、街道、车辆或人物。保持照片的真实性。三个图二中的卡通形象在建筑物周围，一个趴在建筑物上方，一个从建筑物的右边探出头来，一个坐在建筑物前的空地上。该形象应采用扁平化的图形风格绘制，轮廓清晰，类似于壁画或海报插图。")
            ])
        };

        Console.WriteLine("Generating...");
        var response = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-image-2.0-pro",
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    NegativePrompt = "低分辨率，低画质，构图混乱。",
                    N = 1,
                    PromptExtend = true,
                    PromptExtendMode = "direct",
                    Seed = 233,
                    Size = "1328*1328",
                    Watermark = false
                }
            });
        var url = response.Output.Choices[0].Message.Content[0].Image;
        Console.WriteLine($"Edited image url: {url}");
        Console.WriteLine(
            $"Usage: count({response.Usage?.ImageCount}) width({response.Usage?.Width}) height({response.Usage?.Height})");
    }
}
