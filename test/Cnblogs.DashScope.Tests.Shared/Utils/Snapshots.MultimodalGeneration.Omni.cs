using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class MultimodalGeneration
    {
        private const string AudioUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250211/tixcef/cherry.wav";

        private const string ImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241022/emyrja/dog_and_girl.jpeg";

        private const string OmniMixedInputText =
            "**图片内容描述：**\n\n这是一张在海滩上拍摄的温馨照片，整体笼罩在柔和的暖色逆光里，太阳位于女子身后，给她的头发镀上一层金边，推测是日出或日落时分。画面右侧，一位长发女子盘腿坐在沙滩上，身穿" +
            "深色格纹衬衫，赤着脚，脸上带着灿烂而放松的笑容，眼睛微眯，神情十分愉悦。她伸出一只手迎向对面的狗，正与它完成一个类似“击掌”或“握手”的互动动作，另一只手则微微抬起，像是在示意或准备给零食。\n" +
            "\n画面左侧是一只浅黄色的拉布拉多犬，端正地坐着，身上佩戴着一条印有彩色爪印图案的胸背带，牵引绳松松地垂落在沙地上。它抬起一只前爪，准确地搭在女子的手掌上，一人一狗面对面，默契十足。\n\n背景是平" +
            "静的海面，一道海浪正轻轻拍向岸边，天空泛白、光线明亮。前景的沙滩上布满了脚印和自然的起伏纹理，被斜射的阳光勾勒出柔和的明暗。整幅图传递出人与宠物之间亲密、治愈的氛围。\n\n**音频内容转述：**" +
            "\n\n音频是一位女性的口语化描述，语气轻松、带不少语气词。她说道：“嗯，这幅图啊，是在海滩上，有个女人和一只狗在玩呢，背景是海浪拍打着沙滩，天空有点发白，应该是日出或者日落的时候吧。你要是还有啥" +
            "想知道的尽管问哈。” 也就是说，她在用大白话概括画面的地点、人物、动物、背景和可能的时间，并在结尾热情地表示，如果听者还想了解什么，可以继续问她。";

        private const string OmniMixedInputReasoning =
            "用户要求两部分：描述图片内容，以及说明音频在说什么。我需要先仔细观察图像，再准确转述音频。\n\n观察图像：海滩场景，暖色调光线，像是日出或日落的逆光，太阳在女子身后形成光晕。女子坐沙滩上，穿格子" +
            "衬衫（黑白/深色格纹），长发，赤脚，面带微笑，闭眼或微眯，神情愉悦。她伸出一只手，与狗的爪子相触，像是击掌或握手动作。另一只手似乎拿着零食或做手势。狗是浅黄色拉布拉多，坐着，戴花纹胸背带（带爪" +
            "印图案的彩色背带），系着牵引绳，绳子落在沙地上。背景是海浪和发白的天空，沙滩上有脚印和起伏。整体温馨，人与宠物互动。\n\n观察音频：女声，口语化，带语气词“嗯”“呢”“吧”“哈”。内容大致是：这" +
            "幅图啊，是在海滩上，有个女人和一只狗在玩呢，背景是海浪拍打着沙滩，天空有点发白，应该是日出或者日落的时候吧。你要是还有啥想知道的尽管问哈。\n\n我需要把这两部分清晰呈现。描述要细致但不过度冗长，" +
            "音频转述要忠实，保留口语风格。我可以稍微点出音频的语气特征，让回答更完整。\n\n结构上，先图片描述，再音频内容，分两段或加小标题，便于阅读。无需列表，自然段落即可。语言用中文。\n\n我要确保不遗漏" +
            "关键视觉细节：逆光、格子衫、赤脚、击掌动作、胸背带爪印图案、牵引绳、海浪、发白天空。音频部分逐句对应。\n\n现在组织语言，力求流畅自包含。";

        private const string OmniAudioInputText =
            "这段音频的内容是说话人在描述一幅图片：\n\n“嗯，这幅图啊，是在海滩上，有个女人和一只狗在玩呢。背景是海浪拍打着沙滩，天空有点发白，应该是日出或者日落的时候吧。你要是还有啥想知道的，尽管问哈。”" +
            "\n\n简单来说，就是在讲一张海滩上女人和狗玩耍的照片，并推测时间是日出或日落，最后表示愿意回答更多问题。";

        private const string OmniAudioInputReasoning =
            "The user wants me to transcribe or summarize the Chinese audio.\n\n1.  **Transcribe the audio:** \"" +
            "嗯，这幅图啊，是在海滩上，有个女人和一只狗在玩呢。背景是海浪拍打着沙滩，天空有点发白，应该是日出或者日落的时候吧。你要是还有啥想知道的，尽管问哈。\"\n2.  **Translate/Summa" +
            "rize:** The speaker is describing a picture. It shows a woman and a dog playing on a beach. The " +
            "background has waves hitting the sand, and the sky is whitish, suggesting sunrise or sunset. The" +
            " speaker invites further questions.\n3.  **Formulate the response:** Provide the transcription an" +
            "d a brief summary in Chinese.";

        private const string OmniSearchReasoning =
            "用户要求查询今天的日期和星期。系统提示中已经给出了当前实际时间：Friday, November 27, 2026。我可以直接回答，无需调用工具。但为了验证星期几是否正确，可以简单计算：202" +
            "6年11月27日。让我确认一下。\n\n已知2026年1月1日是星期四（可以推算）。或者用蔡勒公式/基姆拉尔森计算。\n\n更简单：2025年11月27日是星期几？我知道2026年11月27日是星期五" +
            "（系统给出）。我可以信任系统信息。\n\n不过用户说“请查询”，可能希望我使用工具？但工具是网页搜索，没必要。直接回答即可，同时可以说明依据。\n\n我可以用中文回答：今天是2026年11月27日，星" +
            "期五。\n\n不需要工具调用。\n";

        private const string OmniSearchSharedReasoning =
            "The user asks me to look up today's date and day of the week. I have the system reminder that sa" +
            "ys the current actual time is Friday, November 27, 2026. But they explicitly ask me to \"查询\" (que" +
            "ry/look up). I could use web_search to verify, but I already know from the system prompt. Howeve" +
            "r, the instruction says \"请查询今天的日期和星期\" — they want me to check. I can simply answer based on the " +
            "provided current date. But maybe doing a quick search adds verification. It's probably fine to j" +
            "ust answer directly since I have the date. However, to be safe and responsive to \"查询\", I could d" +
            "o a search. But searching for \"today's date\" often returns unreliable results. The system gives " +
            "me the actual time: Friday, November 27, 2026.\n\nI'll answer directly, noting it's Friday, Novemb" +
            "er 27, 2026. Maybe also mention Chinese: 2026年11月27日，星期五. I could add lunar calendar info? Not n" +
            "ecessary. Keep it simple. No tool needed.\n";

        /// <summary>
        /// An image and an audio in one turn of qwen3.8-omni-flash via the DashScope API, which
        /// answers with the reasoning_content of its thinking.
        /// </summary>
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OmniMixedInputNoSse = new(
                "multimodal-generation-omni-mixed-input",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-omni-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.ImageContent(ImageUrl),
                                    MultimodalMessageContent.AudioContent(AudioUrl),
                                    MultimodalMessageContent.TextContent("请描述图片内容，并告诉我音频在说什么。")
                                })
                        }
                    },
                    Parameters = new MultimodalParameters()
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "7ff5bce8-6fd0-992a-a4fa-0206f0a4827a",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(OmniMixedInputText)
                                    },
                                    OmniMixedInputReasoning))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 2680,
                        OutputTokens = 716,
                        TotalTokens = 3396,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 63, AudioTokens: 114),
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 367, TextTokens: 349),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 1792)
                    }
                });

        /// <summary>
        /// qwen3.8-omni-flash thinks about an audio via the DashScope API: the reasoning_content
        /// of the frames comes before the text.
        /// </summary>
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OmniAudioInputSse = new(
                "multimodal-generation-omni-audio-input",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-omni-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.AudioContent(AudioUrl),
                                    MultimodalMessageContent.TextContent("这段音频在说什么")
                                })
                        }
                    },
                    Parameters = new MultimodalParameters { IncrementalOutput = true }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "5b02758c-2401-9923-ae16-cd241a3aa63c",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(OmniAudioInputText)
                                    },
                                    OmniAudioInputReasoning))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 170,
                        OutputTokens = 239,
                        TotalTokens = 409,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 56, AudioTokens: 114),
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 148, TextTokens: 91),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 0)
                    }
                });

        /// <summary>
        /// The web search of qwen3.8-omni-flash via the DashScope API (the agent strategy, the
        /// workspace domain). The stream opens with the default frame of the gateway, the output
        /// without the envelope of a response, which reads as a frame without content; the snapshot
        /// is the joined stream.
        /// </summary>
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OmniSearchSse = new(
                "multimodal-generation-omni-search",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-omni-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.TextContent("请查询今天的日期和星期。")
                                })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        IncrementalOutput = true,
                        EnableSearch = true,
                        SearchOptions = new SearchOptions { SearchStrategy = "agent" }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "4150fc95-0e54-9233-866b-d14fc7c59f50",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("今天是 **2026年11月27日，星期五**。")
                                    },
                                    OmniSearchReasoning))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 373,
                        OutputTokens = 210,
                        TotalTokens = 583,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 373),
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 191, TextTokens: 19),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 0)
                    }
                });

        /// <summary>
        /// The same web search on the shared domain, whose default frame carries the envelope but
        /// an empty string as the content instead of an array; the snapshot is the joined stream.
        /// </summary>
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OmniSearchSharedSse = new(
                "multimodal-generation-omni-search-shared",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-omni-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.TextContent("请查询今天的日期和星期。")
                                })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        IncrementalOutput = true,
                        EnableSearch = true,
                        SearchOptions = new SearchOptions { SearchStrategy = "agent" }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "ddc990c6-e6c1-901b-b21b-c49418632098",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("今天是 **2026 年 11 月 27 日，星期五**。")
                                    },
                                    OmniSearchSharedReasoning))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 373,
                        OutputTokens = 263,
                        TotalTokens = 636,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 373),
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 239, TextTokens: 24),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 0)
                    }
                });
    }
}
