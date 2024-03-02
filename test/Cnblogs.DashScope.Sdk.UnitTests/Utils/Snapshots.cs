namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

public static class Snapshots
{
    public static class Error
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            AuthError = new(
                "auth-error",
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？" },
                    Parameters = new()
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
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            ParameterError = new(
                "parameter-error",
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？", Messages = [] },
                    Parameters = new()
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
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            ParameterErrorSse = new(
                "parameter-error",
                new()
                {
                    Model = "qwen-max",
                    Input = new() { Prompt = "请问 1+1 是多少？", Messages = [] },
                    Parameters = new()
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
            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SinglePrompt = new(
                    "single-generation-text",
                    new()
                    {
                        Model = "qwen-max",
                        Input = new() { Prompt = "请问 1+1 是多少？" },
                        Parameters = new()
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SinglePromptIncremental = new(
                    "single-generation-text",
                    new()
                    {
                        Model = "qwen-max",
                        Input = new() { Prompt = "请问 1+1 是多少？" },
                        Parameters = new()
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
            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessage = new(
                    "single-generation-message",
                    new()
                    {
                        Model = "qwen-max",
                        Input =
                            new() { Messages = [new("user", "请问 1+1 是多少？")] },
                        Parameters = new()
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageIncremental = new(
                    "single-generation-message",
                    new()
                    {
                        Model = "qwen-max",
                        Input =
                            new() { Messages = [new("user", "请问 1+1 是多少？")] },
                        Parameters = new()
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
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
                        Parameters = new()
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
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
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
                    Parameters = new()
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

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
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
                    Parameters = new()
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

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
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
                    Parameters = new()
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

        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
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
                    Parameters = new()
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
        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, TextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> NoSse = new(
            "text-embedding",
            new()
            {
                Input = new() { Texts = ["代码改变世界"] },
                Model = "text-embedding-v2",
                Parameters = new() { TextType = "query" }
            },
            new()
            {
                Output = new([new(0, [])]),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new(3)
            });

        public static readonly
            RequestSnapshot<ModelRequest<BatchGetEmbeddingsInput, BatchGetEmbeddingsParameters>,
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
                    Parameters = new() { TextType = "query" }
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
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeNoSse = new(
                "tokenization",
                new()
                {
                    Input = new() { Messages = [new("user", "代码改变世界")] },
                    Model = "qwen-max",
                    Parameters = new() { Seed = 1234 }
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
            RequestSnapshot<ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>,
                ModelResponse<ImageSynthesisOutput, ImageSynthesisUsage>> CreateTask = new(
                "image-synthesis",
                new()
                {
                    Model = "wanx-v1",
                    Input = new() { Prompt = "一只奔跑的猫" },
                    Parameters = new()
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
}
