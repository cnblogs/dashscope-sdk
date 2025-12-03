using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class PrefixCompletionSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "Prefix completion sample";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            var messages = new List<TextChatMessage>
            {
                TextChatMessage.User("请补全这个 C# 函数，不要添加其他内容"),
                TextChatMessage.Assistant("public int Fibonacci(int n)", partial: true)
            };
            Console.WriteLine($"User > {messages[0].Content}");
            Console.Write($"Assistant > {messages[1].Content}");
            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
                });
            var reply = new StringBuilder();
            TextGenerationTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices![0];
                Console.Write(choice.Message.Content);
                reply.Append(choice.Message.Content);
                usage = chunk.Usage;
            }

            Console.WriteLine();
            messages.Add(TextChatMessage.Assistant(reply.ToString()));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
User > 请补全这个 C# 函数，不要添加其他内容
Assistant > public int Fibonacci(int n)
{
    if (n <= 1)
        return n;

    return Fibonacci(n - 1) + Fibonacci(n - 2);
}
Usage: in(31)/out(34)/reasoning()/total(65)
 */
