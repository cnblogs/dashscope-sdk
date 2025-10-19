using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text;

public class ChatReasoningSample : ISample
{
    /// <inheritdoc />
    public string Description => "Chat with reasoning content";

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
            var completion = await client.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters() { ResultFormat = "message", EnableThinking = true }
                });
            Console.WriteLine("Reasoning > " + completion.Output.Choices![0].Message.ReasoningContent);
            Console.WriteLine("Assistant > " + completion.Output.Choices![0].Message.Content);
            var usage = completion.Usage;
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
            }

            messages.Add(TextChatMessage.Assistant(completion.Output.Choices[0].Message.Content));
        }
    }
}

/*
User > 你好，今天感觉怎么样？
Reasoning > 好的，用户问“你好，今天感觉怎么样？”，我需要先理解他的意图。他可能是在关心我的状态，或者想开始一段对话。作为AI助手，我没有真实的情感，但应该以友好和积极的方式回应。

首先，我应该感谢他的问候，然后说明自己没有真实的情感，但愿意帮助他。接下来，可以询问他的情况，表现出关心，这样能促进进一步的交流。同时，保持语气自然，避免过于机械。

要注意用户可能的深层需求，比如他可能想寻求帮助，或者只是闲聊。所以回应要开放，让他知道我随时准备协助。另外，使用表情符号可以增加亲切感，但不要过多。

最后，确保回答简洁，不过于冗长，同时保持友好和专业。这样用户会觉得被重视，并且更愿意继续对话。
Assistant > 你好呀！虽然我没有真实的情感体验，但很高兴能和你聊天！今天过得怎么样呢？有什么我可以帮你的吗？
Usage: in(24)/out(203)/reasoning(169)/total(227)
 */
