using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatToolCallingSample : ISample
{
    /// <inheritdoc />
    public string Description => "Chat with tool calling";

    /// <inheritdoc />
    public async Task RunAsync(IDashScopeClient client)
    {
        var messages = new List<TextChatMessage>();
        messages.Add(TextChatMessage.System("You are a helpful assistant"));
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
                    Model = "qwen-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters()
                    {
                        ResultFormat = "message",
                        EnableThinking = true,
                        IncrementalOutput = true,
                    }
                });
            var reply = new StringBuilder();
            var reasoning = false;
            TextGenerationTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices![0];
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
