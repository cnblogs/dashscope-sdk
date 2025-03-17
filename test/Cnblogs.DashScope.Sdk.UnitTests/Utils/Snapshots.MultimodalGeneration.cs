using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static partial class Snapshots
{
    public static class MultimodalGeneration
    {
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlNoSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.System(
                                [MultimodalMessageContent.TextContent("You are a helpful assistant.")]),
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                            ])
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f,
                        Temperature = 1.1f,
                        VlHighResolutionImages = true,
                        RepetitionPenalty = 1.3f,
                        PresencePenalty = 1.2f,
                        MaxTokens = 120,
                        Stop = "你好"
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent("海滩。")
                            ]))
                    ]),
                    RequestId = "e81aa922-be6c-9f9d-bd4f-0f43e21fd913",
                    Usage = new MultimodalTokenUsage
                    {
                        OutputTokens = 3,
                        InputTokens = 3613,
                        ImageTokens = 3577
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlChatClientNoSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="),
                                MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                            ])
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f,
                        Temperature = 1.1f,
                        RepetitionPenalty = 1.3f,
                        PresencePenalty = 1.2f,
                        MaxTokens = 120,
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent("海滩。")
                            ]))
                    ]),
                    RequestId = "e81aa922-be6c-9f9d-bd4f-0f43e21fd913",
                    Usage = new MultimodalTokenUsage
                    {
                        OutputTokens = 3,
                        InputTokens = 3613,
                        ImageTokens = 3577
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.System(
                                [MultimodalMessageContent.TextContent("You are a helpful assistant.")]),
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="),
                                MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                            ])
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        IncrementalOutput = true,
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                            ]))
                    ]),
                    RequestId = "13c5644d-339c-928a-a09a-e0414bfaa95c",
                    Usage = new MultimodalTokenUsage
                    {
                        OutputTokens = 85,
                        InputTokens = 1283,
                        ImageTokens = 1247
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlChatClientSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                            ])
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        IncrementalOutput = true,
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f,
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                            ]))
                    ]),
                    RequestId = "13c5644d-339c-928a-a09a-e0414bfaa95c",
                    Usage = new MultimodalTokenUsage
                    {
                        OutputTokens = 85,
                        InputTokens = 1283,
                        ImageTokens = 1247
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrNoSse = new(
                "multimodal-generation-vl-ocr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-ocr",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                                    3136,
                                    1003520),
                                MultimodalMessageContent.TextContent("Read all the text in the image.")
                            ]),
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f,
                        MaxTokens = 2000,
                        TopP = 0.01f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "195c98cd-4ee5-998b-b662-132b7aebc048",
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "读者对象 如果你是Linux环境下的系统管理员，那么学会编写shell脚本将让你受益匪浅。本书并未细述安装 Linux系统的每个步骤，但只要系统已安装好Linux并能运行起来，你就可以开始考虑如何让一些日常 的系统管理任务实现自动化。这时shell脚本编程就能发挥作用了，这也正是本书的作用所在。本书将 演示如何使用shell脚本来自动处理系统管理任务，包括从监测系统统计数据和数据文件到为你的老板 生成报表。 如果你是家用Linux爱好者，同样能从本书中获益。现今，用户很容易在诸多部件堆积而成的图形环境 中迷失。大多数桌面Linux发行版都尽量向一般用户隐藏系统的内部细节。但有时你确实需要知道内部 发生了什么。本书将告诉你如何启动Linux命令行以及接下来要做什么。通常，如果是执行一些简单任 务(比如文件管理) ， 在命令行下操作要比在华丽的图形界面下方便得多。在命令行下有大量的命令 可供使用，本书将会展示如何使用它们。")
                            ]))
                    ]),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 1248,
                        OutputTokens = 225,
                        ImageTokens = 1219
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrSse = new(
                "multimodal-generation-vl-ocr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-ocr",
                    Input = new MultimodalInput
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.ImageContent(
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                                    3136,
                                    1003520),
                                MultimodalMessageContent.TextContent("Read all the text in the image.")
                            ]),
                        ]
                    },
                    Parameters = new MultimodalParameters
                    {
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f,
                        MaxTokens = 2000,
                        TopP = 0.01f,
                        IncrementalOutput = true
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "fb33a990-3826-9386-8b0a-8317dfc38c1c",
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "读者对象 如果你是Linux环境下的系统管理员，那么学会编写shell脚本将让你受益匪浅。本书并未细述安装 Linux系统的每个步骤，但只要系统已安装好Linux并能运行起来，你就可以开始考虑如何让一些日常 的系统管理任务实现自动化。这时shell脚本编程就能发挥作用了，这也正是本书的作用所在。本书将 演示如何使用shell脚本来自动处理系统管理任务，包括从监测系统统计数据和数据文件到为你的老板 生成报表。 如果你是家用Linux爱好者，同样能从本书中获益。现今，用户很容易在诸多部件堆积而成的图形环境 中迷失。大多数桌面Linux发行版都尽量向一般用户隐藏系统的内部细节。但有时你确实需要知道内部 发生了什么。本书将告诉你如何启动Linux命令行以及接下来要做什么。通常，如果是执行一些简单任 务(比如文件管理) ， 在命令行下操作要比在华丽的图形界面下方便得多。在命令行下有大量的命令 可供使用，本书将会展示如何使用它们。")
                            ]))
                    ]),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 1248,
                        OutputTokens = 225,
                        ImageTokens = 1219
                    }
                });

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
                        [
                            MultimodalMessage.System(
                                [MultimodalMessageContent.TextContent("You are a helpful assistant.")]),
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.AudioContent(
                                    "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                MultimodalMessageContent.TextContent("这段音频在说什么，请用简短的语言回答")
                            ])
                        ]
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
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "这段音频在说中文，内容是\"没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网\"。")
                            ]))
                    ]),
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
                        [
                            MultimodalMessage.System(
                                [MultimodalMessageContent.TextContent("You are a helpful assistant.")]),
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.AudioContent(
                                    "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                MultimodalMessageContent.TextContent("这段音频的第一句话说了什么？")
                            ])
                        ]
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
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent("第一句话说了没有我互联网。")
                            ]))
                    ]),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 783,
                        OutputTokens = 7,
                        AudioTokens = 752
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            VideoNoSse = new(
                "multimodal-generation-vl-video",
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Model = "qwen-vl-max",
                    Input = new MultimodalInput()
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.VideoContent(
                                [
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
                                ]),
                                MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                            ]),
                        ]
                    },
                    Parameters = new MultimodalParameters()
                    {
                        Seed = 1234,
                        TopP = 0.01f,
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                {
                    RequestId = "d538f8cc-8048-9ca8-9e8a-d2a49985b479",
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "这段视频展示了一场足球比赛的精彩瞬间。具体过程如下：\n\n1. **背景**：画面中是一个大型体育场，观众席上坐满了观众，气氛热烈。\n2. **球员位置**：球场上有两队球员，一队穿着红色球衣，另一队穿着蓝色球衣。守门员穿着绿色球衣，站在球门前准备防守。\n3. **射门动作**：一名身穿红色球衣的球员在禁区内接到队友传球后，迅速起脚射门。\n4. **守门员扑救**：守门员看到对方射门后，立即做出反应，向左侧跃出试图扑救。\n5. **进球瞬间**：尽管守门员尽力扑救，但皮球还是从他的右侧飞入了球网。\n\n整个过程充满了紧张和刺激，展示了足球比赛中的精彩时刻。")
                            ]))
                    ]),
                    Usage = new MultimodalTokenUsage()
                    {
                        VideoTokens = 1440,
                        InputTokens = 1466,
                        OutputTokens = 180
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            VideoSse = new(
                "multimodal-generation-vl-video",
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Model = "qwen-vl-max",
                    Input = new MultimodalInput()
                    {
                        Messages =
                        [
                            MultimodalMessage.User(
                            [
                                MultimodalMessageContent.VideoContent(
                                [
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                                    "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
                                ]),
                                MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                            ]),
                        ]
                    },
                    Parameters = new MultimodalParameters()
                    {
                        Seed = 1234,
                        TopP = 0.01f,
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f,
                        IncrementalOutput = true
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                {
                    RequestId = "851745a1-22ba-90e2-ace2-c04e7445ec6f",
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            MultimodalMessage.Assistant(
                            [
                                MultimodalMessageContent.TextContent(
                                    "这段视频展示了一场足球比赛的精彩瞬间。具体过程如下：\n\n1. **背景**：画面中是一个大型体育场，观众席上坐满了观众，气氛热烈。\n2. **球员位置**：场上有两队球员，一队穿着红色球衣，另一队穿着蓝色球衣。守门员穿着绿色球衣，站在球门前准备防守。\n3. **射门动作**：一名身穿红色球衣的球员在禁区内接到队友传球后，迅速起脚射门。\n4. **扑救尝试**：守门员看到射门后立即做出反应，向左侧跃出试图扑救。\n5. **进球瞬间**：尽管守门员尽力扑救，但皮球还是从他的右侧飞入了球网。\n\n整个过程充满了紧张和刺激，展示了足球比赛中的精彩时刻。")
                            ]))
                    ]),
                    Usage = new MultimodalTokenUsage()
                    {
                        VideoTokens = 1440,
                        InputTokens = 1466,
                        OutputTokens = 176
                    }
                });
    }
}
