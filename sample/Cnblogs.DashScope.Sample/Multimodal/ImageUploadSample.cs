using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class ImageUploadSample : ISample
{
    /// <inheritdoc />
    public string Description => "Upload image from file system";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var lenna = File.OpenRead("Lenna.jpg");
        var ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", lenna, "lenna.jpg");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages = new List<MultimodalMessage>();
        messages.Add(
            MultimodalMessage.User(
            [
                MultimodalMessageContent.ImageContent(ossLink),
                MultimodalMessageContent.TextContent("她是谁？")
            ]));
        var completion = client.GetMultimodalGenerationStreamAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen3-vl-plus",
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    IncrementalOutput = true,
                    EnableThinking = true,
                    VlHighResolutionImages = true
                }
            });
        var reply = new StringBuilder();
        var reasoning = false;
        MultimodalTokenUsage? usage = null;
        await foreach (var chunk in completion)
        {
            var choice = chunk.Output.Choices[0];
            if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
            {
                // reasoning
                if (reasoning == false)
                {
                    Console.Write("Reasoning > ");
                    reasoning = true;
                }

                Console.Write(choice.Message.ReasoningContent);
                continue;
            }

            if (reasoning)
            {
                reasoning = false;
                Console.WriteLine();
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
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
Reasoning > 用户现在需要识别图中的人物。这张照片里的女性是Nancy Sinatra（南希·辛纳特拉），她是美国60年代著名的歌手、演员，也是Frank Sinatra的女儿。她的标志性风格包括复古装扮和独特的 音乐风格，这张照片的造型（宽檐帽、羽毛装饰）符合她那个时期的时尚风格。需要确认信息准确性，Nancy Sinatra在60年代的影像资料中常见这样的复古造型，所以判断是她。

Assistant > 图中人物是**南希·辛纳特拉（Nancy Sinatra）**，她是美国20世纪60年代著名的歌手、演员，也是传奇歌手弗兰克·辛纳特拉（Frank Sinatra）的女儿。她以独特的复古风格、音乐作品（如经典歌曲 *These Boots Are Made for Walkin’* ）和影视表现闻名，这张照片的造型（宽檐帽搭配羽毛装饰等）也契合她标志性的时尚风格。
Usage: in(271)/out(199)/image(258)/reasoning(98)/total(470)
 */
