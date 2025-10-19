using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatSample : ISample
{
    /// <inheritdoc />
    public string Description => "Basic chat completion";

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
                Console.WriteLine("使用默认输入：你是谁？");
                input = "你是谁？";
            }

            messages.Add(TextChatMessage.User(input));
            var completion = await client.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters() { ResultFormat = "message" }
                });
            Console.WriteLine("Assistant > " + completion.Output.Choices![0].Message.Content);
            var usage = completion.Usage;
            if (usage != null)
            {
                Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
            }

            messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));
        }
    }
}

/*
 * User > 你好，你今天过的怎么样？
 * Assistant > 你好！谢谢你关心。虽然我是一个AI助手，没有真实的情感和体验，但我非常高兴能和你交流。今天过得挺好的，因为我可以和很多像你一样的朋友聊天，帮助大家解决问题，分享知识。你今天过得怎么样呢？有什么我可以帮你的吗？
 * Usage: in(29)/out(59)/total(88)
 */
