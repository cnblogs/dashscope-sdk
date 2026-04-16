using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.More;
using Json.Schema;
using Json.Schema.Generation;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Tokenization
    {
        public static readonly
            RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TokenizationOutput, TokenizationUsage>> TokenizeNoSse = new(
                "tokenization",
                new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                {
                    Input = new TextGenerationInput
                    {
                        Messages =
                            new List<TextChatMessage> { TextChatMessage.User("代码改变世界") }.AsReadOnly()
                    },
                    Model = "qwen-max",
                    Parameters = new TextGenerationParameters { Seed = 1234 }
                },
                new ModelResponse<TokenizationOutput, TokenizationUsage>
                {
                    Output = new TokenizationOutput(
                        new List<int>
                        {
                            46100,
                            101933,
                            99489
                        },
                        new List<string>
                        {
                            "代码",
                            "改变",
                            "世界"
                        }),
                    Usage = new TokenizationUsage(3),
                    RequestId = "6615ba01-081d-9147-93ff-7bd26f3adf93"
                });
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

    public static class Upload
    {
        // get-upload-policy.response.body.txt must be CRLF
        public static readonly RequestSnapshot<DashScopeTemporaryUploadPolicy> GetPolicyNoSse = new(
            "get-upload-policy",
            new DashScopeTemporaryUploadPolicy(
                "b744f4f8-1a9c-9c6b-950d-0d327e331f2f",
                new DashScopeTemporaryUploadPolicyData(
                    "eyJleHBpcmF0aW9uIjoiMjAyNS0wNy0xMlQxMjoxMDoyNC40ODhaIiwiY29uZGl0aW9ucyI6W1siY29udGVudC1sZW5ndGgtcmFuZ2UiLDAsMTA3Mzc0MTgyNF0sWyJzdGFydHMtd2l0aCIsIiRrZXkiLCJkYXNoc2NvcGUtaW5zdGFudFwvNTJhZmUwNzdmYjQ4MjVjNmQ3NDQxMTc1OGNiMWFiOThcLzIwMjUtMDctMTJcL2I3NDRmNGY4LTFhOWMtOWM2Yi05NTBkLTBkMzI3ZTMzMWYyZiJdLHsiYnVja2V0IjoiZGFzaHNjb3BlLWZpbGUtbWdyIn0seyJ4LW9zcy1vYmplY3QtYWNsIjoicHJpdmF0ZSJ9LHsieC1vc3MtZm9yYmlkLW92ZXJ3cml0ZSI6InRydWUifV19",
                    "n3dNX/aD3+WAly0QgzsURfiIk00=",
                    "dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-07-12/b744f4f8-1a9c-9c6b-950d-0d327e331f2f",
                    "https://dashscope-file-mgr.oss-cn-beijing.aliyuncs.com",
                    300,
                    1024,
                    999999999,
                    "accessKeyId",
                    "private",
                    "true")));

        // upload-temporary-file.request.body.txt must be CRLF
        public static readonly RequestSnapshot UploadTemporaryFileNoSse = new("upload-temporary-file")
        {
            Boundary = "5aa22a67-eae4-4c54-8f62-c486fefd11a5"
        };
    }

    public static class MicrosoftExtensionsAi
    {
        public static readonly
            SdkMessageSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> ToolCallFirstRound =
                new(
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                    {
                        Model = "qwen-plus",
                        Input = new TextGenerationInput
                        {
                            Messages = new List<TextChatMessage>() { TextChatMessage.User("杭州上海现在的天气如何？"), }
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 6999,
                            MaxTokens = 1500,
                            IncrementalOutput = true,
                            Tools = new List<ToolDefinition>
                            {
                                new(
                                    "function",
                                    new FunctionDefinition(
                                        "GetWeather",
                                        string.Empty,
                                        GenerateSchema<GetCurrentWeatherParameters>().ToJsonDocument().RootElement))
                            },
                            ParallelToolCalls = true,
                            ToolChoice = ToolChoice.AutoChoice
                        }
                    },
                    new List<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>()
                    {
                        new()
                        {
                            Output = new TextGenerationOutput()
                            {
                                Choices = new List<TextGenerationChoice>()
                                {
                                    new()
                                    {
                                        Message = new TextChatMessage(
                                            "assistant",
                                            string.Empty,
                                            toolCalls: new List<ToolCall>()
                                            {
                                                new(
                                                    "call_29a870e7106f45deb8add3",
                                                    "function",
                                                    0,
                                                    new FunctionCall(
                                                        "GetWeather",
                                                        "{\"location\": \""))
                                            })
                                    }
                                }
                            },
                            RequestId = "98b76af4-4c9f-9397-af42-500425556f95",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 250,
                                OutputTokens = 16,
                                TotalTokens = 266,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                            }
                        },
                        new()
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices = new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        Message = new TextChatMessage(
                                            "assistant",
                                            string.Empty,
                                            toolCalls: new List<ToolCall>
                                            {
                                                new(
                                                    string.Empty,
                                                    "function",
                                                    0,
                                                    new FunctionCall { Arguments = "浙江省杭州市\"}" })
                                            })
                                    }
                                }
                            },
                            RequestId = "98b76af4-4c9f-9397-af42-500425556f95",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 250,
                                OutputTokens = 22,
                                TotalTokens = 272,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                            }
                        },
                        new()
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices = new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        Message = new TextChatMessage(
                                            "assistant",
                                            string.Empty,
                                            toolCalls: new List<ToolCall>
                                            {
                                                new(
                                                    "call_026e44bb31a74266949a20",
                                                    "function",
                                                    1,
                                                    new FunctionCall(
                                                        "GetWeather",
                                                        "{\"location\":"))
                                            })
                                    }
                                }
                            },
                            RequestId = "98b76af4-4c9f-9397-af42-500425556f95",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 250,
                                OutputTokens = 34,
                                TotalTokens = 284,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                            }
                        },
                        new()
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices = new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "tool_calls",
                                        Message = new TextChatMessage(
                                            "assistant",
                                            string.Empty,
                                            toolCalls: new List<ToolCall>
                                            {
                                                new(
                                                    string.Empty,
                                                    "function",
                                                    1,
                                                    new FunctionCall { Arguments = " \"上海市\"}" })
                                            })
                                    }
                                }
                            },
                            RequestId = "98b76af4-4c9f-9397-af42-500425556f95",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 250,
                                OutputTokens = 37,
                                TotalTokens = 287,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                            }
                        },
                    });

        public static readonly
            SdkMessageSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> ToolCallSecondRound =
                new(
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-plus",
                        Input = new TextGenerationInput
                        {
                            Messages =
                                new List<TextChatMessage>
                                {
                                    TextChatMessage.User("杭州上海现在的天气如何？"),
                                    TextChatMessage.Assistant(
                                        string.Empty,
                                        toolCalls: new List<ToolCall>()
                                        {
                                            new(
                                                "call_29a870e7106f45deb8add3",
                                                "function",
                                                0,
                                                new FunctionCall(
                                                    "GetWeather",
                                                    "{\"location\":\"浙江省杭州市\"}")),
                                            new(
                                                "call_026e44bb31a74266949a20",
                                                "function",
                                                1,
                                                new FunctionCall(
                                                    "GetWeather",
                                                    "{\"location\":\"上海市\"}")),
                                        }),
                                    TextChatMessage.Tool("\"大部多云\"", "call_29a870e7106f45deb8add3"),
                                    TextChatMessage.Tool("\"大部多云\"", "call_026e44bb31a74266949a20")
                                }
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            Seed = 6999,
                            MaxTokens = 1500,
                            IncrementalOutput = true,
                            Tools = new List<ToolDefinition>
                            {
                                new(
                                    "function",
                                    new FunctionDefinition(
                                        "GetWeather",
                                        string.Empty,
                                        GenerateSchema<GetCurrentWeatherParameters>().ToJsonDocument().RootElement))
                            },
                            ParallelToolCalls = true,
                            ToolChoice = ToolChoice.AutoChoice
                        }
                    },
                    new List<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>()
                    {
                        new()
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
                                                    "目前杭州和上海的天气情况如下：\n\n- **杭州**：大部多云，气温为18℃。\n- **上海**：多云转小雨，气温为19℃。\n\n请注意天气变化，出门携带雨具以防下雨。")
                                        }
                                    }
                            },
                            RequestId = "dd51401b-146e-42a0-96d9-4067a5fac75a",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 283,
                                OutputTokens = 53,
                                TotalTokens = 336,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
                            }
                        }
                    });
    }

    public static JsonSchema GenerateSchema<T>()
    {
        return new JsonSchemaBuilder().FromType<T>(
            new SchemaGeneratorConfiguration { PropertyNameResolver = PropertyNameResolvers.CamelCase }).Build();
    }
}
