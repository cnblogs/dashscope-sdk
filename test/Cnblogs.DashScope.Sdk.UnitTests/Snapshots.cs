namespace Cnblogs.DashScope.Sdk.UnitTests;

public static class Snapshots
{
    public static class Error
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            AuthError = new(
                "auth-error",
                new ModelRequest<TextGenerationInput, TextGenerationParameters>
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
                new DashScopeError
                {
                    Code = "InvalidApiKey",
                    Message = "No API-key provided.",
                    RequestId = "862e8e7a-1fb8-9a50-aa7b-a808c2a988ee"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            ParameterError = new(
                "parameter-error",
                new ModelRequest<TextGenerationInput, TextGenerationParameters>
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
                new DashScopeError
                {
                    Code = "InvalidParameter",
                    Message = "Role must be user or assistant and Content length must be greater than 0",
                    RequestId = "a5898c04-d210-901b-965f-e4bd90478805"
                });

        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>, DashScopeError>
            ParameterErrorSse = new(
                "parameter-error",
                new ModelRequest<TextGenerationInput, TextGenerationParameters>
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
                new DashScopeError
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
                    new ModelRequest<TextGenerationInput, TextGenerationParameters>
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SinglePromptIncremental = new(
                    "single-generation-text",
                    new ModelRequest<TextGenerationInput, TextGenerationParameters>
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
                        Output = new TextGenerationOutput
                        {
                            FinishReason = "stop", Text = "1+1 等于 2。这是最基本的数学加法原则，在十进制数系统中，任何两个相同的数字相加都等于该数字的两倍。"
                        },
                        RequestId = "893a2304-f032-9c7f-bde8-da5e3c1288fc",
                        Usage = new TextGenerationTokenUsage
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
                    new ModelRequest<TextGenerationInput, TextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [new("user", "请问 1+1 是多少？")] },
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
                                    Message = new(
                                        "assistant",
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageIncremental = new(
                    "single-generation-message",
                    new ModelRequest<TextGenerationInput, TextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput { Messages = [new("user", "请问 1+1 是多少？")] },
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
                                    Message = new(
                                        "assistant",
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, TextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationMessageIncremental = new(
                    "conversation-generation-message",
                    new ModelRequest<TextGenerationInput, TextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                [
                                    new ChatMessage("user", "现在请你记住一个数字，42"),
                                    new ChatMessage("assistant", "好的，我已经记住了这个数字。"),
                                    new ChatMessage("user", "请问我刚才提到的数字是多少？")
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
                                    FinishReason = "stop", Message = new ChatMessage("assistant", "您刚才提到的数字是42。")
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
        }
    }

    public static class MultimodalGeneration
    {
        public static readonly RequestSnapshot<ModelRequest<MultimodalInput, MultimodalParameters>,
            ModelResponse<MultimodalOutput, MultimodalTokenUsage>> VlNoSse =
            new(
                "multimodal-generation-vl",
                new ModelRequest<MultimodalInput, MultimodalParameters>()
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput()
                    {
                        Messages =
                        [
                            new MultimodalMessage(
                                "system",
                                [new MultimodalMessageContent(null, "You are a helpful assistant.")]),
                            new MultimodalMessage(
                                "user",
                                [
                                    new MultimodalMessageContent(
                                        "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                    new MultimodalMessageContent(null, "这个图片是哪里，请用简短的语言回答")
                                ])
                        ]
                    },
                    Parameters = new MultimodalParameters()
                    {
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            new MultimodalMessage(
                                "assistant",
                                [
                                    new MultimodalMessageContent(
                                        null,
                                        "这张照片显示的是海滩景色，但是无法确定具体的位置信息。图中有一名女子和一只狗在沙滩上互动。背景中有海洋和日出或日落的光线。由于缺乏特定地标或者细节特征，仅凭此图像很难精确识别具体的地点。")
                                ]))
                    ]),
                    RequestId = "a2a5f2e6-c6d7-9e04-9f92-1d3eee274198",
                    Usage = new MultimodalTokenUsage()
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
                new ModelRequest<MultimodalInput, MultimodalParameters>()
                {
                    Model = "qwen-vl-plus",
                    Input = new MultimodalInput()
                    {
                        Messages =
                        [
                            new MultimodalMessage(
                                "system",
                                [new MultimodalMessageContent(null, "You are a helpful assistant.")]),
                            new MultimodalMessage(
                                "user",
                                [
                                    new MultimodalMessageContent(
                                        "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"),
                                    new MultimodalMessageContent(null, "这个图片是哪里，请用简短的语言回答")
                                ])
                        ]
                    },
                    Parameters = new MultimodalParameters()
                    {
                        IncrementalOutput = true,
                        Seed = 1234,
                        TopK = 100,
                        TopP = 0.81f
                    }
                },
                new ModelResponse<MultimodalOutput, MultimodalTokenUsage>()
                {
                    Output = new MultimodalOutput(
                    [
                        new MultimodalChoice(
                            "stop",
                            new MultimodalMessage(
                                "assistant",
                                [
                                    new MultimodalMessageContent(
                                        null,
                                        "这张照片显示的是海滩景色，但是无法确定具体的位置信息。图中有一名女子和一只狗在沙滩上互动。背景中有海洋和夕阳的余晖照耀着天空。请注意这是一张插画风格的艺术作品，并非实际的照片或新闻内容。")
                                ]))
                    ]),
                    RequestId = "81001ee4-6155-9d17-8533-195f61c8f036",
                    Usage = new MultimodalTokenUsage()
                    {
                        OutputTokens = 58,
                        InputTokens = 1284,
                        ImageTokens = 1247
                    }
                });
    }

    public static class TextEmbedding
    {
        public static readonly RequestSnapshot<ModelRequest<TextEmbeddingInput, TextEmbeddingParameters>,
            ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>> NoSse = new(
            "text-embedding",
            new ModelRequest<TextEmbeddingInput, TextEmbeddingParameters>()
            {
                Input = new TextEmbeddingInput { Texts = ["代码改变世界"] },
                Model = "text-embedding-v2",
                Parameters = new TextEmbeddingParameters() { TextType = "query" }
            },
            new ModelResponse<TextEmbeddingOutput, TextEmbeddingTokenUsage>
            {
                Output = new TextEmbeddingOutput([new TextEmbeddingItem(0, [])]),
                RequestId = "1773f7b2-2148-9f74-b335-b413e398a116",
                Usage = new TextEmbeddingTokenUsage(3)
            });
    }
}
