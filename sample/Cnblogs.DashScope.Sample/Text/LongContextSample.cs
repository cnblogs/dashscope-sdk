using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Text
{
    public class LongContextSample : TextSample
    {
        /// <inheritdoc />
        public override string Description => "File upload and long context model sample";

        /// <inheritdoc />
        public async override Task RunAsync(IDashScopeClient client)
        {
            Console.WriteLine("Uploading file1...");
            var file1 = await client.OpenAiCompatibleUploadFileAsync(File.OpenRead("1024-1.txt"), "file1.txt");
            Console.WriteLine("Uploading file2...");
            var file2 = await client.OpenAiCompatibleUploadFileAsync(File.OpenRead("1024-2.txt"), "file2.txt");
            Console.WriteLine($"Uploaded, file1 id: {file1.Id.ToUrl()},  file2 id: {file2.Id.ToUrl()}");

            await EnsureFileProcessedAsync(client, file1);
            await EnsureFileProcessedAsync(client, file2);

            var messages = new List<TextChatMessage>
            {
                TextChatMessage.System("You are a helpful assistant"),
                TextChatMessage.File(file1.Id),
                TextChatMessage.File(file2.Id),
                TextChatMessage.User("这两篇文章分别讲了什么？")
            };

            messages.ForEach(m => Console.WriteLine($"{m.Role} > {m.Content}"));
            var completion = client.GetTextCompletionStreamAsync(
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                {
                    Model = "qwen-long",
                    Input = new TextGenerationInput() { Messages = messages },
                    Parameters = new TextGenerationParameters() { ResultFormat = "message", IncrementalOutput = true }
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

            // Deleting files
            Console.Write("Deleting file1...");
            var result = await client.OpenAiCompatibleDeleteFileAsync(file1.Id);
            Console.WriteLine(result.Deleted ? "Success" : "Failed");
            Console.Write("Deleting file2...");
            result = await client.OpenAiCompatibleDeleteFileAsync(file2.Id);
            Console.WriteLine(result.Deleted ? "Success" : "Failed");
        }

        private static async Task EnsureFileProcessedAsync(
            IDashScopeClient client,
            DashScopeFile file,
            int timeoutInSeconds = 5)
        {
            if (file.Status == "processed")
            {
                return;
            }

            var timeout = Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds));
            while (timeout.IsCompleted == false)
            {
                var realtime = await client.OpenAiCompatibleGetFileAsync(file.Id);
                if (realtime.Status == "processed")
                {
                    return;
                }

                await Task.Delay(1000);
            }

            throw new InvalidOperationException($"File not processed within timeout, fileId: {file.Id}");
        }
    }
}

/*
Uploading file1...
Uploading file2...
Uploaded, file1 id: fileid://file-fe-b87a5c12cc354533bd882f04,  file2 id: fileid://file-fe-f5269f9996d544c4aecc5f80
system > You are a helpful assistant
system > fileid://file-fe-b87a5c12cc354533bd882f04
system > fileid://file-fe-f5269f9996d544c4aecc5f80
user > 这两篇文章分别讲了什么？
这两篇文章都围绕“中国程序员节”的设立展开，但内容侧重点不同：

**第一篇文章《file1.txt》：**
这篇文章是一篇征求意见稿，标题为《中国程序员节，10月24日，你同意吗？》。文章回顾了此前关于设立中国程序员节的讨论背景——受俄罗斯程序员节（每年第256天）启发，有网友提议设立中国的程序员 节。文中提到曾有人建议定在10月10日（因为“1010”类似二进制），但作者认为10月24日更具意义：
- 因为1024 = 2^10，是计算机中“1K”的近似值；
- 1024在二进制、八进制和十六进制中都有特殊表示；
- 节日时间上避开国庆后的调整期。
因此，文章向读者征求是否同意将**10月24日**作为中国程序员节，并邀请大家参与投票和提出庆祝活动建议。

**第二篇文章《file2.txt》：**
这篇文章是第一篇的后续，标题为《程序员节，10月24日！》，属于正式 announcement（公告）。它宣布：
- 根据前一次讨论的反馈结果，正式确定将**每年的10月24日**定为“中国程序员节”；
- 博客园将在该日组织线上庆祝活动；
- 文章进一步升华主题，强调程序员的社会价值和责任感，呼吁尊重程序员群体，肯定他们是“用代码改变世界的人”，并表达了对技术创造力的敬意。

**总结：**
- 第一篇是**征求意见**，探讨是否将10月24日设为中国程序员节；
- 第二篇是**正式确认**节日日期，并倡导庆祝与认同程序员的价值。
Usage: in(513)/out(396)/reasoning()/total(909)
Deleting file1...Success
Deleting file2...Success
 */
