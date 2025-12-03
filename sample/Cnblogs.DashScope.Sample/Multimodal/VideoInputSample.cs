using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal
{
    public class VideoInputSample : MultimodalSample
    {
        /// <inheritdoc />
        public override string Description => "Video input sample";

        /// <inheritdoc />
        public override async Task RunAsync(IDashScopeClient client)
        {
            // upload file
            await using var video = File.OpenRead("sample.mp4");
            var ossLink = await client.UploadTemporaryFileAsync("qwen3-vl-plus", video, "sample.mp4");
            Console.WriteLine($"File uploaded: {ossLink}");
            var messages = new List<MultimodalMessage>();
            messages.Add(
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>
                    {
                        MultimodalMessageContent.VideoContent(ossLink, fps: 2),
                        MultimodalMessageContent.TextContent("这段视频的内容是什么？")
                    }));
            var completion = client.GetMultimodalGenerationStreamAsync(
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Model = "qwen3-vl-plus",
                    Input = new MultimodalInput() { Messages = messages },
                    Parameters = new MultimodalParameters()
                    {
                        IncrementalOutput = true,
                        EnableThinking = true,
                        VlHighResolutionImages = true
                    }
                });
            var reply = new StringBuilder();
            var reasoning = false;
            MultimodalTokenUsage? usage = null;
            await foreach (var chunk in completion)
            {
                var choice = chunk.Output.Choices[0];
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

                if (choice.Message.Content.Count == 0)
                {
                    continue;
                }

                Console.Write(choice.Message.Content[0].Text);
                reply.Append(choice.Message.Content[0].Text);
                usage = chunk.Usage;
            }

            Console.WriteLine();
            messages.Add(
                MultimodalMessage.Assistant(
                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent(reply.ToString()) }));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/video({usage.VideoTokens})/reasoning({usage.OutputTokensDetails?.ReasoningTokens})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-15/0b590ae2-3904-4919-a886-d0a2e6ede1b8/sample.mp4
Reasoning > 用户现在需要分析这段视频的内容。首先看提供的帧，是连续的多个画面，显示一位女性在户外，背景是建筑物。她的表情和动作有变化：从微笑到大笑，嘴巴开合，应该是说话或表达情绪。右上角有“通义·AI合成”标识，说明是AI生成的视频。

首先确认元素：人物穿着粉色开衫、白色内搭，短发，背景模糊建筑。视频中人物表情动态变化，比如微笑、露齿笑、嘴巴动作，可能是模拟说话或自然表情变化。因为是AI合成，所以是生成的动态视频，展示人物的面部表情变化，可能用于演示AI生成的视频效果，比如表情自然过渡、口型同步等。

总结：这段视频是AI合成的内容，展示一位女性在户外场景中，面部表情随时间变化（微笑、大笑、说话等动态表情），背景为模糊的建筑物，整体呈现自然的表情过渡效果，用于展示AI生成视频的能力。

Assistant > 这段视频是**AI合成**的内容，展示了以下信息：

- **主体**：一位留着齐肩短发、穿着浅粉色针织开衫与白色内搭的女性，佩戴细项链与耳饰。
- **场景**：背景为模糊的户外建筑（暖色调墙面与立柱），光线明亮，营造出自然的日常氛围。
- **动态表现**：女性的面部表情随时间变化，呈现自然的互动感——从微笑到露齿大笑，嘴巴开合、眼神灵动，模拟了“说话”或“表达情绪”的动态效果（如微笑、大笑、唇部动作等）。
- **技术标识**：画面右上角有“通义·AI合成”字样，表明这是由AI技术生成的虚拟视频内容，用于展示AI在**表情自然度、口型同步、光影渲染**等方面的合成能力。


简言之，这是一段**AI生成的虚拟人物表情演示视频**，核心是通过动态表情变化展现AI合成技术的逼真效果。
Usage: in(3606)/out(436)/video(3586)/reasoning(211)/total(4042)
 */
