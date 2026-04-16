using System.Text.Json;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.Schema;
using Json.Schema.Generation;

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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩。") }))
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
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("海滩。") }))
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
                                            "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                                    }))
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
                                            "这是一个海滩，有沙滩和海浪。在前景中坐着一个女人与她的宠物狗互动。背景中有海水、阳光及远处的海岸线。由于没有具体标识物或地标信息，我无法提供更精确的位置描述。这可能是一个公共海滩或是私人区域。重要的是要注意不要泄露任何个人隐私，并遵守当地的规定和法律法规。欣赏自然美景的同时请尊重环境和其他访客。")
                                    }))
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
                    Model = "qwen-vl-ocr-latest",
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
                    RequestId = "e72413ae-c147-4904-a63b-c09e87ea133a",
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
                                            "```json\n[\n\t{\"rotate_rect\": [502, 178, 95, 435, 90], \"text\": \"INTERNATIONAL\"},\n\t{\"rotate_rect\": [500, 316, 95, 543, 90], \"text\": \"MOTHER LANGUAGE\"},\n\t{\"rotate_rect\": [502, 462, 95, 109, 90], \"text\": \"DAY\"},\n\t{\"rotate_rect\": [916, 278, 83, 119, 59], \"text\": \"你好!\"},\n\t{\"rotate_rect\": [80, 320, 139, 60, 29], \"text\": \"Привіт!\"},\n\t{\"rotate_rect\": [196, 680, 139, 60, 29], \"text\": \"Bonjour!\"},\n\t{\"rotate_rect\": [68, 760, 139, 51, 29], \"text\": \"Merhaba!\"},\n\t{\"rotate_rect\": [308, 802, 55, 71, 90], \"text\": \"Ciao!\"},\n\t{\"rotate_rect\": [486, 774, 83, 133, 90], \"text\": \"Hello!\"},\n\t{\"rotate_rect\": [672, 824, 47, 49, 90], \"text\": \"Ola!\"},\n\t{\"rotate_rect\": [774, 680, 51, 139, 53], \"text\": \"שלום רב!\"},\n\t{\"rotate_rect\": [918, 726, 51, 113, 62], \"text\": \"Salam!\"}\n]\n```",
                                            OcrResult: new MultimodalOcrResult(
                                                new List<MultimodalOcrWordInfo>
                                                {
                                                    new(
                                                        "INTERNATIONAL",
                                                        new[] { 3009, 532, 285, 2610, 90 },
                                                        new[] { 1704, 390, 4314, 390, 4314, 675, 1704, 675 }),
                                                    new(
                                                        "MOTHER LANGUAGE",
                                                        new[] { 2997, 946, 285, 3258, 90 },
                                                        new[] { 1368, 804, 4626, 804, 4626, 1089, 1368, 1089 }),
                                                    new(
                                                        "DAY",
                                                        new[] { 3009, 1384, 285, 654, 90 },
                                                        new[] { 2682, 1242, 3336, 1242, 3336, 1527, 2682, 1527 }),
                                                    new(
                                                        "你好!",
                                                        new[] { 5493, 832, 279, 825, 73 },
                                                        new[] { 5058, 819, 5670, 633, 5928, 846, 5316, 1032 }),
                                                    new(
                                                        "Привіт!",
                                                        new[] { 477, 959, 886, 197, 15 },
                                                        new[] { 24, 936, 198, 780, 930, 981, 756, 1137 }),
                                                    new(
                                                        "Bonjour!",
                                                        new[] { 1173, 2038, 886, 197, 15 },
                                                        new[] { 720, 2016, 894, 1860, 1626, 2061, 1452, 2217 }),
                                                    new(
                                                        "Merhaba!",
                                                        new[] { 408, 2279, 863, 167, 15 },
                                                        new[] { -30, 2244, 114, 2112, 846, 2313, 696, 2445 }),
                                                    new(
                                                        "Ciao!",
                                                        new[] { 1845, 2404, 165, 426, 90 },
                                                        new[] { 1632, 2322, 2058, 2322, 2058, 2487, 1632, 2487 }),
                                                    new(
                                                        "Hello!",
                                                        new[] { 2913, 2320, 249, 798, 90 },
                                                        new[] { 2514, 2196, 3312, 2196, 3312, 2445, 2514, 2445 }),
                                                    new(
                                                        "Ola!",
                                                        new[] { 4029, 2470, 141, 294, 90 },
                                                        new[] { 3882, 2400, 4176, 2400, 4176, 2541, 3882, 2541 }),
                                                    new(
                                                        "שלום רב!",
                                                        new[] { 4641, 2039, 179, 837, 69 },
                                                        new[] { 4218, 2103, 4884, 1851, 5064, 1974, 4398, 2226 }),
                                                    new(
                                                        "Salam!",
                                                        new[] { 5505, 2176, 168, 719, 75 },
                                                        new[] { 5136, 2190, 5730, 2028, 5874, 2163, 5280, 2325 })
                                                },
                                                null)),
                                    }))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        ImageTokens = 1862,
                        InputTokens = 1895,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 1862, TextTokens: 33),
                        OutputTokens = 432,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 432),
                        TotalTokens = 2327
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrKeyInformationExtractionNoSse = new(
                "multimodal-generation-vl-ocr-key-information-extraction",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-ocr-latest",
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
                    RequestId = "5f79aafc-8749-4ea2-b122-7d8541d58b6c",
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
                                            "```json\n{\n \"乘车日期\": \"2013-06-29\",\n \"发票\": {\n \"发票代码\": \"221021325353\",\n \"发票号码\": \"10283819\"\n }\n}\n```",
                                            OcrResult: new MultimodalOcrResult(
                                                null,
                                                JsonSerializer.Deserialize<JsonElement>(
                                                    "{\"乘车日期\":\"2013-06-29\",\n\"发票\":{\"发票代码\": \"221021325353\",\"发票号码\": \"10283819\"}}"))),
                                    }))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        ImageTokens = 310,
                        InputTokens = 524,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 310, TextTokens: 214),
                        OutputTokens = 65,
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 65),
                        TotalTokens = 589
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            OcrSse = new(
                "multimodal-generation-vl-ocr",
                new ModelRequest<MultimodalInput, IMultimodalParameters>
                {
                    Model = "qwen-vl-ocr-latest",
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
                    RequestId = "6aaa3c55-98fc-498f-baa5-37ba21a708a3",
                    Output = new MultimodalOutput(
                        new List<MultimodalChoice>
                        {
                            new(
                                "stop",
                                MultimodalMessage.Assistant(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent(
                                            "产品介绍\n本品采用韩国进口纤维制造，不缩水、不变形、不发霉、\n不生菌、不伤物品表面。具有真正的不粘油、吸水力强、耐水\n浸、清洗干净、无毒、无残留、易晾干等特点。\n店家使用经验：不锈钢、陶瓷制品、浴盆、整体浴室大部分是\n白色的光洁表面，用其他的抹布擦洗表面污渍不易洗掉，太尖\n的容易划出划痕。使用这个仿真丝瓜布，沾少量中性洗涤剂揉\n出泡沫，很容易把这些表面污渍擦洗干净。\n6941990612023\n货号：2023")
                                    }))
                        }),
                    Usage = new MultimodalTokenUsage
                    {
                        InputTokens = 971,
                        OutputTokens = 155,
                        ImageTokens = 947,
                        TotalTokens = 1126,
                        InputTokensDetails = new MultimodalInputTokenDetails(ImageTokens: 947, TextTokens: 24),
                        OutputTokensDetails = new MultimodalOutputTokenDetails(TextTokens: 155)
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
                                    }))
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
                                    }))
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
                    new ModelRequest<MultimodalInput, IMultimodalParameters>
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
                        Parameters = new MultimodalParameters { IncrementalOutput = true }
                    },
                    new ModelResponse<MultimodalOutput, MultimodalTokenUsage>
                    {
                        RequestId = "80199c3f-e431-4310-b1de-f2ba11e554f7",
                        Output = new MultimodalOutput(
                            new List<MultimodalChoice>
                            {
                                new(
                                    "stop",
                                    MultimodalMessage.Assistant(
                                        new List<MultimodalMessageContent>
                                        {
                                            MultimodalMessageContent.TextContent(
                                                "这个视频展示了一位年轻女性的面部特写，她有着齐肩的棕色短发和刘海，穿着一件粉色针织开衫搭配白色内搭，脖子上戴着一条细小的项链。背景是模糊的城市建筑环境，阳光明媚，整体画面明亮且温暖。\n\n视频的具体过程如下：\n\n1. **初始画面**：视频开始时，女性面带微笑，眼神温和地注视着镜头，表情自然亲切。\n2. **表情变化**：她的笑容逐渐加深，从浅笑过渡到大笑，露出牙齿，显得非常开心和愉悦。\n3. **头部轻微晃动**：在笑的过程中，她的头部有轻微的前后晃动，增加了动态感和真实感。\n4. **眼神交流**：她一直保持与镜头的眼神接触，给人一种直接对话的感觉。\n5. **情绪传递**：整个过程中，她的表情从轻松愉快逐渐变为更加开朗和兴奋，传递出积极正面的情绪。\n6. **结束状态**：视频最后，她的笑容依然灿烂，但稍微收敛了一些，恢复到一个温和的笑容，结束于一个稳定的画面。\n\n整个视频通过细腻的表情变化和自然的动作，展现了人物的亲和力和愉悦心情，营造出一种温暖、阳光的氛围。右上角有“通义·AI合成”的水印，表明这是由AI技术生成的视频内容。")
                                        }))
                            }),
                        Usage = new MultimodalTokenUsage
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
                                        GenerateSchema<GetCurrentWeatherParameters>()))
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
                                        GenerateSchema<GetCurrentWeatherParameters>()))
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
                                            MultimodalMessageContent.TextContent("告别杭州的多云，我们驱车奔赴上海的晴朗。金色的阳光洒满海滩，海风微暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”击掌。此刻远离喧嚣，只有我们和治愈的暖阳，这就是最完美的周末。")
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
    }
}
