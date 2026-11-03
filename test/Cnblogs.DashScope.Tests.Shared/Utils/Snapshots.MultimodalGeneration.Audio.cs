using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

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
                    Model = "qwen3.5-omni-flash",
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
                    RequestId = "2417c00b-908e-9952-ad25-115301e186bc",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "别急，先深呼吸一下。这种被打断的感觉确实很让人抓狂。\n\n为了帮你尽快找回节奏，你可以试试这几招：\n\n1.  **物理隔绝**：如果条件允许，戴上降噪耳机（哪怕不放音乐），或者找个耳塞。这是最直接有效的方法。\n2.  **沟通缓冲**：如果是同事或家人制造的噪音，试着发个消息或小声说一句：“我现在有个紧急任务要赶，大概需要 XX 分钟，之后我再找你。”大多数人听到“紧急”二字都会懂得分寸。\n3.  **时间管理**：如果噪音无法避免，那就利用这段时间处理那些不需要高度集中注意力的琐事（比如整理文件、回复简单的邮件），等环境安静下来再攻坚。\n\n你是遇到了什么具体的噪音源吗？是装修声、键盘声，还是有人在旁边说话？告诉我具体情况，或许我能给你更针对性的建议。")
                                    }))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 56,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 12, AudioTokens: 44),
                        OutputTokens = 185,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 185),
                        TotalTokens = 241
                    }
                });
    }
}
