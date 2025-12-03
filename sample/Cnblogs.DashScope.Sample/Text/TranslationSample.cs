using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class TranslationSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "Translate with Qwen-MT models";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            var messages = new List<TextChatMessage>
            {
                TextChatMessage.User(
                    "博客园创立于2004年1月，是一个面向开发者群体的技术社区。博客园专注于为开发者服务，致力于为开发者打造一个纯净的技术学习与交流社区，帮助开发者持续学习专业知识，不断提升专业技能。博客园的使命是帮助开发者用代码改变世界。")
            };
            Console.WriteLine("User > " + messages[0].Content);
            var completion = await client.GetTextCompletionAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-mt-plus",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters()
                    {
                        ResultFormat = "message",
                        TranslationOptions = new TextGenerationTranslationOptions()
                        {
                            Domains =
                                "This is a summary of a website for programmers, use formal and professional tones",
                            SourceLang = "zh",
                            TargetLang = "en",
                            Terms = new List<TranslationReference> { new("博客园", "Cnblogs.com") },
                            TmList = new List<TranslationReference>
                            {
                                new("代码改变世界", "Coding Changes the World")
                            }
                        }
                    }
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
User > 博客园创立于2004年1月，是一个面向开发者群体的技术社区。博客园专注于为开发者服务，致力于为开发者打造一个纯净的技术学习与交流社区，帮助开发者持续学习专业知识，不断提升专业技能。博客园的使命是帮助开发者用代码改变世界。
Assistant > Cnblogs.com was founded in January 2004 and is a technology community aimed at developers. Cnblogs.com focuses on serving developers, committed to creating a pure technology learning and communication community for them, helping developers continuously learn professional knowledge and improve their professional skills. Cnblogs.com's mission is to help developers change the world through coding.
Usage: in(207)/out(72)/total(279)
 */
