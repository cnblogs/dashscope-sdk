using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class MultimodalGeneration
    {
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            QwenAsrNoSse = new(
                "multimodal-generation-qwen-asr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-asr-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.System(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("这是一段古诗朗读的音频") }),
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.AudioContent("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        AsrOptions = new AsrOptions { Language = "zh", EnableItn = true }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "c216b8d0-a621-9a00-a4df-d670e3f985e7",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。") })
                                with
                                {
                                    Annotations = new List<MultimodalAnnotation> { new("zh", "audio_info", "angry") }
                                })
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                            InputTokens = 241,
                            OutputTokens = 37,
                            TotalTokens = 278,
                            AudioTokens = 229,
                            Seconds = 9,
                            InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 12, AudioTokens: 229),
                            OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 37),
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            QwenAsrSse = new(
                "multimodal-generation-qwen-asr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-asr-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.AudioContent("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        IncrementalOutput = true,
                        AsrOptions = new AsrOptions { EnableItn = false }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "4907bdfc-8b64-914b-ac86-6e23880fa8b0",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。") }))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                            InputTokens = 229,
                            OutputTokens = 37,
                            TotalTokens = 266,
                            AudioTokens = 229,
                            Seconds = 9,
                            InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 0, AudioTokens: 229),
                            OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 37),
                    }
                });

        // Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash recorded speech recognition, same endpoint but typed contents
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            RecordedAsrNoSse = new(
                "recorded-speech-recognition",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-audio-3.1-asr-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.InputAudioContent("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        Format = "mp3",
                        Vocabulary = new Dictionary<string, int> { ["青海"] = 5, ["楼兰"] = 5 }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "9eec34cd-20d5-9256-afc3-29c7049f291b",
                    Output = new MultimodalOutput(
                        null!,
                        Text: "青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。",
                        Sentence:
                        new SpeechRecognitionSentence(
                            320,
                            9190,
                            "青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。",
                            null,
                            true,
                            null,
                            null,
                            new[]
                            {
                                new SpeechRecognitionWord("青", 320, 580, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("海", 580, 840, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("长", 840, 1080, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("云", 1080, 1360, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("暗", 1360, 1640, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("雪山", 1640, 2360, "，", Fixed: true),
                                new SpeechRecognitionWord("孤", 2360, 2600, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("城", 2600, 2880, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("遥", 2880, 3080, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("望", 3080, 3520, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("玉", 3520, 3800, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("门关", 3800, 4800, "。", Fixed: true),
                                new SpeechRecognitionWord("黄沙", 4800, 5280, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("百战", 5280, 5880, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("穿", 5880, 6160, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("金", 6160, 6400, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("甲", 6400, 7040, "，", Fixed: true),
                                new SpeechRecognitionWord("不破", 7040, 7520, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("楼", 7520, 7680, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("兰", 7680, 8160, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("终", 8160, 8400, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("不还", 8400, 9190, "。", Fixed: true)
                            },
                            SentenceId: 2,
                            ChannelId: 0)),
                    Usage = new MultimodalTokenUsage
                    {
                            InputTokens = 120,
                            OutputTokens = 25,
                            TotalTokens = 145,
                            Duration = 9,
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            RecordedAsrSse = new(
                "recorded-speech-recognition",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "fun-asr-flash-2026-06-15",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.InputTextContent("背诵一首唐诗") }),
                            MultimodalMessage.Assistant(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.AssistantTextContent("好的，您想听哪一首？") }),
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.InputTextContent("请背诵《从军行》") }),
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.InputAudioContent("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        Format = "mp3",
                        LanguageHints = new List<string> { "zh" }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "7f296d1b-13e3-9afd-beed-aaf6036636ec",
                    Output = new MultimodalOutput(
                        null!,
                        Text: "青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。",
                        Sentence:
                        new SpeechRecognitionSentence(
                            280,
                            9190,
                            "青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。",
                            null,
                            true,
                            null,
                            null,
                            new[]
                            {
                                new SpeechRecognitionWord("青海", 280, 799, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("长", 799, 1079, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("云", 1079, 1358, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("暗", 1358, 1638, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("雪山", 1638, 2117, "，", Fixed: true),
                                new SpeechRecognitionWord("孤", 2357, 2637, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("城", 2637, 2877, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("遥", 2877, 3116, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("望", 3116, 3516, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("玉", 3516, 3836, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("门", 3836, 4075, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("关", 4075, 4355, "。", Fixed: true),
                                new SpeechRecognitionWord("黄", 4794, 5034, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("沙", 5034, 5274, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("百", 5274, 5514, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("战", 5514, 5873, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("穿", 5873, 6153, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("金", 6153, 6433, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("甲", 6433, 6752, "，", Fixed: true),
                                new SpeechRecognitionWord("不", 6992, 7232, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("破", 7232, 7471, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("楼", 7471, 7711, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("兰", 7711, 8151, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("终", 8151, 8390, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("不", 8390, 8670, string.Empty, Fixed: true),
                                new SpeechRecognitionWord("还", 8670, 9190, "。", Fixed: true)
                            },
                            SentenceId: 1,
                            ChannelId: 0)),
                    Usage = new MultimodalTokenUsage
                    {
                            Duration = 9,
                    }
                });
    }
}
