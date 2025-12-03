using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class CodeInterpreterSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "Chat with code interpreter";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            var messages = new List<TextChatMessage>();
            const string input = "123的21次方是多少？";
            Console.Write($"User > {input}");
            messages.Add(TextChatMessage.User(input));
            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Model = "qwen3-max-preview",
                    Input = new TextGenerationInput { Messages = messages },
                    Parameters = new TextGenerationParameters
                    {
                        ResultFormat = "message",
                        EnableThinking = true,
                        EnableCodeInterpreter = true,
                        IncrementalOutput = true
                    }
                });
            var reply = new StringBuilder();
            var codeGenerated = false;
            var reasoning = false;
            TextGenerationTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices![0];
                var tool = chunk.Output.ToolInfo?.FirstOrDefault();
                if (codeGenerated == false && tool?.CodeInterpreter != null)
                {
                    Console.WriteLine(tool.CodeInterpreter.Code);
                    codeGenerated = true;
                }

                if (string.IsNullOrEmpty(choice.Message.ReasoningContent) == false)
                {
                    // reasoning
                    if (reasoning == false)
                    {
                        Console.WriteLine();
                        Console.Write("Reasoning > ");
                        reasoning = true;
                    }

                    Console.Write(choice.Message.ReasoningContent);
                    continue;
                }

                if (reasoning && string.IsNullOrEmpty(choice.Message.Content.Text) == false)
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
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/plugins({usage.Plugins?.CodeInterpreter?.Count})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
User > 123的21次方是多少？
Reasoning > 用户问的是123的21次方是多少。这是一个大数计算问题，我需要使用代码计算器来计算这个值。

我需要调用code_interpreter函数，传入计算123**21的Python代码。
Code > 123**21
用户询问123的21次方是多少，我使用代码计算器计算出了结果。结果是一个非常大的数字：77269364466549865653073473388030061522211723

我应该直接给出这个结果，因为这是一个精确的数学计算问题，不需要额外的解释或
Assistant > 123的21次方是：77269364466549865653073473388030061522211723
Usage: in(704)/out(234)/reasoning(142)/plugins(1)/total(938)
 */
