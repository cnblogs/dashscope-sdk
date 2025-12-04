using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class JsonOutputSample : TextSample
{
    /// <inheritdoc />
    public override string Description => "JSON output text sample";

    /// <inheritdoc />
    public async override Task RunAsync(IDashScopeClient client)
    {
        var messages = new List<TextChatMessage>();
        messages.Add(TextChatMessage.System("使用 JSON 输出用户输入的字数信息"));
        while (true)
        {
            Console.Write("User > ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a user input.");
                return;
            }

            messages.Add(TextChatMessage.User(input));
            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-plus",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters()
                    {
                        ResultFormat = "message",
                        ResponseFormat = DashScopeResponseFormat.Json,
                        IncrementalOutput = true
                    }
                });
            var reply = new StringBuilder();
            var firstChunk = true;
            TextGenerationTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices![0];
                if (firstChunk)
                {
                    firstChunk = false;
                    Console.Write("Assistant > ");
                }

                Console.Write(choice.Message.Content);
                reply.Append(choice.Message.Content);
                usage = chunk.Usage;
            }

            Console.WriteLine();
            messages.Add(TextChatMessage.Assistant(reply.ToString()));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
User > 你好
Assistant > {"word_count": 2}
Usage: in(25)/out(7)/total(32)
 */
