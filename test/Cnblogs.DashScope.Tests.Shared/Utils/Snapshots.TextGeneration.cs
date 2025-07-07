using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.Schema;
using Json.Schema.Generation;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
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
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output =
                            new TextGenerationOutput { FinishReason = "stop", Text = "1+1等于2。这是最基本的数学加法运算之一。" },
                        RequestId = "7e3d5586-cb70-98ce-97bf-8a2ac0091c3f",
                        Usage = new TextGenerationTokenUsage
                        {
                            InputTokens = 16,
                            OutputTokens = 14,
                            TotalTokens = 30,
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
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
                            Stop = new[] { new[] { 37763, 367 } },
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
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
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
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何两个相同的数字相加都等于该数字的二倍。")
                                    }
                                }
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
                SingleMessageReasoning = new(
                    "single-generation-message-reasoning",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "deepseek-r1",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            IncrementalOutput = false, ResultFormat = ResultFormats.Message
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message =
                                            TextChatMessage.Assistant(
                                                "1 + 1 等于 **2**。这是基础的算术加法，当我们将一个单位与另一个单位相加时，总和为两个单位。",
                                                null,
                                                null,
                                                "嗯，用户问1加1等于多少。这个问题看起来很简单，但可能有一些需要注意的地方。首先，我得确认用户是不是真的在问基本的数学问题，还是有其他的意图，比如测试我的反应或者开玩笑。\n\n1加1在基础算术里确实是2，但有时候可能会有不同的解释，比如在二进制中1+1等于10，或者在逻辑学中有时候表示为1，如果是布尔代数的话。不过通常情况下，用户可能只需要最直接的答案，也就是2。\n\n不过也有可能用户想考察我是否能够处理更复杂的情况，或者是否有隐藏的意思。比如，在某些情况下，1加1可能被用来比喻合作的效果，比如“1+1大于2”，但这可能超出了当前问题的范围。\n\n我需要考虑用户的背景。如果用户是小学生，那么直接回答2是正确的，并且可能需要鼓励的话。如果是成年人，可能还是同样的答案，但不需要额外的解释。如果用户来自数学或计算机领域，可能需要确认是否需要其他进制的答案，但通常默认是十进制。\n\n另外，检查是否有拼写错误或非阿拉伯数字的情况，比如罗马数字的I+I，但问题里明确写的是1+1，所以应该是阿拉伯数字。\n\n总结下来，最安全也是最正确的答案就是2。不过为了确保，可以简短地确认用户的意图，但按照常规问题处理，直接回答即可。")
                                    }
                                }
                        },
                        RequestId = "7039d8ff-89e0-9191-b4d3-0d258a7d70e1",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 313,
                            OutputTokens = 302,
                            InputTokens = 11
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
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
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
                            ToolChoice = ToolChoice.AutoChoice
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何两个相同的数字相加都等于该数字的二倍。")
                                    }
                                }
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
                SingleMessageLogprobs = new(
                    "single-generation-message-logprobs",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？请直接输出结果") }
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
                            Logprobs = true,
                            TopLogprobs = 2
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant("2"),
                                        Logprobs = new TextGenerationLogprobs(
                                            new List<TextGenerationLogprobContent>()
                                            {
                                                new(
                                                    "2",
                                                    new byte[] { 50 },
                                                    0.0f,
                                                    new List<TextGenerationTopLogprobContent>()
                                                    {
                                                        new("2", new byte[] { 50 }, 0.0f)
                                                    }),
                                            })
                                    }
                                }
                        },
                        RequestId = "1d881da5-0028-9f20-8e7f-6bc7ae891c54",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 21,
                            OutputTokens = 1,
                            InputTokens = 20,
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageTranslation = new(
                    "single-generation-message-translation",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-mt-plus",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("博客园的理念是代码改变世界") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            IncrementalOutput = false,
                            TranslationOptions = new TextGenerationTranslationOptions()
                            {
                                SourceLang = "Chinese",
                                TargetLang = "English",
                                Domains = "This text is a promotion.",
                                Terms = new List<TranslationReference>() { new("博客园", "cnblogs") },
                                TmList = new List<TranslationReference>() { new("代码改变世界", "Coding changes world") }
                            }
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            FinishReason = "stop",
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "The concept of cnblogs is that coding changes world "),
                                    }
                                }
                        },
                        RequestId = "bf86e0f9-a8a2-9b32-be8d-ea3cae47c8ea",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 122,
                            OutputTokens = 11,
                            InputTokens = 111,
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
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？用 JSON 格式输出。") }
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
                            Stop = new[] { new[] { 37763, 367 } },
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
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant("{\n  \"result\": 2\n}")
                                    }
                                }
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
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
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
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下 1 加上另一个 1 的结果都是 2。")
                                    }
                                }
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
                SingleMessageReasoningIncremental = new(
                    "single-generation-message-reasoning",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-plus-latest",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            IncrementalOutput = true,
                            ResultFormat = ResultFormats.Message,
                            EnableThinking = true,
                            ThinkingBudget = 10
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "1+1 等于 **2**。这是数学中最基本的加法运算之一。\n\n如果你有其他关于数学、科学或任何领域的问题，欢迎继续提问！😊",
                                            null,
                                            null,
                                            "嗯，用户问的是“1+1是多少")
                                    }
                                }
                        },
                        RequestId = "ab9f3446-9bbf-963e-9754-2d6543343d7e",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 69,
                            OutputTokens = 53,
                            InputTokens = 16,
                            OutputTokensDetails = new TextGenerationOutputTokenDetails(ReasoningTokens: 10)
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
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
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
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "1+1 等于 2。这是最基本的数学加法之一，在十进制计数体系中，任何情况下 1 加上另一个 1 的结果都是 2。")
                                    }
                                }
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
                            Input = new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("杭州现在的天气如何？") }
                            },
                            Parameters = new TextGenerationParameters
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
                                    new List<ToolDefinition>
                                    {
                                        new(
                                            "function",
                                            new FunctionDefinition(
                                                "get_current_weather",
                                                "获取现在的天气",
                                                new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>(
                                                        new SchemaGeneratorConfiguration
                                                        {
                                                            PropertyNameResolver =
                                                                PropertyNameResolvers.LowerSnakeCase
                                                        })
                                                    .Build()))
                                    },
                                ToolChoice = ToolChoice.FunctionChoice("get_current_weather")
                            }
                        },
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices =
                                    new List<TextGenerationChoice>
                                    {
                                        new()
                                        {
                                            FinishReason = "stop",
                                            Message = TextChatMessage.Assistant(
                                                string.Empty,
                                                toolCalls:
                                                new List<ToolCall>
                                                {
                                                    new(
                                                        "call_cec4c19d27624537b583af",
                                                        ToolTypes.Function,
                                                        0,
                                                        new FunctionCall(
                                                            "get_current_weather",
                                                            "{\"location\": \"浙江省杭州市\"}"))
                                                })
                                        }
                                    }
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
                            Input = new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("杭州现在的天气如何？") }
                            },
                            Parameters = new TextGenerationParameters
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
                                    new List<ToolDefinition>
                                    {
                                        new(
                                            "function",
                                            new FunctionDefinition(
                                                "get_current_weather",
                                                "获取现在的天气",
                                                new JsonSchemaBuilder().FromType<GetCurrentWeatherParameters>(
                                                        new SchemaGeneratorConfiguration
                                                        {
                                                            PropertyNameResolver =
                                                                PropertyNameResolvers.LowerSnakeCase
                                                        })
                                                    .Build()))
                                    },
                                ToolChoice = ToolChoice.FunctionChoice("get_current_weather")
                            }
                        },
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices =
                                    new List<TextGenerationChoice>
                                    {
                                        new()
                                        {
                                            FinishReason = "stop",
                                            Message = TextChatMessage.Assistant(
                                                string.Empty,
                                                toolCalls:
                                                new List<ToolCall>
                                                {
                                                    new(
                                                        "call_cec4c19d27624537b583af",
                                                        ToolTypes.Function,
                                                        0,
                                                        new FunctionCall(
                                                            "get_current_weather",
                                                            "{\"location\": \"浙江省杭州市\"}"))
                                                })
                                        }
                                    }
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
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-max",
                        Input = new TextGenerationInput
                        {
                            Messages =
                                new List<TextChatMessage>
                                {
                                    TextChatMessage.User("请对“春天来了，大地”这句话进行续写，来表达春天的美好和作者的喜悦之情"),
                                    TextChatMessage.Assistant("春天来了，大地", true)
                                }
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = ResultFormats.Message,
                            Seed = 1234,
                            TopP = 0.8f,
                            TopK = 100,
                            RepetitionPenalty = 1.1f,
                            Temperature = 0.85f,
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        RequestId = "4c45d7fd-3158-9ff4-96a0-6e92c710df2c",
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message =
                                            TextChatMessage.Assistant(
                                                "仿佛从漫长的冬眠中苏醒过来，万物复苏。嫩绿的小草悄悄地探出了头，争先恐后地想要沐浴在温暖的阳光下；五彩斑斓的花朵也不甘示弱，竞相绽放着自己最美丽的姿态，将田野、山林装扮得分外妖娆。微风轻轻吹过，带来了泥土的气息与花香混合的独特香味，让人心旷神怡。小鸟们开始忙碌起来，在枝头欢快地歌唱，似乎也在庆祝这个充满希望的新季节的到来。这一切美好景象不仅让人感受到了大自然的魅力所在，更激发了人们对生活无限热爱和向往的心情。")
                                    }
                                }
                        },
                        Usage = new TextGenerationTokenUsage
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
                                    new List<TextChatMessage>
                                    {
                                        TextChatMessage.User("现在请你记住一个数字，42"),
                                        TextChatMessage.Assistant("好的，我已经记住了这个数字。"),
                                        TextChatMessage.User("请问我刚才提到的数字是多少？")
                                    }
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
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant("您刚才提到的数字是42。")
                                    }
                                }
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
                                    new List<TextChatMessage>
                                    {
                                        TextChatMessage.File(
                                            new List<DashScopeFileId>
                                            {
                                                "file-fe-WTTG89tIUTd4ByqP3K48R3bn",
                                                "file-fe-l92iyRvJm9vHCCfonLckf1o2"
                                            }),
                                        TextChatMessage.User("这两个文件是相同的吗？")
                                    }
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
                            Stop = new[] { new[] { 37763, 367 } },
                            EnableSearch = false,
                            IncrementalOutput = true
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            Choices =
                                new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Message = TextChatMessage.Assistant(
                                            "你上传的两个文件并不相同。第一个文件`test1.txt`包含两行文本，每行都是“测试”。而第二个文件`test2.txt`只有一行文本，“测试2”。尽管它们都含有“测试”这个词，但具体内容和结构不同。")
                                    }
                                }
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
}
