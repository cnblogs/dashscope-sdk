using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class ChatThinkingBudgetSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "Chat completion with thinking budget";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            const int budget = 10;
            Console.WriteLine($"Set thinking budget to {budget} tokens");
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
                            ThinkingBudget = budget,
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
}

/*
Set thinking budget to 10 tokens
User > 你是谁？
Reasoning > 好的，用户问我“你是谁？”，我
Assistant > 我是通义千问，是阿里巴巴集团研发的超大规模语言模型，可以回答问题、创作文字、编程、逻辑推理等多种任务。我旨在为用户提供帮助和便利。有什么我可以帮您的吗？
Usage: in(21)/out(59)/reasoning(10)/total(80)
 */
