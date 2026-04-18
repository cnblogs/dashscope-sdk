using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatStreamSample : TextSample
{
    /// <inheritdoc />
    public override string Description => "Chat completion with stream output";

    /// <inheritdoc />
    public async override Task RunAsync(IDashScopeClient client)
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
                        IncrementalOutput = true
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

/*
User > 你好
Reasoning > 好的，用户发来“你好”，我需要友好回应。首先，应该用中文回复，保持自然。可以问好并询问有什么可以帮助的，这样既礼貌又开放。注意不要用太正式的语言，让对话轻松一些。同时，要确保回复简洁，避免冗长。检查有没有需要特别注意的地方，比如用户可能的需求或之前的对话历史，但这里看起来是第一次交流。所以，确定回复内容应该是：“你好！有什么我可以帮你的吗？” 这样既友好 又明确，鼓励用户进一步说明需求。
Assistant > 你好！有什么我可以帮你的吗？
Usage: in(19)/out(125)/reasoning(112)/total(144)
 */
