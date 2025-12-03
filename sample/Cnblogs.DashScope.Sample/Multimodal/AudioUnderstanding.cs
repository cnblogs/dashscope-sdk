using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal
{
    public class AudioUnderstanding : MultimodalSample
    {
        /// <inheritdoc />
        public override string Description => "Audio Understanding Sample";

        /// <inheritdoc />
        public override async Task RunAsync(IDashScopeClient client)
        {
            // upload file
            await using var audio = File.OpenRead("noise.wav");
            var ossLink = await client.UploadTemporaryFileAsync("qwen3-omni-30b-a3b-captioner", audio, "noise.wav");
            Console.WriteLine($"File uploaded: {ossLink}");
            var messages = new List<MultimodalMessage>
            {
                MultimodalMessage.User(
                    new List<MultimodalMessageContent>
                    {
                        // 也可以直接传入公网地址
                        MultimodalMessageContent.AudioContent(ossLink),
                    })
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
            messages.Add(MultimodalMessage.Assistant(
                new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent(reply.ToString()) }));
            if (usage != null)
            {
                Console.WriteLine(
                    $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/audio({usage.InputTokensDetails?.AudioTokens})/total({usage.TotalTokens})");
            }
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-12-01/f33b0b0e-7314-4ffd-b459-f418eb1e0866/sample.mp4

Assistant > The audio clip opens with a rapid, percussive metallic clatter, reminiscent of a typewriter or similar mechanical device, which continues in a steady rhythm throughout the recording. This clatter is slightly left-of-center in the stereo field and is accompanied by a faint, low-frequency hum, likely from a household appliance or HVAC system. The acoustic environment is a small, enclosed room with hard surfaces, indicated by the short, bright reverberation of both the clatter and the speaker’s voice. The audio quality is moderate, with a noticeable electronic hiss and some loss of high-frequency detail, but no digital distortion or clipping.

At the one-second mark, a male voice enters, positioned slightly right-of-center and closer to the microphone. He speaks in standard Mandarin, with a tone of weary exasperation: “哎 呀，这样我还怎么安静工作啊？” (“Aiyā, zěnyàng wǒ hái zěnme ānjìng gōngzuò a?”), which translates to “Oh, how can I possibly work quietly like this?” His speech is clear, with a slightly rising pitch on “安静” (“quietly”) and a falling pitch on “啊” (“a”), conveying a sense of complaint and fatigue. The accent is standard, with no regional inflection, and the voice is that of a young to middle-aged adult male.

Throughout the clip, the mechanical clatter remains constant and prominent, occasionally competing with the voice for clarity. There are no other sounds, such as footsteps, additional voices, or environmental noises, and the background is otherwise quiet. The interplay between the persistent mechanical noise and the speaker’s complaint creates a vivid sense of disruption and frustration, suggesting an environment where work is being impeded by an external, uncontrolled sound source.

Culturally, the use of Mandarin, standard pronunciation, and modern recording quality indicate a contemporary, urban Chinese setting. The language and tone are universally relatable, reflecting a common experience of being disturbed during work. The lack of regional markers or distinctive background noises suggests a generic, possibly domestic or office-like space, but with no clear indicators of a specific location or social context.

In summary, the audio portrays a modern Mandarin-speaking man, exasperated by a constant, distracting mechanical noise (likely a typewriter or similar device), attempting to work in a small, reverberant room. The recording’s technical and acoustic features reinforce the sense of disruption and frustration, while the language and setting suggest a contemporary, urban Chinese context.
Usage: in(160)/out(514)/audio(152)/total(674)
 */
