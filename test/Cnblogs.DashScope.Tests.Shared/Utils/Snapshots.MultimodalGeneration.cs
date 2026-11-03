using System.Text.Json;
using System.Text.Json.Nodes;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class MultimodalGeneration
    {
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlNoSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    })
                            }
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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩（日落时分）。") },
                                    string.Empty))
                        }),
                    RequestId = "b1a5c438-a5b3-9b08-b24b-da097a96d2c8",
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 2783,
                        ImageTokens = 2754,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 2754, TextTokens: 29),
                        OutputTokens = 8,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 8),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                        TotalTokens = 2791
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlChatClientNoSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                    })
                            }
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f,
                        Temperature = 1.1f,
                        RepetitionPenalty = 1.3f,
                        PresencePenalty = 1.2f,
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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩（日落时分）。") },
                                    string.Empty))
                        }),
                    RequestId = "b1a5c438-a5b3-9b08-b24b-da097a96d2c8",
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 2783,
                        ImageTokens = 2754,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 2754, TextTokens: 29),
                        OutputTokens = 8,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 8),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                        TotalTokens = 2791
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent("这个图片是哪里，请用简短的语言回答")
                                    })
                            }
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
                                            "海滩（日落时分），具体地点无法从图片中确定。")
                                    }))
                        }),
                    RequestId = "2a8b0003-26e7-967e-b6f5-a43942d85d89",
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 2532,
                    ImageTokens = 2503,
                    InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 29),
                    OutputTokens = 16,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 16),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                    TotalTokens = 2548
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlChatClientSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                    })
                            }
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
                                            "海滩（日落时分），具体地点无法从图片中确定。")
                                    }))
                        }),
                    RequestId = "2a8b0003-26e7-967e-b6f5-a43942d85d89",
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 2532,
                    ImageTokens = 2503,
                    InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 29),
                    OutputTokens = 16,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 16),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                    TotalTokens = 2548
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
                                    }),
                            }
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
                                    }))
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
            OcrAdvancedRecognitionNoSse = new(
                "multimodal-generation-vl-ocr-advanced-recognition",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.5-ocr",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.ImageContent(
                                        "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5727078571/p1008252.png"),
                                })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        OcrOptions = new MultimodalOcrOptions { Task = "advanced_recognition" }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "dd719101-1817-9665-9dfb-7fb67075f527",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        new(
                                            Text:
                                            "```json\n[\n\t{\"rotate_rect\": [500, 178, 101, 441, 90], \"text\": \"INTERNATIONAL\"},\n\t{\"rotate_rect\": [496, 318, 101, 539, 90], \"text\": \"MOTHER LANGUAGE\"},\n\t{\"rotate_rect\": [500, 462, 101, 113, 90], \"text\": \"DAY\"},\n\t{\"rotate_rect\": [916, 278, 101, 101, 90], \"text\": \"你好！\"},\n\t{\"rotate_rect\": [178, 680, 151, 101, 23], \"text\": \"Bonjour\"},\n\t{\"rotate_rect\": [70, 752, 123, 63, 23], \"text\": \"Merhaba\"},\n\t{\"rotate_rect\": [486, 776, 101, 139, 90], \"text\": \"Hello!\"},\n\t{\"rotate_rect\": [298, 800, 71, 73, 90], \"text\": \"Ciao\"},\n\t{\"rotate_rect\": [666, 800, 71, 51, 90], \"text\": \"Ola\"},\n\t{\"rotate_rect\": [768, 670, 63, 139, 65], \"text\": \"안녕하세요\"},\n\t{\"rotate_rect\": [916, 730, 63, 113, 65], \"text\": \"Salam!\"},\n\t{\"rotate_rect\": [102, 326, 123, 63, 23], \"text\": \"Привет\"}\n]\n```",
                                            OcrResult: new MultimodalOcrResult(
                                                new List<MultimodalOcrWordInfo>
                                                {
                                                new(
                                                    "INTERNATIONAL",
                                                    new[] { 3005, 534, 304, 2653, 90 },
                                                    new[] { 1678, 382, 4332, 382, 4332, 686, 1678, 686 }),
                                                new(
                                                    "MOTHER LANGUAGE",
                                                    new[] { 2981, 955, 304, 3243, 90 },
                                                    new[] { 1360, 803, 4602, 803, 4602, 1107, 1360, 1107 }),
                                                new(
                                                    "DAY",
                                                    new[] { 3005, 1388, 304, 680, 90 },
                                                    new[] { 2665, 1236, 3345, 1236, 3345, 1540, 2665, 1540 }),
                                                new(
                                                    "你好！",
                                                    new[] { 5508, 835, 304, 608, 90 },
                                                    new[] { 5204, 683, 5811, 683, 5811, 987, 5204, 987 }),
                                                new(
                                                    "Bonjour",
                                                    new[] { 1068, 2044, 1033, 321, 12 },
                                                    new[] { 770, 1817, 1606, 1994, 1366, 2271, 529, 2094 }),
                                                new(
                                                    "Merhaba",
                                                    new[] { 418, 2261, 800, 201, 12 },
                                                    new[] { 150, 2100, 830, 2247, 686, 2421, 6, 2274 }),
                                                new(
                                                    "Hello!",
                                                    new[] { 2921, 2333, 304, 836, 90 },
                                                    new[] { 2503, 2181, 3339, 2181, 3339, 2485, 2503, 2485 }),
                                                new(
                                                    "Ciao",
                                                    new[] { 1790, 2405, 214, 439, 90 },
                                                    new[] { 1570, 2298, 2009, 2298, 2009, 2512, 1570, 2512 }),
                                                new(
                                                    "Ola",
                                                    new[] { 4004, 2405, 214, 307, 90 },
                                                    new[] { 3850, 2298, 4157, 2298, 4157, 2512, 3850, 2512 }),
                                                new(
                                                    "안녕하세요",
                                                    new[] { 4617, 2014, 204, 898, 77 },
                                                    new[] { 4157, 2015, 4915, 1841, 5078, 2012, 4319, 2187 }),
                                                new(
                                                    "Salam!",
                                                    new[] { 5508, 2194, 204, 749, 77 },
                                                    new[] { 5120, 2181, 5733, 2036, 5896, 2208, 5282, 2352 }),
                                                new(
                                                    "Привет",
                                                    new[] { 611, 979, 800, 201, 12 },
                                                    new[] { 343, 818, 1023, 966, 878, 1140, 199, 993 }),
                                                },
                                                null)),
                                    }))
                        }),
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 17709,
                    ImageTokens = 17674,
                    InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 17674, TextTokens: 35),
                    OutputTokens = 447,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 447),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(),
                    TotalTokens = 18156
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrKeyInformationExtractionNoSse = new(
                "multimodal-generation-vl-ocr-key-information-extraction",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.5-ocr",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>
                                {
                                    MultimodalMessageContent.ImageContent(
                                        "https://duguang-labelling.oss-cn-shanghai.aliyuncs.com/demo_ocr/receipt_zh_demo.jpg"),
                                })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        OcrOptions = new MultimodalOcrOptions
                        {
                            Task = "key_information_extraction",
                            TaskConfig = new MultimodalOcrTaskConfig
                            {
                                ResultSchema = new Dictionary<string, object>
                                {
                                    { "乘车日期", "对应图中乘车日期时间，格式为年-月-日，比如2025-03-05" },
                                    {
                                        "发票",
                                        new Dictionary<string, string>
                                        {
                                            { "发票代码", "提取图中的发票代码，通常为一组数字或字母组合" },
                                            { "发票号码", "提取发票上的号码，通常由纯数字组成。" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                {
                    RequestId = "35758939-0c8f-912e-ba8c-5ac23e3373a4",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        new(
                                            Text:
                                            "```json\n{\n    \"乘车日期\": \"null\",\n    \"发票\": {\n        \"发票代码\": \"221021325353\",\n        \"发票号码\": \"10283819\"\n    }\n}\n```",
                                            OcrResult: new MultimodalOcrResult(
                                                null,
                                                JsonSerializer.Deserialize<JsonElement>(
                                                "{\"乘车日期\":\"null\",\"发票\":{\"发票代码\":\"221021325353\",\"发票号码\":\"10283819\"}}"))),
                                    }))
                        }),
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 510,
                    ImageTokens = 310,
                    InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 310, TextTokens: 200),
                    OutputTokens = 63,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 63),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(),
                    TotalTokens = 573
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrSse = new(
                "multimodal-generation-vl-ocr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.5-ocr",
                    Input = new MultimodalInput
                    {
                        Messages =
                            new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5727078571/p1006562.png",
                                            3136,
                                            1003520,
                                            true),
                                        MultimodalMessageContent.TextContent("Read all the text in the image.")
                                    }),
                            }
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
                    RequestId = "a33eac04-f831-95a1-95e3-1f975ba95110",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        new(
                                            Text: "产品介绍\n\n本品采用韩国进口纤维丝制造，不缩水、不变形、不发霉、\n不生菌、不伤物品表面。具有真正的不粘油、吸水力强、耐水\n浸、清洗干净、无毒、无残留、易晾干等特点。\n\n店家使用经验：不锈钢、陶瓷制品、浴盆、整体浴室大部分是\n白色的光洁表面，用其他的抹布擦洗表面污渍不易洗掉，太尖\n的容易划出划痕。使用这个仿真丝瓜布，沾少量中性洗涤剂揉\n出泡沫，很容易把这些表面污渍擦洗干净。\n\n6 941990 612023\n货号：2023",
                                            OcrResult: new MultimodalOcrResult(null, null))
                                    }))
                        }),
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 975,
                    ImageTokens = 947,
                    InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 947, TextTokens: 28),
                    OutputTokens = 143,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 143),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(),
                    TotalTokens = 1118
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            VideoNoSse = new(
                "multimodal-generation-vl-video",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                            }),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    }),
                            }
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
                    RequestId = "03983218-3f1f-948b-a903-266829737cc6",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这是一个从球门网后方拍摄的足球比赛关键瞬间，视角极具沉浸感和戏剧性。以下是该视频片段的具体过程描述（基于两帧画面推断出的连续动作）：\n\n**时间线与动作分解：**\n\n- **起始时刻（约0.2秒）**：  \n  镜头固定在球门内侧、紧贴球网的位置，透过白色球网网格望向球场。夜场灯光璀璨，体育场座无虚席，观众密集分布于看台，营造出紧张激烈的氛围。  \n  场上可见多名球员：一名身穿红色球衣的进攻球员（可能为前锋或边锋）正带球高速突入禁区左侧；一名穿蓝色球衣的防守球员试图拦截；守门员身着绿色长袖球衣与短裤，已提前移动至球门中路偏右位置，双膝微屈，身体前倾，双手张开，高度专注地紧盯来球方向——显然预判到射门即将发生。\n\n- **关键瞬间（约1秒）**：  \n  红衣球员完成射门动作（虽未直接拍到踢球脚部，但从其身体姿态及球的轨迹可判断），足球以高速飞向球门左下角（相对于守门员而言是右侧低处）。  \n  守门员迅速做出扑救反应：他向自己的右侧（即球门左侧）全力鱼跃伸展，右手向前猛探，试图封堵近角；整个身体腾空，双腿离地，展现出极强的爆发力与反应速度。  \n  足球此时已越过守门员指尖，正穿过球网——从第二帧可见，白球已进入网内并轻微触碰网眼，产生形变；同时，守门员落地时身体尚未完全恢复平衡，仍处于动态扑救后的惯性状态。  \n  背景中其他球员（如蓝衣后卫、另一名红衣队友）仍在奔跑或观望，部分观众举起手臂欢呼或惊呼，情绪达到高潮\n\n**技术细节补充：**\n- 拍摄采用广角镜头+浅景深，前景球网清晰锐利，背景人物略虚化，强化了“进球视角”的临场感。\n- 光影对比强烈：顶灯直射形成高光耀斑，草地反光明显，凸显夜间赛事的专业质感。\n- “CHAMPIONSHIP”字样出现在广告牌上，暗示这是一场重要锦标赛（如欧冠、联赛争冠战等）的关键对决。\n\n**结论：**  \n该视频捕捉的是一个典型的“近距离爆射破门”场景——进攻方精准施射，守门员奋力扑救但未能阻止皮球入网。整个过程仅数秒，却浓缩了足球运动的速度、力量、技巧与悬念，极具视觉冲击力与情感张力。")
                                    }, string.Empty))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 1208,
                        VideoTokens = 1190,
                        InputTokensDetails = new MultimodalInputTokenDetails(VideoTokens: 1190, TextTokens: 18),
                        OutputTokens = 614,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 614),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                        TotalTokens = 1822
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            VideoSse = new(
                "multimodal-generation-vl-video",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3-vl-plus",
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
                                            }),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    }),
                            }
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
                    RequestId = "202369c2-9bcd-9b9e-becf-a5b52cc5dc20",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "这是一个从球门网内视角拍摄的足球比赛关键瞬间，画面充满张力与临场感。以下是该视频片段的具体过程描述（基于两帧图像推断出的连续动作）：\n\n**场景设定：**\n- 时间：夜晚，体育场灯光全开，照亮整个球场。\n- 地点：大型专业足球场，看台坐满观众，背景可见“CHAMPIONSHIP”字样横幅（可能为某项锦标赛或联赛），氛围热烈。\n- 视角：固定于球门后方、紧贴球网内侧，网格清晰在前景形成虚化框架，增强沉浸感。\n\n**人物与装备：**\n- 守门员：身穿绿色长袖守门服、黑色手套和短裤，金发扎起；正全力向右侧扑救。\n- 进攻球员（射门者）：身着红色球衣（号码3号可见）、白色短裤，在左侧准备/完成射门动作。\n- 防守/协防球员：至少两名穿蓝色球衣的球员在中路及右路参与防守或补位。\n- 足球：白色传统皮球，位于球门前约1–2米处，正高速飞向球门右下角（从守门员视角）。\n\n**动作过程（按时间顺序还原）：**\n\n1. **起始帧（≈0.0秒）**：  \n   - 红衣3号球员刚完成射门动作（身体前倾、左腿支撑、右腿已踢出），足球离脚飞行中。\n   - 守门员尚未完全启动，仍处于站立预备姿态，目光紧盯来球方向。\n   - 蓝衣球员在禁区中央试图封堵但稍晚一步；一名蓝衣球员已在球门右侧准备回追。\n   - 球网轻微晃动，暗示即将受冲击。\n\n2. **后续帧（≈0.5秒后）**：  \n   - 守门员已全力向自己右侧（即球门右下角）鱼跃扑救——身体舒展，双臂前伸，右手接近触到球；\n   - 足球几乎同时抵达球门线附近，位置极低且贴近地面，轨迹指向球门右下死角；\n   - 由于守门员扑救幅度极大，其身体已腾空并部分越过球门线，左腿高抬，重心偏移；\n   - 前景球网因气流与冲击开始明显震颤变形；\n   - 看台上观众姿态各异：有人站起、有人举手惊呼，体现紧张情绪。\n\n**结果推测（虽未完整呈现入网）：**\n- 从守门员扑救时机与球的位置判断，此球极有可能已越过门线——若守门员指尖未能触及，则为进球；若指尖擦到但无力改变轨迹，亦大概率入网。\n- 整个过程仅持续不到1秒，是典型的“决定性瞬间”，突出速度、反应与竞技悬念\n\n**视听风格补充：**\n- 画面采用浅景深+广角镜头，强化纵深感与紧迫感；\n- 强光顶灯造成轻微眩光与镜头光晕，烘托夜间大赛氛围；\n- 若配声效，应有球鞋击球声、观众骤然爆发的呐喊、以及球网被撞击的“砰”声。\n\n综上，该视频捕捉了足球比赛中最具戏剧性的时刻之一：一次近距离爆射 + 守门员极限扑救 —— 是技术、勇气与运气交织的缩影。")
                                    }))
                        }),
                                        Usage = new MultimodalTokenUsage
                    {
                    InputTokens = 1208,
                    VideoTokens = 1190,
                    InputTokensDetails = new MultimodalInputTokenDetails(VideoTokens: 1190, TextTokens: 18),
                    OutputTokens = 770,
                    OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 770),
                    PromptTokensDetails = new MultimodalPromptTokenDetails(1152),
                    TotalTokens = 1978
                    }
                });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> OssVideoSse =
                new(
                    "multimodal-generation-vl-video-file",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen3-vl-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new[]
                            {
                                MultimodalMessage.User(
                                    new[]
                                    {
                                        MultimodalMessageContent.VideoContent(
                                            "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250328/eepdcq/phase_change_480p.mov",
                                            2),
                                        MultimodalMessageContent.TextContent("描述这个视频的具体过程")
                                    })
                            }
                        },
                        Parameters = new MultimodalParameters { IncrementalOutput = true }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "f64dcf3f-392e-962a-8b89-4044c9c34ef8",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                            "这个视频是一个科普类动画短片，通过视觉对比与微观模型，系统性地解释**温度对物质状态和粒子行为的影响**。以下是其具体过程的详细描述：\n\n---\n\n### **第一阶段：宏观现象对比（0–21秒）**\n视频以**四宫格分屏**形式呈现四种不同温度下的典型物理现象，突出温度差异带来的显著变化：\n\n1. **左上角：0°C —— 冰的融化**\n   - 画面：几块冰块置于木质桌面上，底部已有少量融水。\n   - 标注：“0°C”（蓝色字体）。\n   - 过程：随时间推移，冰块逐渐变小、变形，最终完全融化为一滩水——体现**固→液相变**（熔化），发生在水的熔点。\n\n2. **右上角：约1000°C —— 金属熔化/铸造**\n   - 画面：高温炉中，数个长方形模具内盛有发光的橙黄色熔融金属（如金或铜），表面流动、泛光。\n   - 标注：“About 1000°C”（黄色字体）。\n   - 暗示：高温使高熔点固体（金属）熔化为液体，体现**高温下固→液转变**。\n\n3. **左下角：100°C —— 水的沸腾**\n   - 画面：不锈钢锅置于电磁炉上，锅内水剧烈翻滚，大量气泡上升破裂。\n   - 标注：“100°C”（蓝色字体）。\n   - 体现：标准大气压下，水达到沸点，发生**液→气相变**（汽化）。\n\n4. **右下角：约−200°C —— 极低温现象（液氮/超冷环境）**\n   - 画面：一个金属容器中盛有白色雾气缭绕的液体（很可能是液氮，沸点−196°C），容器边缘结霜，持续冒出大量“白雾”（其实是空气中的水蒸气遇冷凝结成的小液滴）。\n   - 标注：“About −200°C”（绿色字体）。\n   - 体现：极低温下气体可液化，且周围空气冷凝形成可见雾。\n\n> ✅ **设计亮点**：  \n> - 黄色小圆点作为“引导光标”，依次在四个画面间移动（从左上→右上→左下→右下），引导观众注意力；  \n> - 四种温度跨度极大（−200°C → 1000°C），覆盖常见物态变化的关键温度点；  \n> - 所有画面均为真实拍摄（非动画），增强可信度。\n\n---\n\n### **第二阶段：微观粒子模型引入（22–33秒）**\n场景切换至纯黑背景，转入**抽象动画解释**：\n\n1. **22–27秒**：一块灰色不规则岩石出现在左侧，黄色小球（代表一个粒子/原子）在其表面滚动、弹跳，象征粒子在固体中的振动运动。\n   - 岩石代表**固体宏观形态**；\n   - 黄点代表单个粒子，运动幅度小，体现固体中粒子被束缚在晶格位置附近振动。\n\n2. **28–33秒**：\n   - 从岩石表面引出一条线，连接到右侧放大的圆形区域（显微视图）；\n   - 圆形区域内展示**紧密排列的灰色球体阵列**（代表固体中原子/分子的有序晶格结构）；\n   - 黄点进入该区域，在晶格间隙中轻微移动（仍受限），随后停在某个位置——强调**固体中粒子虽振动但位置相对固定**。\n\n---\n\n### **第三阶段：分子间作用力示意（34–38秒）**\n画面进一步放大，聚焦两个相邻的灰色粒子球：\n\n- 两球之间出现一个黄色小点（仍代表“探针”或能量单元）；\n- 两侧各出现一个**白色箭头**，分别指向彼此；\n- 屏幕中央浮现文字：“**Attraction**”（青绿色字体）；\n- 箭头动态闪烁，黄点在两球之间轻微振荡，模拟粒子因**分子间吸引力**而维持稳定间距。\n\n> ✅ 此段意在说明：  \n> 即使在固体中粒子看似静止，实则因热运动而振动；而它们不飞散，是因为存在**相互吸引力**（范德华力、金属键、离子键等，依物质而定）。\n\n---\n\n### **总结：视频逻辑脉络**\n1. **现象层**（宏观）→ 展示不同温度下物质状态变化（冰融、水沸、金属熔、液氮冷凝）；  \n2. **结构层**（微观）→ 用岩石与粒子模型揭示固体内部粒子排列与运动方式；  \n3. **原理层**（力与能量）→ 强调“吸引力”是维持固体结构的关键，为后续讲解热运动如何克服吸引力（导致熔化/汽化）埋下伏笔。\n\n该视频适合用于中学物理/化学教学，以直观、简洁、多模态的方式建立“温度 ↔ 粒子动能 ↔ 物质状态”的核心概念联系。")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokens = 9380,
                            VideoTokens = 9362,
                            InputTokensDetails = new MultimodalInputTokenDetails(VideoTokens: 9362, TextTokens: 18),
                            OutputTokens = 1159,
                            OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 1159),
                            PromptTokensDetails = new MultimodalPromptTokenDetails(0),
                            TotalTokens = 10539
                        }
                    });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> FunctionCallSse =
                new(
                    "multimodal-generation-message-with-parallel-function-call",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen3.6-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent(
                                            "根据杭州和上海现在的天气，以这张图片为参考，编一个 100 字以内的小故事")
                                    })
                            }
                        },
                        Parameters = new MultimodalParameters
                        {
                            IncrementalOutput = true,
                            EnableThinking = true,
                            Tools = new List<ToolDefinition>
                            {
                                new(
                                    "function",
                                    new FunctionDefinition(
                                        "get_current_weather",
                                        "获取现在的天气",
                                        GetCurrentWeatherParametersSchema))
                            },
                            ParallelToolCalls = true
                        }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "0d95e615-418d-97a1-9302-ff622a7ceead",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "tool_calls",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>(),
                                        "用户现在需要先获取杭州和上海现在的天气，才能基于天气编故事。所以第一步要调用get_current_weather工具，分别获取杭州和上海的天气。先处理杭州的，参数location设为“浙江省杭州市”。",
                                        new List<ToolCall>
                                        {
                                            new(
                                                "call_aa99ad078f294a3d81da41d7",
                                                "function",
                                                0,
                                                new FunctionCall(
                                                    "get_current_weather",
                                                    "{\"location\": \"浙江省杭州市\"}")),
                                            new(
                                                "call_aa8b6311567847e197e6ca7f",
                                                "function",
                                                1,
                                                new FunctionCall(
                                                    "get_current_weather",
                                                    "{\"location\": \"上海市\"}"))
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokensDetails =
                                new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 304),
                            PromptTokensDetails = new(),
                            OutputTokensDetails =
                                new MultimodalOutputTokenDetails(ReasoningTokens: 38, TextTokens: 65),
                            ImageTokens = 2503,
                            TotalTokens = 2910,
                            InputTokens = 2807,
                            OutputTokens = 103
                        }
                    });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> ToolUseSse =
                new(
                    "multimodal-generation-message-with-tools",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen3.6-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new List<MultimodalMessage>
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                        MultimodalMessageContent.TextContent(
                                            "根据杭州和上海现在的天气，以这张图片为参考，编一个 100 字以内的小故事")
                                    }),
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>(),
                                    null,
                                    new List<ToolCall>
                                    {
                                        new(
                                            "call_aa99ad078f294a3d81da41d7",
                                            "function",
                                            0,
                                            new FunctionCall(
                                                "get_current_weather",
                                                "{\"location\": \"浙江省杭州市\"}")),
                                        new(
                                            "call_aa8b6311567847e197e6ca7f",
                                            "function",
                                            1,
                                            new FunctionCall(
                                                "get_current_weather",
                                                "{\"location\": \"上海市\"}"))
                                    }),
                                MultimodalMessage.Tool(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("杭州-大部多云")
                                    },
                                    "call_aa99ad078f294a3d81da41d7"),
                                MultimodalMessage.Tool(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("上海-晴") },
                                    "call_aa8b6311567847e197e6ca7f")
                            }
                        },
                        Parameters = new MultimodalParameters
                        {
                            IncrementalOutput = true,
                            EnableThinking = true,
                            Tools = new List<ToolDefinition>
                            {
                                new(
                                    "function",
                                    new FunctionDefinition(
                                        "get_current_weather",
                                        "获取现在的天气",
                                        GetCurrentWeatherParametersSchema))
                            },
                            ParallelToolCalls = true
                        }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "87e6587a-71a3-98ba-89a3-23151746e292",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                                "告别杭州的多云，我们驱车奔赴上海的晴朗。金色的阳光洒满海滩，海风微暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”击掌。此刻远离喧嚣，只有我们和治愈的暖阳，这就是最完美的周末。")
                                        },
                                        "用户需要编写一个基于图片的小故事，参考杭州和上海的天气，并且字数限制在 100 字以内。\n\n1.  **整合信息**：\n    *   **图片内容**：海边、沙滩、夕阳（或清晨的阳光）、金毛犬、年轻女性、握手动作、轻松愉快。\n    *   **天气信息**：杭州是“大部多云”，上海是“晴”。\n    *   **关联逻辑**：图片中的场景明显是海边（沙滩），而上海离海比较近（虽然图片不一定是在上海拍的，但在地理认知上上海和海边关联性更强，且上海是晴天，与图片中阳光明媚的氛围更吻合）。杭州是多云，可以作为故事的背景对比，比如“逃离”多云的杭州去上海看海。\n\n2.  **构思情节**：\n    *   主角：图中的女孩和金毛。\n    *   起因：杭州天气多云，有些沉闷，或者只是想去海边玩。\n    *   经过：带着金毛去了上海（或者在上海的海边），因为上海是晴天。\n    *   高潮/结局：在上海的金色沙滩上，享受阳光，和金毛握手互动，非常开心。\n\n3.  **草拟文本 (Draft 1)**：\n    杭州的大部多云让人有些慵懒，我们便驱车前往上海，去追逐那里的晴朗。当双脚踩上上海温热的沙滩，金色的阳光正好洒下。金毛犬兴奋地举起爪子和我握手，海风轻拂，这惬意的午后，是属于我们最好的时光。\n\n4.  **修改与润色 (Draft 2 - 更加精炼，强调天气对比)**：\n    告别杭州的多云，我们一路向东，奔赴上海的晴朗。金色的阳光洒满上海的海滩，海风温暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”。此刻没有城市的喧嚣，只有我们和这治愈的阳光，这就是最完美的周末。\n\n5.  **字数检查**：\n    “告别杭州的多云，我们一路向东，奔赴上海的晴朗。金色的阳光洒满上海的海滩，海风温暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”。此刻没有城市的喧嚣，只有我们和这治愈的阳光，这就是最完美的周末。” -> 大约 85 字。\n\n6.  **最终定稿**：\n    告别杭州的多云，我们驱车奔赴上海的晴朗。金色的夕阳洒满海滩，海风微暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”击掌。此刻远离喧嚣，只有我们和治愈的阳光，这就是最完美的周末。"))
                            }),
                        Usage = new MultimodalTokenUsage
                        {
                            InputTokensDetails =
                                new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 388),
                            PromptTokensDetails = new(),
                            OutputTokensDetails =
                                new MultimodalOutputTokenDetails(ReasoningTokens: 556, TextTokens: 64),
                            ImageTokens = 2503,
                            TotalTokens = 3511,
                            InputTokens = 2891,
                            OutputTokens = 620
                        }
                    });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> WebSearchNoSse =
                new(
                    "multimodal-generation-with-websearch",
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
                    {
                        Model = "qwen3.6-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new[]
                            {
                                MultimodalMessage.User(
                                    new[] { MultimodalMessageContent.TextContent("介绍一下杭州西湖最近有什么好玩的？") })
                            }
                        },
                        Parameters = new MultimodalParameters
                        {
                            IncrementalOutput = false,
                            EnableSearch = true,
                            EnableTextImageMixed = true,
                            SearchOptions = new SearchOptions()
                            {
                                EnableSource = true,
                                AssignedSiteList = new List<string>() { "westlake.hangzhou.gov.cn" },
                                Freshness = 30,
                                IntentionOptions = new IntentionOptions { PromptIntervene = "建议偏向公园自然类景点" }
                            }
                        }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "c5595e94-d5c6-94a3-80c3-9affff93463e",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                                "结合当前杭州西湖风景名胜区管委会发布的最新动态（2026年4月），西湖最近有不少精彩纷呈的活动和景观，非常适合游玩。以下为您精选的几个亮点：\n\n### 🌸 1. 赏花雅事：西湖花朝节与牡丹科普\n正值春意盎然之际，西湖周边的花卉活动非常丰富。\n*   **西湖花朝节**：近期，西湖举办了盛大的花朝节活动，不仅有百花争艳的美景，**浙里办杭州城市频道**也亮相于此。游客可以在此感受传统的汉服文化与现代城市服务的结合，非常适合摄影打卡。\n*   **跟着大师赏牡丹**：如果您喜欢花卉，**六和塔文化公园**正在开展“跟着大师赏牡丹”科普活动。这里有专业的讲解带领，不仅能欣赏到雍容华贵的牡丹花海，还能深入了解牡丹的种植与文化内涵。\n\n\n\n<p align=\"center\">\n<img src=\"http://q3.itc.cn/q_70/images03/20240404/74d7ac08448d404cb364e9f0f2425776.jpeg\" alt=\"杭州西湖花朝节，穿着传统汉服的游客在百花丛中游玩，春日阳光明媚，背景有西湖山水，色彩艳丽，充满古典韵味。\" width=\"400\" height=\"auto\">\n</p>\n\n\n\n### 🌿 2. 自然生态：西湖荷区开启年度“清塘”\n虽然荷花盛开多在夏日，但西湖荷区的年度**“清塘”**工作也是近期的一大看点。清塘不仅是为了净化水质、改良底泥，更是为了来年荷花更好地生长。您可以前往荷区，观察园林工人们如何呵护这片水域，感受西湖生态维护的独特一面，体会人与自然和谐共处的细节。\n\n\n\n<p align=\"center\">\n<img src=\"http://p9.itc.cn/images01/20230906/e4e152b3e130463388333692bc3ac9e6.png\" alt=\"杭州西湖荷塘，工作人员正在进行清塘作业，挖掘清理湖底淤泥，水面平静，远处有断桥或保俶塔，展现西湖生态环境治理场景。\" width=\"400\" height=\"auto\">\n</p>\n\n\n\n### 🏛️ 3. 人文缅怀：纪念岳飞诞辰923周年系列活动\n西子湖畔驻忠魂。近期，西湖举办了**“纪念岳飞诞辰923周年系列活动”**。岳王庙是游客必去之地，通过参与这些纪念活动，您可以更深入地了解岳飞的生平事迹和爱国主义精神。系列活动通常包括祭祀仪式、文化讲座或相关的历史展览，为西湖之旅增添了一份厚重的历史底蕴。\n\n\n\n<p align=\"center\">\n<img src=\"http://gips2.baidu.com/it/u=697087769,2879158706&fm=3074&app=3074&f=JPEG\" alt=\"杭州西湖岳王庙，古朴庄严的建筑外观，红墙黑瓦，门前有纪念岳飞诞辰活动的横幅或装饰，氛围肃穆，具有历史文化感。\" width=\"400\" height=\"auto\">\n</p>\n\n\n\n### 🎨 4. 艺术熏陶：肖峰艺术馆开馆一周年暨宋韧作品展\n对于艺术爱好者来说，**肖峰艺术馆**开馆一周年是一个不错的消息。馆内正在举办**宋韧首次个人作品展**（杭城启幕）。您可以走进艺术馆，欣赏名家画作，感受当代艺术的魅力。在游览完自然风光后，来一场艺术之旅，会让您的西湖之行更加丰富多彩。\n\n\n\n<p align=\"center\">\n<img src=\"http://qcloud.dpfile.com/pc/1o-XfOFwGDFwG0GU1ispVYW0thlCeQx8zkzf1BmHwMHGJW5DK8vc6d6xMDLxsjpxY0q73sB2DyQcgmKUxZFQtw.jpg\" alt=\"杭州肖峰艺术馆内部展厅，墙上挂着精美的油画作品，灯光柔和，参观者在画前驻足欣赏，艺术氛围浓厚，空间设计现代典雅。\" width=\"400\" height=\"auto\">\n</p>\n\n\n\n### 📝 游玩小贴士\n*   **交通建议**：西湖景区人流较大，建议乘坐公交或地铁前往，避免自驾造成的拥堵。\n*   **行程安排**：建议上午前往六和塔赏牡丹，下午漫步苏堤或白堤感受花朝节气氛，傍晚可参观艺术馆或前往岳王庙。\n\n祝您在杭州西湖度过一个愉快的旅程！")
                                        },
                                        "用户希望了解杭州西湖最近有什么好玩的活动或景点。\n根据知识库中的信息（时间戳显示为2026年4月17日），最近的活动有：\n1. 西湖荷区开启年度“清塘”。\n2. 西湖花朝节，浙里办杭州城市频道亮相。\n3. 六和塔文化公园开展“跟着大师赏牡丹”科普活动。\n4. 西子湖畔驻忠魂|纪念岳飞诞辰923周年系列活动。\n5. 肖峰艺术馆开馆一周年，宋韧首次个人作品展。\n\n需要将这些信息整理成一篇介绍，并插入合适的图片。\n由于知识库的时间是2026年4月，回答时应基于这些信息进行组织。\n\n图片搜索策略：\n1. 西湖荷花/清塘场景：搜索“杭州西湖荷花 清塘”或“西湖初夏”相关图片。\n2. 西湖花朝节：搜索“杭州西湖 花朝节”相关图片。\n3. 牡丹观赏：搜索“杭州西湖 牡丹”或“六和塔 牡丹”相关图片。\n4. 岳飞纪念活动：搜索“杭州西湖 岳王庙”或“岳飞纪念”相关图片。\n5. 艺术馆展览：搜索“杭州肖峰艺术馆”或“艺术展览”相关图片。\n\n布局规划：\n- 引言：欢迎来到杭州西湖，基于当前时间（2026年4月），推荐近期热门活动和景点。\n- 第一部分：荷区清塘（西湖自然生态）。插入图片。\n- 第二部分：花朝节（赏花活动）。插入图片。\n- 第三部分：赏牡丹（六和塔科普活动）。插入图片。\n- 第四部分：岳飞诞辰纪念活动（人文历史）。插入图片。\n- 第五部分：肖峰艺术馆周年展（文化艺术）。插入图片。\n- 结语：祝游玩愉快。\n\n注意：用户的问题是“最近有什么好玩的”，需要结合知识库的“最近”信息。\n图片提示词需要详细且为中文。\n\n图片1：西湖荷塘景色，正在进行清塘作业，或者美丽的荷塘风光。\n图片2：西湖花朝节，穿着汉服的人们在赏花，百花盛开的场景。\n图片3：牡丹花盛开的特写，背景可能有古建筑（六和塔）。\n图片4：岳王庙外景，或者纪念岳飞的活动现场，庄严肃穆。\n图片5：肖峰艺术馆内部，正在进行画展，艺术氛围浓厚。\n\n执行工具调用并生成回复。\n时间设定：2026年4月。\n活动列表：\n1. 西湖荷区年度“清塘”。\n2. 西湖花朝节（浙里办杭州城市频道亮相）。\n3. 六和塔文化公园“跟着大师赏牡丹”科普活动。\n4. 纪念岳飞诞辰923周年系列活动。\n5. 肖峰艺术馆开馆一周年及宋韧个人作品展。\n\n构建回复内容。"))
                            },
                            new DashScopeWebSearchInfo(
                                new List<DashScopeWebSearchResult>()
                                {
                                    new(
                                        "杭州西湖风景名胜区管委会",
                                        "https://img.alicdn.com/imgextra/i1/O1CN01uVLQQ51guZF6NjXhP_!!6000000004202-55-tps-32-32.svg",
                                        1,
                                        "杭州西湖风景名胜区管委会",
                                        "https://westlake.hangzhou.gov.cn/"),
                                    new(
                                        "杭州西湖风景名胜区管委会",
                                        "https://img.alicdn.com/imgextra/i1/O1CN01uVLQQ51guZF6NjXhP_!!6000000004202-55-tps-32-32.svg",
                                        2,
                                        "杭州西湖博物馆总馆(西湖学研究院、杭州南宋官窑博物馆、唐云艺术馆)2026年单位预算",
                                        "https://westlake.hangzhou.gov.cn/col/col1229288584/art/2026/art_9b6b1ef9ad844050823a8f3aef900001.html"),
                                    new(
                                        "杭州西湖风景名胜区管委会",
                                        "https://img.alicdn.com/imgextra/i1/O1CN01uVLQQ51guZF6NjXhP_!!6000000004202-55-tps-32-32.svg",
                                        3,
                                        "杭州西湖风景名胜区岳庙管理处(连横纪念馆)2026年度单位预算",
                                        "https://westlake.hangzhou.gov.cn/col/col1229288584/art/2026/art_e8fcbf7cb6eb40df9b2f9512f0070181.html"),
                                },
                                new List<DashScopeWebSearchExtra>())),
                        Usage = new MultimodalTokenUsage
                        {
                            TotalTokens = 1998,
                            OutputTokens = 1379,
                            InputTokens = 619,
                            InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 619),
                            OutputTokensDetails = new MultimodalOutputTokenDetails(
                                TextTokens: 1379,
                                ReasoningTokens: 636),
                            Plugins = new DashScopePluginUsages(
                                new DashScopeSearchPluginUsage(1, "turbo"),
                                new DashScopeImageSearchPluginUsage(4))
                        }
                    });

        public static readonly
            RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> ImageGenerationNoSse = new(
                "multimodal-generation-image",
                new ModelRequest<MultimodalInput, IMultimodalParameters>()
                {
                    Model = "qwen-image-3.0",
                    Input = new MultimodalInput()
                    {
                        Messages = new List<MultimodalMessage>()
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent>()
                                {
                                    MultimodalMessageContent.TextContent(
                                        "冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。")
                                })
                        }
                    },
                    Parameters = new MultimodalParameters()
                    {
                        NegativePrompt = "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。",
                        N = 1,
                        PromptExtend = true,
                        Watermark = false,
                        Seed = 233,
                        Size = "1664*928"
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                {
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>()
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>()
                                    {
                                        MultimodalMessageContent.ImageContent(
                                            "https://dashscope-a717.oss-accelerate.aliyuncs.com/1d/57/20261102/010b2f0f/422e725f-a2dc-93d3-bd19-8e6f536cab2a_qwen_image3_serving_output_0.png?Expires=1793717970&OSSAccessKeyId=LTAI5tPxpiCM2hjmWrFXrym1&Signature=MAwfZ0oxUIMffK3u7Ni2alG7FGQ%3D")
                                    }))
                        }),
                    RequestId = "422e725f-a2dc-93d3-bd19-8e6f536cab2a",
                    Usage = new MultimodalTokenUsage()
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> JsonSchemaNoSse =
            new(
                "multimodal-generation-json-schema",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("请问 1+1 是多少？") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.8f,
                        Temperature = 0.85f,
                        RepetitionPenalty = 1.1f,
                        IncrementalOutput = false,
                        ResponseFormat = DashScopeResponseFormat.JsonSchema(
                            new DashScopeJsonSchema(
                                "calculation",
                                new JsonObject
                                {
                                    ["type"] = "object",
                                    ["properties"] = new JsonObject
                                    {
                                        ["result"] = new JsonObject { ["type"] = "number" }
                                    },
                                    ["required"] = new JsonArray("result"),
                                    ["additionalProperties"] = false
                                },
                                Strict: true))
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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("{ \"result\": 2 }") },
                                    "用户问的是 1+1 等于多少。这是一个简单的数学问题，答案是 2。我直接回答即可。"))
                        }),
                    RequestId = "dbadff32-1819-9aa7-87dc-2c5cef315dcc",
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 71,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 71),
                        OutputTokens = 35,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 25, TextTokens: 35),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 0),
                        TotalTokens = 106
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> JsonSchemaSse =
            new(
                "multimodal-generation-json-schema",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen3.8-flash",
                    Input = new MultimodalInput
                    {
                        Messages = new List<MultimodalMessage>
                        {
                            MultimodalMessage.User(
                                new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("请问 1+1 是多少？") })
                        }
                    },
                    Parameters = new MultimodalParameters
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.8f,
                        Temperature = 0.85f,
                        RepetitionPenalty = 1.1f,
                        IncrementalOutput = true,
                        ResponseFormat = DashScopeResponseFormat.JsonSchema(
                            new DashScopeJsonSchema(
                                "calculation",
                                new JsonObject
                                {
                                    ["type"] = "object",
                                    ["properties"] = new JsonObject
                                    {
                                        ["result"] = new JsonObject { ["type"] = "number" }
                                    },
                                    ["required"] = new JsonArray("result"),
                                    ["additionalProperties"] = false
                                },
                                Strict: true))
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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("{\n  \"result\": 2\n}") },
                                    "用户问的是\"1+1是多少\"，这是一个非常基础的数学问题。答案是2。用中文回复即可。"))
                        }),
                    RequestId = "143d5633-4878-9b34-baf2-a10e86e8bb6c",
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 71,
                        InputTokensDetails = new MultimodalInputTokenDetails(TextTokens: 71),
                        OutputTokens = 37,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(ReasoningTokens: 24, TextTokens: 37),
                        PromptTokensDetails = new MultimodalPromptTokenDetails(CachedTokens: 0),
                        TotalTokens = 108
                    }
                });
    }
}
