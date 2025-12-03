using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal
{
    public class ImageInputSample : MultimodalSample
    {
        /// <inheritdoc />
        public override string Description => "Chat with image input";

        /// <inheritdoc />
        public override async Task RunAsync(IDashScopeClient client)
        {
            var messages = new List<MultimodalMessage>
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>
                    {
                        MultimodalMessageContent.ImageContent(
                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241022/emyrja/dog_and_girl.jpeg"),
                        MultimodalMessageContent.ImageContent(
                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/tiger.png"),
                        MultimodalMessageContent.TextContent("这些图展现了什么内容？")
                    })
            };
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
            messages.Add(MultimodalMessage.Assistant(
                new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent(reply.ToString()) }));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
            }
        }
    }
}
