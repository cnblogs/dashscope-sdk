using System.Net.Mime;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample.MSEAI;

public class RawOutputExample : MseAiExample
{
    /// <inheritdoc />
    public override string Description => "Chat with extra data from raw output";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var response = client
            .AsChatClient("qwen3-vl-plus")
            .GetStreamingResponseAsync(
                new List<ChatMessage>()
                {
                    new(
                        ChatRole.User,
                        new List<AIContent>()
                        {
                            new UriContent(
                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241022/emyrja/dog_and_girl.jpeg",
                                MediaTypeNames.Image.Jpeg),
                            new UriContent(
                                "https://dashscope.oss-cn-beijing.aliyuncs.com/images/tiger.png",
                                MediaTypeNames.Image.Png),
                            new TextContent("这些图展现了什么内容？")
                        })
                },
                new ChatOptions());
        var lastChunk = (ChatResponseUpdate?)null;
        await foreach (var chunk in response)
        {
            Console.Write(chunk.Text);
            lastChunk = chunk;
        }

        Console.WriteLine();
        var raw = lastChunk?.RawRepresentation as ModelResponse<MultimodalOutput, MultimodalTokenUsage>;
        Console.WriteLine($"Image token usage: {raw?.Usage?.ImageTokens}");
    }
}

/*
这两张图片展现了截然不同的主题和氛围：

- **第一张图片**：描绘了一幅温馨、宁静的场景。一位年轻女子坐在沙滩上，与一只金毛犬互动。狗狗抬起前爪，似乎在与女子“击掌”，女子面带微笑，享受着与宠物共度的美好时光。背景是波光粼粼的大海和柔和的日落光线，整个画面充满了温暖、快乐和人与动物之间亲密无间的情感。

- **第二张图片**：展现了一只威严的老虎在森林中行走的画面。老虎正视镜头，眼神锐利，姿态充满力量感，仿佛正在巡视自己的领地。周围是茂密的树林、覆盖苔藓的树根和散落的落叶，营造出一种原始、神秘且略带危险的自然野性氛围。

总的来说，这两张图分别代表了：
- **人与宠物的温情陪伴**（第一张）
- **野生动物的原始力量与自然之美**（第二张）

它们形成了鲜明对比：一张是温柔的人类情感，另一张是野性的自然力量。
Image token usage: 3529
 */
