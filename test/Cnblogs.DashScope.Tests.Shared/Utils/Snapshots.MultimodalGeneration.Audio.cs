using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public static partial class Snapshots
    {
        public static partial class MultimodalGeneration
        {
            public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                    ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
                AudioNoSse = new(
                    "multimodal-generation-audio",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen-audio-turbo",
                        Input = new MultimodalInput
                        {
                            Messages =
                                new List<MultimodalMessage>
                                {
                                    MultimodalMessage.System(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent("You are a helpful assistant.")
                                        }),
                                    MultimodalMessage.User(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.AudioContent(
                                                "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                            MultimodalMessageContent.TextContent("这段音频在说什么，请用简短的语言回答")
                                        })
                                }
                        },
                        Parameters = new MultimodalParameters
                        {
                            Seed = 1234,
                            TopK = 100,
                            TopP = 0.81f
                        }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "6b6738bd-dd9d-9e78-958b-02574acbda44",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                                "这段音频在说中文，内容是\"没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网\"。")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokens = 786,
                            OutputTokens = 38,
                            AudioTokens = 752
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                    ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
                AudioSse = new(
                    "multimodal-generation-audio",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen-audio-turbo",
                        Input = new MultimodalInput
                        {
                            Messages =
                                new List<MultimodalMessage>
                                {
                                    MultimodalMessage.System(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent("You are a helpful assistant.")
                                        }),
                                    MultimodalMessage.User(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.AudioContent(
                                                "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                            MultimodalMessageContent.TextContent("这段音频的第一句话说了什么？")
                                        })
                                }
                        },
                        Parameters = new MultimodalParameters
                        {
                            Seed = 1234,
                            TopK = 100,
                            TopP = 0.81f,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "bb6ab962-af57-99f1-9af8-eb7016ebc18e",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent("第一句话说了没有我互联网。")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokens = 783,
                            OutputTokens = 7,
                            AudioTokens = 752
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                    ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
                AudioCaptionSse = new(
                    "multimodal-generation-audio-caption",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen3-omni-30b-a3b-captioner",
                        Input = new MultimodalInput
                        {
                            Messages =
                                new List<MultimodalMessage>
                                {
                                    MultimodalMessage.User(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.AudioContent(
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20240916/xvappi/%E8%A3%85%E4%BF%AE%E5%99%AA%E9%9F%B3.wav"),
                                        })
                                }
                        },
                        Parameters = new MultimodalParameters { IncrementalOutput = true }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "5159c491-56f4-47a3-8197-0c81114424d8",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                                "The audio clip is a short, moderately clear recording—approximately 6 seconds in length—set in a small, reverberant indoor space, likely a private home or office. The dominant feature is a series of loud, metallic, rhythmic hammering noises, occurring at a regular tempo of about 120 beats per minute, with each blow exhibiting a sharp, high-frequency attack and brief metallic resonance. These impacts are highly regular, suggesting the use of a power tool such as a pneumatic nail gun or electric hammer, rather than a hand-held hammer.\n\nOverlaying this mechanical sound, a single male voice, speaking in standard Mandarin Chinese, delivers a brief, exasperated complaint. His tone is weary and resigned, marked by a sigh at the start and a drawn-out, slightly whining intonation. The spoken phrase, “哎呀，这样我还怎么安静工作啊？” (“Aiyah, how can I work quietly like this?”), directly addresses the disruptive nature of the hammering, indicating a personal, informal relationship with the person or people responsible for the noise.\n\nThe recording is of moderate quality, with both the voice and hammering clear and intelligible, though the metallic sounds are slightly distorted and clipped at their loudest peaks. There is a faint, consistent electronic hiss throughout, and a low-frequency hum—potentially from nearby electrical equipment—can be detected. The room’s acoustics are “live,” with short, bright reverberation tails on both the voice and hammering, suggesting hard surfaces and a small, enclosed space.\n\nNo other human voices, background conversations, or environmental cues are present, reinforcing the impression of a solitary, private setting. The overall atmosphere is one of mild frustration and resignation, with the speaker’s tone and the content of his complaint expressing the classic annoyance of being unable to concentrate or work in peace due to disruptive, persistent noise.\n\nIn summary, the recording captures a brief, personal moment of complaint: a Mandarin-speaking man, working alone in a quiet, reverberant indoor space, is interrupted by the rhythmic operation of a power tool, prompting him to voice his frustration at the disruption to his peace and productivity.")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokens = 160,
                            InputTokensDetails = new MultimodalInputTokenDetails(AudioTokens: 152, TextTokens: 8),
                            OutputTokens = 442,
                            OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 442),
                            TotalTokens = 602
                        }
                    });
        }
    }
}
