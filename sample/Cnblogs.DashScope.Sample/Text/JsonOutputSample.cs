using System.Text;
using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class JsonOutputSample : TextSample
{
    /// <inheritdoc />
    public override string Description => "Structured output with JSON schema";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var messages = new List<MultimodalMessage>
        {
            // json_schema does not require the word "json" in the prompt, the output
            // structure is fully controlled by the schema below.
            MultimodalMessage.System([MultimodalMessageContent.TextContent("统计用户输入的字数信息")])
        };
        while (true)
        {
            Console.Write("User > ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a user input.");
                return;
            }

            messages.Add(MultimodalMessage.User([MultimodalMessageContent.TextContent(input)]));
            var completion = client.GetMultimodalGenerationStreamAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Model = "qwen3.8-flash",
                    Input = new MultimodalInput() { Messages = messages },
                    Parameters = new MultimodalParameters()
                    {
                        IncrementalOutput = true,
                        ResponseFormat = DashScopeResponseFormat.JsonSchema(
                            new DashScopeJsonSchema(
                                "word_count",
                                new JsonObject
                                {
                                    ["type"] = "object",
                                    ["properties"] = new JsonObject
                                    {
                                        ["word_count"] = new JsonObject { ["type"] = "number" }
                                    },
                                    ["required"] = new JsonArray("word_count"),
                                    ["additionalProperties"] = false
                                },
                                Strict: true))
                    }
                });
            var reply = new StringBuilder();
            var firstChunk = true;
            var reasoning = false;
            MultimodalTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices[0];
                if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
                {
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
                }

                if (choice.Message.Content.Count == 0)
                {
                    continue;
                }

                if (firstChunk)
                {
                    firstChunk = false;
                    Console.Write("Assistant > ");
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
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
User > 你好
Reasoning > 用户输入的是“你好”，需要统计用户输入的字数信息。中文“你好”由两个汉字组成，没有标点符号和空格。
Assistant > {"word_count": 2}
Usage: in(61)/out(38)/reasoning(28)/total(99)
 */
