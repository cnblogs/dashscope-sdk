using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Schema;
using Cnblogs.DashScope.Core;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample.MsExtensionsAI;

public class MsExtensionAiJsonOutputExample : MsExtensionsAiSample
{
    /// <inheritdoc />
    public override string Description => "Request JSON output from LLM";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var chatClient = client.AsChatClient("qwen3.5-35b-a3b");
        var schema = JsonSerializerOptions.Web.GetJsonSchemaAsNode(typeof(ResultModel)).ToJsonString();
        var element = JsonDocument.Parse(schema).RootElement;
        var options = new ChatOptions()
        {
            ResponseFormat = new ChatResponseFormatJson(element),
        };
        var history = new List<ChatMessage>()
        {
            new(
                ChatRole.User,
                new List<AIContent>()
                {
                    new UriContent(
                        "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg",
                        "image/jpeg"),
                    new TextContent($"请为这张图片生成 alt 信息。Schema 为：{schema}。现在，请直接输出 JSON，不需要使用 Markdown 代码块包裹")
                })
        };
        var stream = chatClient.GetStreamingResponseAsync(history, options);
        var json = new StringBuilder();
        await foreach (var chatResponseUpdate in stream)
        {
            Console.Write(chatResponseUpdate);
            json.Append(chatResponseUpdate.Text);
        }

        Console.WriteLine();
        var model = JsonSerializer.Deserialize<ResultModel>(json.ToString(), JsonSerializerOptions.Web);
        Console.WriteLine($"Deserialized image alt: {model?.ImageAlt}");
    }
}

internal record ResultModel([Description("<img> 标签的 alt 内容")]string ImageAlt);

/*
{
  "alt": "一位笑容灿烂的年轻女子坐在沙滩上，与她的拉布拉多犬进行互动。狗狗戴着带有彩色图案的蓝色胸背带，正伸出前爪与女子的握手。背景是波光粼粼的海洋和明亮的落日余晖，画面充满了温馨、快乐的人与动物之间的亲密氛围。"
}
 */
