using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class DataMiningSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "Data Mining with Qwen-Doc-Turbo";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            Console.WriteLine("Uploading file1...");
            var file1 = await client.OpenAiCompatibleUploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
            var messages = new List<TextChatMessage>
            {
                TextChatMessage.System("You are a helpful assistant"),
                TextChatMessage.File(file1.Id),
                TextChatMessage.User("这篇文章讲了什么，整理成一个 JSON，需要包含标题（title）和摘要（description）")
            };
            messages.ForEach(m => Console.WriteLine($"{m.Role} > {m.Content}"));
            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-doc-turbo",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters()
                    {
                        ResultFormat = "message",
                        IncrementalOutput = true,
                    }
                });
            var reply = new StringBuilder();
            var first = true;
            TextGenerationTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices![0];
                if (first)
                {
                    first = false;
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
                Console.WriteLine($"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/total({usage.TotalTokens})");
            }

            // Deleting files
            Console.Write("Deleting file1...");
            var result = await client.OpenAiCompatibleDeleteFileAsync(file1.Id);
            Console.WriteLine(result.Deleted ? "Success" : "Failed");
        }
    }
}

/*
Uploading file1...
system > You are a helpful assistant
system > fileid://file-fe-893f2ba62e17498fa9bd17f8
user > 这篇文章讲了什么，整理成一个 JSON，需要包含标题（title）和摘要（description）

Assistant > ```json
{
  "title": "中国程序员节日期的讨论与投票",
  "description": "文章讨论了将10月24日确定为中国程序员节的提议。由于1024在计算机领域具有特殊意义（1K=1024，二进制、八进制和十六进制表示），且该日期位于国庆假期之后，便于庆祝活动的开 展，因此发起投票征求网友意见。"
}
```
Usage: in(360)/out(97)/total(457)
Deleting file1...Success
 */
