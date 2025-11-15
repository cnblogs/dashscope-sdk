using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.System(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("You are a helpful assistant.")
                                    }.AsReadOnly()),
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩。") }
                                        .AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩。") }
                                        .AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.System(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("You are a helpful assistant.")
                                    }.AsReadOnly()),
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                                    }.AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                                    }.AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                                            3136,
                                            1003520),
                                        MultimodalMessageContent.TextContent("Read all the text in the image.")
                                    }.AsReadOnly()),
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "读者对象 如果你是Linux环境下的系统管理员，那么学会编写shell脚本将让你受益匪浅。本书并未细述安装 Linux系统的每个步骤，但只要系统已安装好Linux并能运行起来，你就可以开始考虑如何让一些日常 的系统管理任务实现自动化。这时shell脚本编程就能发挥作用了，这也正是本书的作用所在。本书将 演示如何使用shell脚本来自动处理系统管理任务，包括从监测系统统计数据和数据文件到为你的老板 生成报表。 如果你是家用Linux爱好者，同样能从本书中获益。现今，用户很容易在诸多部件堆积而成的图形环境 中迷失。大多数桌面Linux发行版都尽量向一般用户隐藏系统的内部细节。但有时你确实需要知道内部 发生了什么。本书将告诉你如何启动Linux命令行以及接下来要做什么。通常，如果是执行一些简单任 务(比如文件管理) ， 在命令行下操作要比在华丽的图形界面下方便得多。在命令行下有大量的命令 可供使用，本书将会展示如何使用它们。")
                                    }.AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/ctdzex/biaozhun.jpg",
                                            3136,
                                            1003520),
                                        MultimodalMessageContent.TextContent("Read all the text in the image.")
                                    }.AsReadOnly()),
                            }.AsReadOnly()
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
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "读者对象 如果你是Linux环境下的系统管理员，那么学会编写shell脚本将让你受益匪浅。本书并未细述安装 Linux系统的每个步骤，但只要系统已安装好Linux并能运行起来，你就可以开始考虑如何让一些日常 的系统管理任务实现自动化。这时shell脚本编程就能发挥作用了，这也正是本书的作用所在。本书将 演示如何使用shell脚本来自动处理系统管理任务，包括从监测系统统计数据和数据文件到为你的老板 生成报表。 如果你是家用Linux爱好者，同样能从本书中获益。现今，用户很容易在诸多部件堆积而成的图形环境 中迷失。大多数桌面Linux发行版都尽量向一般用户隐藏系统的内部细节。但有时你确实需要知道内部 发生了什么。本书将告诉你如何启动Linux命令行以及接下来要做什么。通常，如果是执行一些简单任 务(比如文件管理) ， 在命令行下操作要比在华丽的图形界面下方便得多。在命令行下有大量的命令 可供使用，本书将会展示如何使用它们。")
                                    }.AsReadOnly()))
                        }),
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
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.System(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("You are a helpful assistant.")
                                    }.AsReadOnly()),
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.AudioContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                        MultimodalMessageContent.TextContent("这段音频在说什么，请用简短的语言回答")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                                    }.AsReadOnly()))
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
                                    }.AsReadOnly()),
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.AudioContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                        MultimodalMessageContent.TextContent("这段音频的第一句话说了什么？")
                                    }.AsReadOnly())
                            }.AsReadOnly()
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
                                    }.AsReadOnly()))
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
            VideoNoSse = new(
                "multimodal-generation-vl-video",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-max",
                    Input = new MultimodalInput
                    {
                        Messages =
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.VideoFrames(
                                            new List<string>
                                            {
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
                                            }.AsReadOnly()),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    }.AsReadOnly()),
                            }.AsReadOnly()
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopP = 0.01f,
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "d538f8cc-8048-9ca8-9e8a-d2a49985b479",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这段视频展示了一场足球比赛的精彩瞬间。具体过程如下：\n\n1. **背景**：画面中是一个大型体育场，观众席上坐满了观众，气氛热烈。\n2. **球员位置**：球场上有两队球员，一队穿着红色球衣，另一队穿着蓝色球衣。守门员穿着绿色球衣，站在球门前准备防守。\n3. **射门动作**：一名身穿红色球衣的球员在禁区内接到队友传球后，迅速起脚射门。\n4. **守门员扑救**：守门员看到对方射门后，立即做出反应，向左侧跃出试图扑救。\n5. **进球瞬间**：尽管守门员尽力扑救，但皮球还是从他的右侧飞入了球网。\n\n整个过程充满了紧张和刺激，展示了足球比赛中的精彩时刻。")
                                    }.AsReadOnly()))
                        }),
                    Usage = new MultimodalTokenUsage
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
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-max",
                    Input = new MultimodalInput
                    {
                        Messages =
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.VideoFrames(
                                            new List<string>
                                            {
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                                                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
                                            }.AsReadOnly()),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    }.AsReadOnly()),
                            }.AsReadOnly()
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopP = 0.01f,
                        Temperature = 0.1f,
                        RepetitionPenalty = 1.05f,
                        IncrementalOutput = true
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "851745a1-22ba-90e2-ace2-c04e7445ec6f",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这段视频展示了一场足球比赛的精彩瞬间。具体过程如下：\n\n1. **背景**：画面中是一个大型体育场，观众席上坐满了观众，气氛热烈。\n2. **球员位置**：场上有两队球员，一队穿着红色球衣，另一队穿着蓝色球衣。守门员穿着绿色球衣，站在球门前准备防守。\n3. **射门动作**：一名身穿红色球衣的球员在禁区内接到队友传球后，迅速起脚射门。\n4. **扑救尝试**：守门员看到射门后立即做出反应，向左侧跃出试图扑救。\n5. **进球瞬间**：尽管守门员尽力扑救，但皮球还是从他的右侧飞入了球网。\n\n整个过程充满了紧张和刺激，展示了足球比赛中的精彩时刻。")
                                    }.AsReadOnly()))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        VideoTokens = 1440,
                        InputTokens = 1466,
                        OutputTokens = 176
                    }
                });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OssVideoSse =
                new(
                    "multimodal-generation-vl-video-file",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>()
                    {
                        Model = "qwen-vl-max",
                        Input = new MultimodalInput
                        {
                            Messages = new[]
                            {
                                MultimodalMessage.User(
                                    new[]
                                    {
                                        MultimodalMessageContent.VideoContent(
                                            "oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-15/0b590ae2-3904-4919-a886-d0a2e6ede1b8/sample.mp4",
                                            2),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    })
                            }
                        },
                        Parameters = new MultimodalParameters() { IncrementalOutput = true }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                    {
                        RequestId = "80199c3f-e431-4310-b1de-f2ba11e554f7",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>()
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>()
                                        {
                                            MultimodalMessageContent.TextContent("这个视频展示了一位年轻女性的面部特写，她有着齐肩的棕色短发和刘海，穿着一件粉色针织开衫搭配白色内搭，脖子上戴着一条细小的项链。背景是模糊的城市建筑环境，阳光明媚，整体画面明亮且温暖。\n\n视频的具体过程如下：\n\n1. **初始画面**：视频开始时，女性面带微笑，眼神温和地注视着镜头，表情自然亲切。\n2. **表情变化**：她的笑容逐渐加深，从浅笑过渡到大笑，露出牙齿，显得非常开心和愉悦。\n3. **头部轻微晃动**：在笑的过程中，她的头部有轻微的前后晃动，增加了动态感和真实感。\n4. **眼神交流**：她一直保持与镜头的眼神接触，给人一种直接对话的感觉。\n5. **情绪传递**：整个过程中，她的表情从轻松愉快逐渐变为更加开朗和兴奋，传递出积极正面的情绪。\n6. **结束状态**：视频最后，她的笑容依然灿烂，但稍微收敛了一些，恢复到一个温和的笑容，结束于一个稳定的画面。\n\n整个视频通过细腻的表情变化和自然的动作，展现了人物的亲和力和愉悦心情，营造出一种温暖、阳光的氛围。右上角有“通义·AI合成”的水印，表明这是由AI技术生成的视频内容。")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage()
                        {
                            VideoTokens = 3586,
                            TotalTokens = 3887,
                            OutputTokens = 283,
                            InputTokens = 3604,
                            InputTokensDetails = new MultimodalInputTokenDetails(VideoTokens: 3586, TextTokens: 18),
                            OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 283),
                            PromptTokensDetails = new MultimodalPromptTokenDetails(0)
                        }
                    });
    }
}
