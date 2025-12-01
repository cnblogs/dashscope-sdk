using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class AudioUnderstanding : MultimodalSample
{
    /// <inheritdoc />
    public override string Description => "Audio Understanding Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var video = File.OpenRead("noise.wav");
        var ossLink = await client.UploadTemporaryFileAsync("qwen3-omni-30b-a3b-captioner", video, "sample.mp4");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages = new List<MultimodalMessage>
        {
            MultimodalMessage.User(
            [
                // 也可以直接传入公网地址
                MultimodalMessageContent.AudioContent(ossLink),
            ])
        };
        var completion = client.GetMultimodalGenerationStreamAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen3-omni-30b-a3b-captioner",
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters() { IncrementalOutput = true, }
            });
        var reply = new StringBuilder();
        var first = true;
        MultimodalTokenUsage? usage = null;
        await foreach (var chunk in completion)
        {
            var choice = chunk.Output.Choices[0];
            if (first)
            {
                first = false;
                Console.WriteLine();
                Console.Write("Assistant > ");
            }

            if (choice.Message.Content.Count == 0)
            {
                continue;
            }

            Console.Write(choice.Message.Content[0].Text);
            reply.Append(choice.Message.Content[0].Text);
            usage = chunk.Usage;
        }

        Console.WriteLine();
        messages.Add(MultimodalMessage.Assistant([MultimodalMessageContent.TextContent(reply.ToString())]));
        if (usage != null)
        {
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/audio({usage.InputTokensDetails?.AudioTokens})/total({usage.TotalTokens})");
        }
    }
}
