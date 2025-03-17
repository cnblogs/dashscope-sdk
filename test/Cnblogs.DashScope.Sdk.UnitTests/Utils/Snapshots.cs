using Cnblogs.DashScope.Core;
using Json.Schema;
using Json.Schema.Generation;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static class Snapshots
{
    public static class Error
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
            AuthError = new(
                "auth-error",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Model = "qwen-max",
                    Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？" },
                    Parameters = new TextGenerationParameters
                    {
                        ResultFormat = "text",
                        Seed = 1234,
                        MaxTokens = 1500,
                        TopP = 0.8f,
                        TopK = 100,
                        RepetitionPenalty = 1.1f,
                        Temperature = 0.85f,
                        Stop = new int[][] { [37763, 367] },
                        EnableSearch = false,
                        IncrementalOutput = false
                    }
                },
                new DashScopeError
                {
                    Code = "InvalidApiKey",
                    Message = "Invalid API-key provided.",
                    RequestId = "a1c0561c-1dfe-98a6-a62f-983577b8bc5e"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
            ParameterError = new(
                "parameter-error",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Model = "qwen-max",
                    Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？", Messages = [] },
                    Parameters = new TextGenerationParameters
                    {
                        ResultFormat = "text",
                        Seed = 1234,
                        MaxTokens = 1500,
                        TopP = 0.8f,
                        TopK = 100,
                        RepetitionPenalty = 1.1f,
                        Temperature = 0.85f,
                        Stop = new int[][] { [37763, 367] },
                        EnableSearch = false,
                        IncrementalOutput = false
                    }
                },
                new DashScopeError
                {
                    Code = "InvalidParameter",
                    Message = "Role must be user or assistant and Content length must be greater than 0",
                    RequestId = "a5898c04-d210-901b-965f-e4bd90478805"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
            ParameterErrorSse = new(
                "parameter-error",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Model = "qwen-max",
                    Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？", Messages = [] },
                    Parameters = new TextGenerationParameters
                    {
                        ResultFormat = "text",
                        Seed = 1234,
                        MaxTokens = 1500,
                        TopP = 0.8f,
                        TopK = 100,
                        RepetitionPenalty = 1.1f,
                        Temperature = 0.85f,
                        Stop = new int[][] { [37763, 367] },
                        EnableSearch = false,
                        IncrementalOutput = true
                    }
                },
                new DashScopeError
                {
                    Code = "InvalidParameter",
                    Message = "Role must be user or assistant and Content length must be greater than 0",
                    RequestId = "7671ecd8-93cc-9ee9-bc89-739f0fd8b809"
                });

        public static readonly RequestSnapshot<DashScopeError> UploadErrorNoSse = new(
            "upload-file-error",
            new DashScopeError
            {
                Code = "invalid_request_error",
                Message = "'purpose' must be 'file-extract'",
                RequestId = string.Empty
            });
    }

    public static class TextGeneration
    {
        public static class TextFormat
        {
            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SinglePrompt = new(
                    "single-generation-text",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？" },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "text",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            FinishReason = "stop", Text = "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下两个一相加的结果都是二。"
                        },
                        RequestId = "4ef2ed16-4dc3-9083-a723-fb2e80c84d3b",
                        Usage = new TextGenerationTokenUsage
                        {
                            InputTokens = 8,
                            OutputTokens = 35,
                            TotalTokens = 43
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SinglePromptIncremental = new(
                    "single-generation-text",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput { Prompt = "请问 1+1 是多少？" },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "text",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput { FinishReason = "stop", Text = "1+1等于2。" },
                        RequestId = "5b441aa7-0b9c-9fbc-ae0a-e2b212b71eac",
                        Usage = new TextGenerationTokenUsage
                        {
                            InputTokens = 16,
                            OutputTokens = 6,
                            TotalTokens = 22
                        }
                    });
        }

        public static class MessageFormat
        {
            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessage = new(
                    "single-generation-message",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [TextChatMessage.User("请问 1+1 是多少？")] },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何两个相同的数字相加都等于该数字的二倍。")
                                }
                            ]
                        },
                        RequestId = "e764bfe3-c0b7-97a0-ae57-cd99e1580960",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 47,
                            OutputTokens = 39,
                            InputTokens = 8
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleChatClientMessage = new(
                    "single-generation-message",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [TextChatMessage.User("请问 1+1 是多少？")] },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            ToolChoice = ToolChoice.AutoChoice
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何两个相同的数字相加都等于该数字的二倍。")
                                }
                            ]
                        },
                        RequestId = "e764bfe3-c0b7-97a0-ae57-cd99e1580960",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 47,
                            OutputTokens = 39,
                            InputTokens = 8
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageJson = new(
                    "single-generation-message-json",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [TextChatMessage.User("请问 1+1 是多少？用 JSON 格式输出。")] },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = false,
                            ResponseFormat = DashScopeResponseFormat.Json
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant("{\n  \"result\": 2\n}")
                                }
                            ]
                        },
                        RequestId = "6af9571b-1033-98f9-a287-c06f2e9d6f7f",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 34,
                            OutputTokens = 9,
                            InputTokens = 25
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageIncremental = new(
                    "single-generation-message",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [TextChatMessage.User("请问 1+1 是多少？")] },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下 1 加上另一个 1 的结果都是 2。")
                                }
                            ]
                        },
                        RequestId = "d272255f-82d7-9cc7-93c5-17ff77024349",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 48,
                            OutputTokens = 40,
                            InputTokens = 8
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageChatClientIncremental = new(
                    "single-generation-message",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [TextChatMessage.User("请问 1+1 是多少？")] },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new[] { "你好" },
                            IncrementalOutput = true,
                            ToolChoice = ToolChoice.AutoChoice
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下 1 加上另一个 1 的结果都是 2。")
                                }
                            ]
                        },
                        RequestId = "d272255f-82d7-9cc7-93c5-17ff77024349",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 48,
                            OutputTokens = 40,
                            InputTokens = 8
                        }
                    });

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> SingleMessageWithTools =
                    new(
                        "single-generation-message-with-tools",
                        new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                        {
                            Model = "qwen-max",
                            Input = new TextGenerationInput { Messages = [TextChatMessage.User("杭州现在的天气如何？")] },
                            Parameters = new TextGenerationParameters()
                            {
                                ResultFormat = "message",
                                Seed = 1234,
                                MaxTokens = 1500,
                                TopP = 0.8f,
                                TopK = 100,
                                RepetitionPenalty = 1.1f,
                                PresencePenalty = 1.2f,
                                Temperature = 0.85f,
                                Stop = new TextGenerationStop("你好"),
                                EnableSearch = false,
                                IncrementalOutput = false,
                                Tools =
                                [
                                    new ToolDefinition(
                                        "function",
                                        new FunctionDefinition(
                                            "get_current_weather",
                                            "获取现在的天气",
                                            new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>(
                                                    new SchemaGeneratorConfiguration
                                                    {
                                                        PropertyNameResolver = PropertyNameResolvers.LowerSnakeCase
                                                    })
                                                .Build()))
                                ],
                                ToolChoice = ToolChoice.FunctionChoice("get_current_weather")
                            }
                        },
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices =
                                [
                                    new TextGenerationChoice
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            string.Empty,
                                            toolCalls:
                                            [
                                                new ToolCall(
                                                    "call_cec4c19d27624537b583af",
                                                    ToolTypes.Function,
                                                    0,
                                                    new FunctionCall(
                                                        "get_current_weather",
                                                        """{"location": "浙江省杭州市"}"""))
                                            ])
                                    }
                                ]
                            },
                            RequestId = "67300049-c108-9987-b1c1-8e0ee2de6b5d",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 211,
                                OutputTokens = 8,
                                TotalTokens = 219
                            }
                        });

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> SingleMessageChatClientWithTools =
                    new(
                        "single-generation-message-with-tools",
                        new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                        {
                            Model = "qwen-max",
                            Input = new TextGenerationInput { Messages = [TextChatMessage.User("杭州现在的天气如何？")] },
                            Parameters = new TextGenerationParameters()
                            {
                                ResultFormat = "message",
                                Seed = 1234,
                                MaxTokens = 1500,
                                TopP = 0.8f,
                                TopK = 100,
                                RepetitionPenalty = 1.1f,
                                PresencePenalty = 1.2f,
                                Temperature = 0.85f,
                                Tools =
                                [
                                    new ToolDefinition(
                                        "function",
                                        new FunctionDefinition(
                                            "get_current_weather",
                                            "获取现在的天气",
                                            new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>(
                                                    new SchemaGeneratorConfiguration
                                                    {
                                                        PropertyNameResolver = PropertyNameResolvers.LowerSnakeCase
                                                    })
                                                .Build()))
                                ],
                                ToolChoice = ToolChoice.FunctionChoice("get_current_weather")
                            }
                        },
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices =
                                [
                                    new TextGenerationChoice
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            string.Empty,
                                            toolCalls:
                                            [
                                                new ToolCall(
                                                    "call_cec4c19d27624537b583af",
                                                    ToolTypes.Function,
                                                    0,
                                                    new FunctionCall(
                                                        "get_current_weather",
                                                        """{"location": "浙江省杭州市"}"""))
                                            ])
                                    }
                                ]
                            },
                            RequestId = "67300049-c108-9987-b1c1-8e0ee2de6b5d",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 211,
                                OutputTokens = 8,
                                TotalTokens = 219
                            }
                        });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationPartialMessageNoSse = new(
                    "conversation-generation-message-partial",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput()
                        {
                            Messages =
                            [
                                TextChatMessage.User("请对“春天来了，大地”这句话进行续写，来表达春天的美好和作者的喜悦之情"),
                                TextChatMessage.Assistant("春天来了，大地", true)
                            ]
                        },
                        Parameters = new TextGenerationParameters()
                        {
                            ResultFormat = ResultFormats.Message,
                            Seed = 1234,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>()
                    {
                        RequestId = "4c45d7fd-3158-9ff4-96a0-6e92c710df2c",
                        Output = new TextGenerationOutput()
                        {
                            Choices =
                            [
                                new TextGenerationChoice()
                                {
                                    FinishReason = "stop",
                                    Message =
                                        TextChatMessage.Assistant(
                                            "仿佛从漫长的冬眠中苏醒过来，万物复苏。嫩绿的小草悄悄地探出了头，争先恐后地想要沐浴在温暖的阳光下；五彩斑斓的花朵也不甘示弱，竞相绽放着自己最美丽的姿态，将田野、山林装扮得分外妖娆。微风轻轻吹过，带来了泥土的气息与花香混合的独特香味，让人心旷神怡。小鸟们开始忙碌起来，在枝头欢快地歌唱，似乎也在庆祝这个充满希望的新季节的到来。这一切美好景象不仅让人感受到了大自然的魅力所在，更激发了人们对生活无限热爱和向往的心情。")
                                }
                            ]
                        },
                        Usage = new TextGenerationTokenUsage()
                        {
                            TotalTokens = 165,
                            OutputTokens = 131,
                            InputTokens = 34
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationMessageIncremental = new(
                    "conversation-generation-message",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                [
                                    TextChatMessage.User("现在请你记住一个数字，42"),
                                    TextChatMessage.Assistant("好的，我已经记住了这个数字。"),
                                    TextChatMessage.User("请问我刚才提到的数字是多少？")
                                ]
                            },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop", Message = TextChatMessage.Assistant("您刚才提到的数字是42。")
                                }
                            ]
                        },
                        RequestId = "9188e907-56c2-9849-97f6-23f130f7fed7",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 33,
                            OutputTokens = 9,
                            InputTokens = 24
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationMessageWithFilesIncremental = new(
                    "conversation-generation-message-with-files",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-long",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                [
                                    TextChatMessage.File(
                                        ["file-fe-WTTG89tIUTd4ByqP3K48R3bn", "file-fe-l92iyRvJm9vHCCfonLckf1o2"]),
                                    TextChatMessage.User("这两个文件是相同的吗？")
                                ]
                            },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 1234,
                            MaxTokens = 1500,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new int[][] { [37763, 367] },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                            [
                                new TextGenerationChoice
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "你上传的两个文件并不相同。第一个文件`test1.txt`包含两行文本，每行都是“测试”。而第二个文件`test2.txt`只有一行文本，“测试2”。尽管它们都含有“测试”这个词，但具体内容和结构不同。")
                                }
                            ]
                        },
                        RequestId = "7865ae43-8379-9c79-bef6-95050868bc52",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 115,
                            OutputTokens = 57,
                            InputTokens = 58
                        }
                    });
        }
    }

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

    public static class TextEmbedding
    {
        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> NoSse = new(
            "text-embedding",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = ["代码改变世界"] },
                Model = "text-embedding-v2",
                Parameters = new TextEmbeddingParameters { TextType = "query" }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput([new TextEmbeddingItem(0, [])]),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new TextEmbeddingTokenUsage(3)
            });

        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> EmbeddingClientNoSse = new(
            "text-embedding",
            new ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>
            {
                Input = new TextEmbeddingInput { Texts = ["代码改变世界"] },
                Model = "text-embedding-v3",
                Parameters = new TextEmbeddingParameters { Dimension = 1024 }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput([new TextEmbeddingItem(0, [])]),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new TextEmbeddingTokenUsage(3)
            });

        public static readonly
            RequestSnapshot<ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters>,
                ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>> BatchNoSse = new(
                "batch-text-embedding",
                new ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters>
                {
                    Input = new BatchGetEmbeddingsInput
                    {
                        Url =
                            "https://modelscope.oss-cn-beijing.aliyuncs.com/resource/text_embedding_file.txt"
                    },
                    Model = "text-embedding-async-v2",
                    Parameters = new BatchGetEmbeddingsParameters { TextType = "query" }
                },
                new ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>
                {
                    RequestId = "db5ce040-4548-9919-9a75-3385ee152335",
                    Output = new BatchGetEmbeddingsOutput
                    {
                        TaskId = "6075262c-b56d-4968-9abf-2a9784a90f3e",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });
    }

    public static class Tokenization
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeNoSse = new(
                "tokenization",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = new TextGenerationInput { Messages = [TextChatMessage.User("代码改变世界")] },
                    Model = "qwen-max",
                    Parameters = new TextGenerationParameters { Seed = 1234 }
                },
                new ModelResponse<TokenizationOutput, TokenizationUsage>
                {
                    Output = new TokenizationOutput([46100, 101933, 99489], ["代码", "改变", "世界"]),
                    Usage = new TokenizationUsage(3),
                    RequestId = "6615ba01-081d-9147-93ff-7bd26f3adf93"
                });
    }

    public static class Tasks
    {
        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            Unknown = new(
                "get-task-unknown",
                new DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                    "85c25460-6440-91a7-b14e-2978fe60bd0f",
                    new BatchGetEmbeddingsOutput { TaskId = "1111", TaskStatus = DashScopeTaskStatus.Unknown }));

        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            BatchEmbeddingSuccess = new(
                "get-task-batch-text-embedding-success",
                new DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>(
                    "0b2ebeda-a91b-948f-986a-d395cbf1d0e1",
                    new BatchGetEmbeddingsOutput
                    {
                        TaskId = "7408ef3d-a0be-4379-9e72-a6e95a569483",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        Url =
                            "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/5fc5c860/2024-11-25/c6c4456e-3c66-42ba-a52a-a16c58dda4d6_output_1732514147173.txt.gz?Expires=1732773347&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=perMNS1RdHHroUn2YnXxzTmOZtg%3D",
                        SubmitTime = new DateTime(2024, 11, 25, 13, 55, 46, 536),
                        ScheduledTime = new DateTime(2024, 11, 25, 13, 55, 46, 557),
                        EndTime = new DateTime(2024, 11, 25, 13, 55, 47, 446)
                    },
                    new TextEmbeddingTokenUsage(28)));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisRunning =
                new(
                    "get-task-running",
                    new DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>(
                        "edbd4e81-d37b-97f1-9857-d7394829dd0f",
                        new ImageSynthesisOutput
                        {
                            TaskStatus = DashScopeTaskStatus.Running,
                            TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                            SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                            ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                            TaskMetrics = new DashScopeTaskMetrics(4, 0, 0)
                        }));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisSuccess = new(
                "get-task-image-synthesis-success",
                new DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>(
                    "6662e925-4846-9afe-a3af-0d131805d378",
                    new ImageSynthesisOutput
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                        ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                        EndTime = new DateTime(2024, 3, 1, 17, 38, 55, 565),
                        Results =
                        [
                            new ImageSynthesisResult(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1d/d4/20240301/8d820c8d/4c48fa53-2907-499b-b9ac-76477fe8d299-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=bEfLmd%2BarXgZyhxcVYOWs%2BovJb8%3D"),
                            new ImageSynthesisResult(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/79/20240301/3ab595ad/aa3e6d8d-884d-4431-b9c2-3684edeb072e-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=fdPScmRkIXyH3TSaSaWwvVjxREQ%3D"),
                            new ImageSynthesisResult(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/0f/20240301/3ab595ad/ecfe06b3-b91c-4950-a932-49ea1619a1f9-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=gNuVAt8iy4X8Nl2l3K4Gu4f0ydw%3D"),
                            new ImageSynthesisResult(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/3d/20240301/3ab595ad/3fca748e-d491-458a-bb72-73649af33209-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=Mx5TueC9I9yfDno9rjzi48opHtM%3D")
                        ],
                        TaskMetrics = new DashScopeTaskMetrics(4, 4, 0)
                    },
                    new ImageSynthesisUsage(4)));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>>
            ImageGenerationSuccess = new(
                "get-task-image-generation-success",
                new DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>(
                    "f927c766-5079-90f8-9354-6a87d2167897",
                    new ImageGenerationOutput
                    {
                        TaskId = "c4f94e00-5899-431b-9579-eb1ebe686379",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 2, 22, 22, 13, 026),
                        ScheduledTime = new DateTime(2024, 3, 2, 22, 22, 13, 051),
                        EndTime = new DateTime(2024, 3, 2, 22, 22, 21),
                        StartTime = new DateTime(2024, 3, 2, 22, 22, 13),
                        StyleIndex = 3,
                        ErrorCode = 0,
                        ErrorMessage = "Success",
                        Results =
                        [
                            new ImageGenerationResult(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/viapi-video/2024-03-02/ac5d435a-9ea9-4287-8666-e1be7bbba943/20240302222213528791_style3_jxdf6o4zwy.jpg?Expires=1709475741&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=LM26fy1Pk8rCfPzihzpUqa3Vst8%3D")
                        ]
                    },
                    new ImageGenerationUsage(1)));

        public static readonly RequestSnapshot<DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>>
            BackgroundGenerationSuccess = new(
                "get-task-background-generation-success",
                new DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>(
                    "8b22164d-c784-9a31-bda3-3c26259d4213",
                    new BackgroundGenerationOutput
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 4, 10, 8, 57, 333),
                        ScheduledTime = new DateTime(2024, 3, 4, 10, 8, 57, 363),
                        EndTime = new DateTime(2024, 3, 4, 10, 9, 7, 727),
                        Results =
                        [
                            new BackgroundGenerationResult(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_0_02dc0bba-8b1d-4648-8b95-eb2b92fe715d.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=OYstgSxWOl%2FOxYTLa2Mx3bi2RWw%3D"),
                            new BackgroundGenerationResult(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_1_e1af86ec-152a-4ebe-b2a0-b40a592043b2.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=p0UXTUdXfp0tFlt0K5tDsA%2Fxl1M%3D")
                        ],
                        TaskMetrics = new DashScopeTaskMetrics(2, 2, 0),
                        TextResults =
                            new BackgroundGenerationTextResult(
                                [
                                    new BackgroundGenerationTextResultUrl(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_0_4645005c-713d-4e92-9629-b12cbe5f3671.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=kmZGXc2s8P4uI%2BVrADITyrPz82U%3D"),
                                    new BackgroundGenerationTextResultUrl(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_1_b1979b75-c553-4d9b-9c9f-80f401a0d124.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=cb1Qg%2FkIuZyI7XQqWHjP712N0ak%3D")
                                ],
                                [
                                    new BackgroundGenerationTextResultParams(
                                        0,
                                        [
                                            new BackgroundGenerationTextResultLayer(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#521b08",
                                                Opacity: 0.8f,
                                                Radius: 0,
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    [
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#521b0800",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#521b08ff",
                                                            1)
                                                    ])
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new BackgroundGenerationTextResultLayer(
                                                1,
                                                "text",
                                                25,
                                                319,
                                                385,
                                                77,
                                                SubType: "Title",
                                                FontWeight: "Regular",
                                                FontSize: 67,
                                                Content: "分享好时光",
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                FontColor: "#e6baa7",
                                                TextShadow: "1px 0px #80808080",
                                                TextStroke: "1px #fffffff0",
                                                FontFamily: "站酷文艺体",
                                                Alignment: "center",
                                                FontLineThrough: false,
                                                Direction: "horizontal",
                                                Opacity: 1f),
                                            new BackgroundGenerationTextResultLayer(
                                                2,
                                                "text_mask",
                                                118,
                                                395,
                                                233,
                                                50,
                                                Color: "#e6baa7",
                                                Opacity: 1f,
                                                Radius: 37,
                                                BoxShadow: "2px 1px #80808080",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    [
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#e6baa7ff",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#e6baa7ff",
                                                            1)
                                                    ])
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 0 },
                                                        { "x1", 0 },
                                                        { "y2", 50 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new BackgroundGenerationTextResultLayer(
                                                3,
                                                "text",
                                                118,
                                                395,
                                                233,
                                                50,
                                                FontWeight: "Medium",
                                                FontSize: 27,
                                                Content: "只为不一样的你",
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                SubType: "SubTitle",
                                                FontColor: "#223629",
                                                TextShadow: null,
                                                FontFamily: "阿里巴巴普惠体",
                                                Alignment: "center",
                                                Opacity: 1f,
                                                FontLineThrough: false,
                                                Direction: "horizontal")
                                        ]),
                                    new BackgroundGenerationTextResultParams(
                                        1,
                                        [
                                            new BackgroundGenerationTextResultLayer(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#efeae4",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    [
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#efeae400",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#efeae4ff",
                                                            1)
                                                    ])
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                },
                                                Opacity: 0.8f,
                                                Radius: 0),
                                            new BackgroundGenerationTextResultLayer(
                                                1,
                                                "text",
                                                25,
                                                319,
                                                385,
                                                77,
                                                SubType: "Title",
                                                Content: "分享好时光",
                                                FontWeight: "Regular",
                                                FontSize: 67,
                                                FontUnderLine: false,
                                                LineHeight: 1f,
                                                FontItalic: false,
                                                FontColor: "#421f12",
                                                TextStroke: "1px #fffffff0",
                                                TextShadow: "0px 2px #80808080",
                                                FontFamily: "钉钉进步体",
                                                Alignment: "center",
                                                Opacity: 1f,
                                                FontLineThrough: false,
                                                Direction: "horizontal"),
                                            new BackgroundGenerationTextResultLayer(
                                                2,
                                                "text_mask",
                                                118,
                                                395,
                                                233,
                                                50,
                                                Color: "#421f12",
                                                Gradient: new BackgroundGenerationTextResultGradient(
                                                    "linear",
                                                    "pixels",
                                                    [
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#421f12ff",
                                                            0),
                                                        new BackgroundGenerationTextResultGradientColorStop(
                                                            "#421f12ff",
                                                            1)
                                                    ])
                                                {
                                                    Coords = new Dictionary<string, int>
                                                    {
                                                        { "y1", 0 },
                                                        { "x1", 0 },
                                                        { "y2", 50 },
                                                        { "x2", 0 }
                                                    }
                                                },
                                                Opacity: 1f,
                                                Radius: 37,
                                                BoxShadow: "0px 0px #80808080"),
                                            new BackgroundGenerationTextResultLayer(
                                                3,
                                                "text",
                                                118,
                                                395,
                                                233,
                                                50,
                                                FontWeight: "Regular",
                                                FontSize: 27,
                                                Content: "只为不一样的你",
                                                FontUnderLine: false,
                                                LineHeight: 1,
                                                FontItalic: false,
                                                SubType: "SubTitle",
                                                FontColor: "#f1eeec",
                                                TextShadow: null,
                                                FontFamily: "阿里巴巴普惠体",
                                                Alignment: "center",
                                                Opacity: 1,
                                                FontLineThrough: false,
                                                Direction: "horizontal")
                                        ])
                                ])
                    },
                    new BackgroundGenerationUsage(2)));

        public static readonly RequestSnapshot<DashScopeTaskOperationResponse> CancelCompletedTask = new(
            "cancel-completed-task",
            new DashScopeTaskOperationResponse(
                "4d496c94-1389-9ca9-a92a-3e732f675686",
                "UnsupportedOperation",
                "Failed to cancel the task, please confirm if the task is in PENDING status."));

        public static readonly RequestSnapshot<DashScopeTaskList> ListTasks = new(
            "list-task",
            new DashScopeTaskList(
                "fcb29ae5-a352-9e7b-901c-e53525376cde",
                [
                    new DashScopeTaskListItem(
                        "42677",
                        "1493478651020171",
                        "1493478651020171",
                        1709260684485,
                        1709260684527,
                        1709260685184,
                        "cn-beijing",
                        "db5ce040-4548-9919-9a75-3385ee152335",
                        DashScopeTaskStatus.Succeeded,
                        "6075262c-b56d-4968-9abf-2a9784a90f3e",
                        "apikey:v1:embeddings:text-embedding:text-embedding:text-embedding-async-v2",
                        "text-embedding-async-v2")
                ],
                1,
                1,
                1,
                10));
    }

    public static class ImageSynthesis
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>,
                ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateTask = new(
                "image-synthesis",
                new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>
                {
                    Model = "wanx-v1",
                    Input = new ImageSynthesisInput { Prompt = "一只奔跑的猫" },
                    Parameters = new ImageSynthesisParameters
                    {
                        N = 4,
                        Seed = 42,
                        Size = "1024*1024",
                        Style = "<sketch>"
                    }
                },
                new ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>
                {
                    Output = new ImageSynthesisOutput
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Pending
                    },
                    RequestId = "33da8e6b-1309-9a44-be83-352165959608"
                });
    }

    public static class ImageGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<ImageGenerationInput>,
                ModelResponse<ImageGenerationOutput, ImageGenerationUsage>> CreateTaskNoSse = new(
                "image-generation",
                new ModelRequest<ImageGenerationInput>
                {
                    Model = "wanx-style-repaint-v1",
                    Input = new ImageGenerationInput
                    {
                        ImageUrl =
                            "https://public-vigen-video.oss-cn-shanghai.aliyuncs.com/public/dashscope/test.png",
                        StyleIndex = 3
                    }
                },
                new ModelResponse<ImageGenerationOutput, ImageGenerationUsage>
                {
                    Output = new ImageGenerationOutput
                    {
                        TaskId = "c4f94e00-5899-431b-9579-eb1ebe686379", TaskStatus = DashScopeTaskStatus.Pending,
                    },
                    RequestId = "565ff453-bcf7-99ec-9fbe-b99bb8caab07"
                });
    }

    public static class BackgroundGeneration
    {
        public static readonly
            RequestSnapshot<ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>,
                ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>> CreateTaskNoSse = new(
                "background-generation",
                new ModelRequest<BackgroundGenerationInput, IBackgroundGenerationParameters>
                {
                    Input = new BackgroundGenerationInput
                    {
                        BaseImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/hailuo_2236873898_2.png",
                        RefImageUrl =
                            "http://inner-materials.oss-cn-beijing.aliyuncs.com/graphic_design/jianguo/lllcho.lc/test_data/demo_example/demo/d1faf4f26c8c4ea798d043a8bf3784bb_2.png",
                        RefPrompt = "远处有朝阳升起",
                        Title = "分享好时光",
                        SubTitle = "只为不一样的你",
                        NegRefPrompt = "低质量的，模糊的，错误的"
                    },
                    Model = "wanx-background-generation-v2",
                    Parameters = new BackgroundGenerationParameters
                    {
                        N = 2,
                        NoiseLevel = 300,
                        RefPromptWeight = 0.5f
                    }
                },
                new ModelResponse<BackgroundGenerationOutput, BackgroundGenerationUsage>
                {
                    RequestId = "a010df33-effc-9e32-aaa0-e55fffa40ef5",
                    Output = new BackgroundGenerationOutput
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });
    }

    public static class File
    {
        public static readonly FileInfo TestFile = new FileInfo("RawHttpData/test1.txt");

        public static readonly RequestSnapshot<DashScopeFile> UploadFileNoSse = new(
            "upload-file",
            new DashScopeFile("file-fe-qBKjZKfTx64R9oYmwyovNHBH", "file", 6, 1720582024, "test1.txt", "file-extract"));

        public static readonly RequestSnapshot<DashScopeFile> GetFileNoSse = new(
            "get-file",
            new DashScopeFile("file-fe-qBKjZKfTx64R9oYmwyovNHBH", "file", 6, 1720582024, "test1.txt", "file-extract"));

        public static readonly RequestSnapshot<DashScopeFileList> ListFileNoSse = new(
            "list-files",
            new DashScopeFileList(
                "list",
                false,
                [
                    new DashScopeFile(
                        "file-fe-qBKjZKfTx64R9oYmwyovNHBH",
                        "file",
                        6,
                        1720582024,
                        "test1.txt",
                        "file-extract"),
                    new DashScopeFile(
                        "file-fe-WTTG89tIUTd4ByqP3K48R3bn",
                        "file",
                        6,
                        1720535665,
                        "test1.txt",
                        "file-extract")
                ]));

        public static readonly RequestSnapshot<DashScopeDeleteFileResult> DeleteFileNoSse = new(
            "delete-file",
            new DashScopeDeleteFileResult("file", true, "file-fe-qBKjZKfTx64R9oYmwyovNHBH"));
    }
}
