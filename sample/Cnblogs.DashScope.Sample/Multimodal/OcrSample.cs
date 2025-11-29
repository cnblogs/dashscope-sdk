using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrSample : ISample
{
    /// <inheritdoc />
    public string Description => "OCR Sample with rotate enabled";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var tilted = File.OpenRead("tilted.png");
        var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", tilted, "tilted.jpg");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages = new List<MultimodalMessage>();
        messages.Add(
            MultimodalMessage.User(
            [
                MultimodalMessageContent.ImageContent(ossLink, enableRotate: true),
            ]));
        var completion = client.GetMultimodalGenerationStreamAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-vl-ocr-latest",
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    IncrementalOutput = true,
                }
            });
        var reply = new StringBuilder();
        var first = false;
        MultimodalTokenUsage? usage = null;
        await foreach (var chunk in completion)
        {
            var choice = chunk.Output.Choices[0];
            if (first)
            {
                first = false;
                Console.Write("Assistant > ");
            }

            if (choice.Message.Content.Count == 0)
            {
                continue;
            }

            Console.Write(choice.Message.Content[0].Text);
            reply.Append(choice.Message.Content[0].Text);
            usage = chunk.Usage;
        }

        Console.WriteLine();
        messages.Add(MultimodalMessage.Assistant([MultimodalMessageContent.TextContent(reply.ToString())]));
        if (usage != null)
        {
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-28/435ea45f-9942-4fd4-983a-9ea8a3cd5ecb/tilted.jpg
产品介绍
本品采用韩国进口纤维丝制造，不缩水、不变形、不发霉、
不生菌、不伤物品表面。具有真正的不粘油、吸水力强、耐水
浸、清洗干净、无毒、无残留、易晾干等特点。
店家使用经验：不锈钢、陶瓷制品、浴盆、整体浴室大部分是
白色的光洁表面，用其他的抹布擦洗表面污渍不易洗掉，太尖
的容易划出划痕。使用这个仿真丝瓜布，沾少量中性洗涤剂揉
出泡沫，很容易把这些表面污渍擦洗干净。
6941990612023
货号：2023
Usage: in(2434)/out(155)/image(2410)/total(2589)
 */
