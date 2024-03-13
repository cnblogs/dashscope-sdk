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
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？" },
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
                new()
                {
                    Code = "InvalidApiKey",
                    Message = "No API-key provided.",
                    RequestId = "862e8e7a-1fb8-9a50-aa7b-a808c2a988ee"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
            ParameterError = new(
                "parameter-error",
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？", Messages = [] },
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
                new()
                {
                    Code = "InvalidParameter",
                    Message = "Role must be user or assistant and Content length must be greater than 0",
                    RequestId = "a5898c04-d210-901b-965f-e4bd90478805"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>, DashScopeError>
            ParameterErrorSse = new(
                "parameter-error",
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？", Messages = [] },
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
                new()
                {
                    Code = "InvalidParameter",
                    Message = "Role must be user or assistant and Content length must be greater than 0",
                    RequestId = "7671ecd8-93cc-9ee9-bc89-739f0fd8b809"
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
                    new()
                    {
                        Model = "qwen-max",
                        Input = new() { Prompt = "请问 1+1 是多少？" },
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
                    new()
                    {
                        Output = new()
                        {
                            FinishReason = "stop", Text = "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下两个一相加的结果都是二。"
                        },
                        RequestId = "4ef2ed16-4dc3-9083-a723-fb2e80c84d3b",
                        Usage = new()
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
                    new()
                    {
                        Model = "qwen-max",
                        Input = new() { Prompt = "请问 1+1 是多少？" },
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
                    new()
                    {
                        Output = new()
                        {
                            FinishReason = "stop", Text = "1+1 等于 2。这是最基本的数学加法原则，在十进制数系统中，任何两个相同的数字相加都等于该数字的两倍。"
                        },
                        RequestId = "893a2304-f032-9c7f-bde8-da5e3c1288fc",
                        Usage = new()
                        {
                            InputTokens = 8,
                            OutputTokens = 38,
                            TotalTokens = 46
                        }
                    });
        }

        public static class MessageFormat
        {
            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessage = new(
                    "single-generation-message",
                    new()
                    {
                        Model = "qwen-max",
                        Input =
                            new() { Messages = [new("user", "请问 1+1 是多少？")] },
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
                    new()
                    {
                        Output = new()
                        {
                            Choices =
                            [
                                new()
                                {
                                    FinishReason = "stop",
                                    Message = new(
                                        "assistant",
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何两个相同的数字相加都等于该数字的二倍。")
                                }
                            ]
                        },
                        RequestId = "e764bfe3-c0b7-97a0-ae57-cd99e1580960",
                        Usage = new()
                        {
                            TotalTokens = 47,
                            OutputTokens = 39,
                            InputTokens = 8
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageIncremental = new(
                    "single-generation-message",
                    new()
                    {
                        Model = "qwen-max",
                        Input =
                            new() { Messages = [new("user", "请问 1+1 是多少？")] },
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
                    new()
                    {
                        Output = new()
                        {
                            Choices =
                            [
                                new()
                                {
                                    FinishReason = "stop",
                                    Message = new(
                                        "assistant",
                                        "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下 1 加上另一个 1 的结果都是 2。")
                                }
                            ]
                        },
                        RequestId = "d272255f-82d7-9cc7-93c5-17ff77024349",
                        Usage = new()
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
                        new()
                        {
                            Model = "qwen-max",
                            Input = new() { Messages = [new("user", "杭州现在的天气如何？")] },
                            Parameters = new TextGenerationParameters()
                            {
                                ResultFormat = "message",
                                Seed = 1234,
                                MaxTokens = 1500,
                                TopP = 0.8f,
                                TopK = 100,
                                RepetitionPenalty = 1.1f,
                                Temperature = 0.85f,
                                Stop = new([[37763, 367]]),
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
                                                    new()
                                                    {
                                                        PropertyNameResolver = PropertyNameResolvers.LowerSnakeCase
                                                    })
                                                .Build()))
                                ]
                            }
                        },
                        new()
                        {
                            Output = new()
                            {
                                Choices =
                                [
                                    new()
                                    {
                                        FinishReason = "tool_calls",
                                        Message = new(
                                            "assistant",
                                            string.Empty,
                                            [
                                                new(
                                                    string.Empty,
                                                    ToolTypes.Function,
                                                    new(
                                                        "get_current_weather",
                                                        """{"location": "浙江省杭州市", "unit": "Celsius"}"""))
                                            ])
                                    }
                                ]
                            },
                            RequestId = "40b4361e-e936-91b5-879d-355a45d670f8",
                            Usage = new()
                            {
                                InputTokens = 5,
                                OutputTokens = 31,
                                TotalTokens = 36
                            }
                        });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationMessageIncremental = new(
                    "conversation-generation-message",
                    new()
                    {
                        Model = "qwen-max",
                        Input =
                            new()
                            {
                                Messages =
                                [
                                    new("user", "现在请你记住一个数字，42"),
                                    new("assistant", "好的，我已经记住了这个数字。"),
                                    new("user", "请问我刚才提到的数字是多少？")
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
                    new()
                    {
                        Output = new()
                        {
                            Choices =
                            [
                                new() { FinishReason = "stop", Message = new("assistant", "您刚才提到的数字是42。") }
                            ]
                        },
                        RequestId = "9188e907-56c2-9849-97f6-23f130f7fed7",
                        Usage = new()
                        {
                            TotalTokens = 33,
                            OutputTokens = 9,
                            InputTokens = 24
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
                new()
                {
                    Model = "qwen-vl-plus",
                    Input = new()
                    {
                        Messages =
                        [
                            new(
                                "system",
                                [new(null, "You are a helpful assistant.")]),
                            new(
                                "user",
                                [
                                    new("https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                    new(null, "这个图片是哪里，请用简短的语言回答")
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
                new()
                {
                    Output = new(
                    [
                        new(
                            "stop",
                            new(
                                "assistant",
                                [
                                    new(
                                        null,
                                        "这张照片显示的是海滩景色，但是无法确定具体的位置信息。图中有一名女子和一只狗在沙滩上互动。背景中有海洋和日出或日落的光线。由于缺乏特定地标或者细节特征，仅凭此图像很难精确识别具体的地点。")
                                ]))
                    ]),
                    RequestId = "a2a5f2e6-c6d7-9e04-9f92-1d3eee274198",
                    Usage = new()
                    {
                        OutputTokens = 58,
                        InputTokens = 1284,
                        ImageTokens = 1247
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlSse =
            new(
                "multimodal-generation-vl",
                new()
                {
                    Model = "qwen-vl-plus",
                    Input = new()
                    {
                        Messages =
                        [
                            new(
                                "system",
                                [new(null, "You are a helpful assistant.")]),
                            new(
                                "user",
                                [
                                    new("https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                    new(null, "这个图片是哪里，请用简短的语言回答")
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
                new()
                {
                    Output = new(
                    [
                        new(
                            "stop",
                            new(
                                "assistant",
                                [
                                    new(
                                        null,
                                        "这张照片显示的是海滩景色，但是无法确定具体的位置信息。图中有一名女子和一只狗在沙滩上互动。背景中有海洋和夕阳的余晖照耀着天空。请注意这是一张插画风格的艺术作品，并非实际的照片或新闻内容。")
                                ]))
                    ]),
                    RequestId = "81001ee4-6155-9d17-8533-195f61c8f036",
                    Usage = new()
                    {
                        OutputTokens = 58,
                        InputTokens = 1284,
                        ImageTokens = 1247
                    }
                });

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
            AudioNoSse = new(
                "multimodal-generation-audio",
                new()
                {
                    Model = "qwen-audio-turbo",
                    Input = new()
                    {
                        Messages =
                        [
                            new(
                                "system",
                                [new(Text: "You are a helpful assistant.")]),
                            new(
                                "user",
                                [
                                    new(Audio: "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                    new(Text: "这段音频在说什么，请用简短的语言回答")
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
                new()
                {
                    RequestId = "6b6738bd-dd9d-9e78-958b-02574acbda44",
                    Output = new(
                    [
                        new(
                            "stop",
                            new(
                                "assistant",
                                [
                                    new(
                                        Text:
                                        "这段音频在说中文，内容是\"没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网未来没有我互联网\"。")
                                ]))
                    ]),
                    Usage = new()
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
                new()
                {
                    Model = "qwen-audio-turbo",
                    Input = new()
                    {
                        Messages =
                        [
                            new(
                                "system",
                                [new(Text: "You are a helpful assistant.")]),
                            new(
                                "user",
                                [
                                    new(Audio: "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/2channel_16K.wav"),
                                    new(Text: "这段音频的第一句话说了什么？")
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
                new()
                {
                    RequestId = "bb6ab962-af57-99f1-9af8-eb7016ebc18e",
                    Output = new(
                    [
                        new(
                            "stop",
                            new(
                                "assistant",
                                [
                                    new(Text: "第一句话说了没有我互联网。")
                                ]))
                    ]),
                    Usage = new()
                    {
                        InputTokens = 783,
                        OutputTokens = 7,
                        AudioTokens = 752
                    }
                });
    }

    public static class TextEmbedding
    {
        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, ITextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> NoSse = new(
            "text-embedding",
            new()
            {
                Input = new() { Texts = ["代码改变世界"] },
                Model = "text-embedding-v2",
                Parameters = new TextEmbeddingParameters { TextType = "query" }
            },
            new()
            {
                Output = new([new(0, [])]),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new(3)
            });

        public static readonly
            RequestSnapshot<ModelRequest<BatchGetEmbeddingsInput, IBatchGetEmbeddingsParameters>,
                ModelResponse<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>> BatchNoSse = new(
                "batch-text-embedding",
                new()
                {
                    Input = new()
                    {
                        Url =
                            "https://modelscope.oss-cn-beijing.aliyuncs.com/resource/text_embedding_file.txt"
                    },
                    Model = "text-embedding-async-v2",
                    Parameters = new BatchGetEmbeddingsParameters { TextType = "query" }
                },
                new()
                {
                    RequestId = "db5ce040-4548-9919-9a75-3385ee152335",
                    Output = new()
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
                new()
                {
                    Input = new() { Messages = [new("user", "代码改变世界")] },
                    Model = "qwen-max",
                    Parameters = new TextGenerationParameters { Seed = 1234 }
                },
                new()
                {
                    Output = new([46100, 101933, 99489], ["代码", "改变", "世界"]),
                    Usage = new(3),
                    RequestId = "6615ba01-081d-9147-93ff-7bd26f3adf93"
                });
    }

    public static class Tasks
    {
        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            Unknown = new(
                "get-task-unknown",
                new(
                    "85c25460-6440-91a7-b14e-2978fe60bd0f",
                    new() { TaskId = "1111", TaskStatus = DashScopeTaskStatus.Unknown }));

        public static readonly RequestSnapshot<DashScopeTask<BatchGetEmbeddingsOutput, TextEmbeddingTokenUsage>>
            BatchEmbeddingSuccess = new(
                "get-task-batch-text-embedding-success",
                new(
                    "b41afd70-251a-9625-97fd-63caf63edb44",
                    new()
                    {
                        TaskId = "6075262c-b56d-4968-9abf-2a9784a90f3e",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        Url =
                            "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/5fc5c860/2024-03-01/78a8c5e1-cc44-497e-b8c8-1a46e7e57d03_output_1709260685020.txt.gz?Expires=1709519885&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=lUaHmlf5XkjBBb8Yj3Y%2FZMb%2BhA4%3D",
                        SubmitTime = new DateTime(2024, 3, 1, 10, 38, 04, 485),
                        ScheduledTime = new DateTime(2024, 3, 1, 10, 38, 04, 527),
                        EndTime = new DateTime(2024, 3, 1, 10, 38, 05, 184)
                    },
                    new(28)));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisRunning =
                new(
                    "get-task-running",
                    new(
                        "edbd4e81-d37b-97f1-9857-d7394829dd0f",
                        new()
                        {
                            TaskStatus = DashScopeTaskStatus.Running,
                            TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                            SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                            ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                            TaskMetrics = new(4, 0, 0)
                        }));

        public static readonly RequestSnapshot<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>>
            ImageSynthesisSuccess = new(
                "get-task-image-synthesis-success",
                new(
                    "6662e925-4846-9afe-a3af-0d131805d378",
                    new()
                    {
                        TaskId = "9e2b6ef6-285d-4efa-8651-4dbda7d571fa",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 1, 17, 38, 24, 817),
                        ScheduledTime = new DateTime(2024, 3, 1, 17, 38, 24, 831),
                        EndTime = new DateTime(2024, 3, 1, 17, 38, 55, 565),
                        Results =
                        [
                            new(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1d/d4/20240301/8d820c8d/4c48fa53-2907-499b-b9ac-76477fe8d299-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=bEfLmd%2BarXgZyhxcVYOWs%2BovJb8%3D"),
                            new(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/79/20240301/3ab595ad/aa3e6d8d-884d-4431-b9c2-3684edeb072e-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=fdPScmRkIXyH3TSaSaWwvVjxREQ%3D"),
                            new(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/0f/20240301/3ab595ad/ecfe06b3-b91c-4950-a932-49ea1619a1f9-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=gNuVAt8iy4X8Nl2l3K4Gu4f0ydw%3D"),
                            new(
                                "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/1d/3d/20240301/3ab595ad/3fca748e-d491-458a-bb72-73649af33209-1.png?Expires=1709372333&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=Mx5TueC9I9yfDno9rjzi48opHtM%3D")
                        ],
                        TaskMetrics = new(4, 4, 0)
                    },
                    new(4)));

        public static readonly RequestSnapshot<DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>>
            ImageGenerationSuccess = new(
                "get-task-image-generation-success",
                new(
                    "f927c766-5079-90f8-9354-6a87d2167897",
                    new()
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
                new(
                    "8b22164d-c784-9a31-bda3-3c26259d4213",
                    new()
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Succeeded,
                        SubmitTime = new DateTime(2024, 3, 4, 10, 8, 57, 333),
                        ScheduledTime = new DateTime(2024, 3, 4, 10, 8, 57, 363),
                        EndTime = new DateTime(2024, 3, 4, 10, 9, 7, 727),
                        Results =
                        [
                            new(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_0_02dc0bba-8b1d-4648-8b95-eb2b92fe715d.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=OYstgSxWOl%2FOxYTLa2Mx3bi2RWw%3D"),
                            new(
                                "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100905_1_e1af86ec-152a-4ebe-b2a0-b40a592043b2.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=p0UXTUdXfp0tFlt0K5tDsA%2Fxl1M%3D")
                        ],
                        TaskMetrics = new(2, 2, 0),
                        TextResults =
                            new(
                                [
                                    new(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_0_4645005c-713d-4e92-9629-b12cbe5f3671.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=kmZGXc2s8P4uI%2BVrADITyrPz82U%3D"),
                                    new(
                                        "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/466b5214/20240304/100901_1_b1979b75-c553-4d9b-9c9f-80f401a0d124.png?Expires=1709604547&OSSAccessKeyId=LTAI5tQZd8AEcZX6KZV4G8qL&Signature=cb1Qg%2FkIuZyI7XQqWHjP712N0ak%3D")
                                ],
                                [
                                    new(
                                        0,
                                        [
                                            new(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#521b08",
                                                Opacity: 0.8f,
                                                Radius: 0,
                                                Gradient: new(
                                                    "linear",
                                                    "pixels",
                                                    [new("#521b0800", 0), new("#521b08ff", 1)])
                                                {
                                                    Coords = new()
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new(
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
                                            new(
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
                                                Gradient: new(
                                                    "linear",
                                                    "pixels",
                                                    [new("#e6baa7ff", 0), new("#e6baa7ff", 1)])
                                                {
                                                    Coords = new()
                                                    {
                                                        { "y1", 0 },
                                                        { "x1", 0 },
                                                        { "y2", 50 },
                                                        { "x2", 0 }
                                                    }
                                                }),
                                            new(
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
                                    new(
                                        1,
                                        [
                                            new(
                                                0,
                                                "text_mask",
                                                0,
                                                0,
                                                1024,
                                                257,
                                                Color: "#efeae4",
                                                Gradient: new(
                                                    "linear",
                                                    "pixels",
                                                    [new("#efeae400", 0), new("#efeae4ff", 1)])
                                                {
                                                    Coords = new()
                                                    {
                                                        { "y1", 257 },
                                                        { "x1", 0 },
                                                        { "y2", 0 },
                                                        { "x2", 0 }
                                                    }
                                                },
                                                Opacity: 0.8f,
                                                Radius: 0),
                                            new(
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
                                            new(
                                                2,
                                                "text_mask",
                                                118,
                                                395,
                                                233,
                                                50,
                                                Color: "#421f12",
                                                Gradient: new(
                                                    "linear",
                                                    "pixels",
                                                    [new("#421f12ff", 0), new("#421f12ff", 1)])
                                                {
                                                    Coords = new()
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
                                            new(
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
                    new(2)));

        public static readonly RequestSnapshot<DashScopeTaskOperationResponse> CancelCompletedTask = new(
            "cancel-completed-task",
            new(
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
                new()
                {
                    Model = "wanx-v1",
                    Input = new() { Prompt = "一只奔跑的猫" },
                    Parameters = new ImageSynthesisParameters
                    {
                        N = 4,
                        Seed = 42,
                        Size = "1024*1024",
                        Style = "<sketch>"
                    }
                },
                new()
                {
                    Output = new()
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
                new()
                {
                    Model = "wanx-style-repaint-v1",
                    Input = new()
                    {
                        ImageUrl =
                            "https://public-vigen-video.oss-cn-shanghai.aliyuncs.com/public/dashscope/test.png",
                        StyleIndex = 3
                    }
                },
                new()
                {
                    Output = new()
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
                new()
                {
                    Input = new()
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
                new()
                {
                    RequestId = "a010df33-effc-9e32-aaa0-e55fffa40ef5",
                    Output = new()
                    {
                        TaskId = "b2e98d78-c79b-431c-b2d7-c7bcd54465da",
                        TaskStatus = DashScopeTaskStatus.Pending
                    }
                });
    }
}
