using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;
using Json.More;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
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

        public static readonly
            SdkMessageSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> MultimodalToolCallFirstRound =
                new(
                    new ModelRequest<MultimodalInput, IMultimodalParameters>()
                    {
                        Model = "qwen3.6-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new List<MultimodalMessage>()
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>()
                                    {
                                        MultimodalMessageContent.TextContent("杭州上海现在的天气如何")
                                    })
                            }
                        },
                        Parameters = new MultimodalParameters()
                        {
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
                    new List<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
                    {
                        new()
                        {
                            Output =
                                MultimodalAssistantReply(
                                    "用户现在需要先获取杭州和上海现在的天气，才能基于天气编故事。所以第一步要调用get_current_weather工具，分别获取杭州和上海的天气。先处理杭州的，参数location设为“浙江省杭州市”。"),
                            RequestId = "0d95e615-418d-97a1-9302-ff622a7ceead",
                            Usage = new MultimodalTokenUsage()
                            {
                                InputTokensDetails =
                                    new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 304),
                                ImageTokens = 2503,
                                InputTokens = 2807,
                                OutputTokens = 50,
                                TotalTokens = 2857,
                                OutputTokensDetails = new MultimodalOutputTokenDetails(38, 12),
                                PromptTokensDetails = new MultimodalPromptTokenDetails()
                            }
                        },
                        new()
                        {
                            Output = MultimodalAssistantReply(
                                toolCalls: new List<ToolCall>
                                {
                                    new(
                                        "call_aa99ad078f294a3d81da41d7",
                                        "function",
                                        0,
                                        new FunctionCall(
                                            "GetWeather",
                                            "{\"location\": \"浙江省杭州市\"}")),
                                    new(
                                        "call_aa8b6311567847e197e6ca7f",
                                        "function",
                                        1,
                                        new FunctionCall(
                                            "GetWeather",
                                            "{\"location\": \"上海市\"}"))
                                },
                                finishReason: "tool_calls"),
                            RequestId = "0d95e615-418d-97a1-9302-ff622a7ceead",
                            Usage = new MultimodalTokenUsage()
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
                        },
                    });

        public static readonly
            SdkMessageSnapshot<ModelRequest<MultimodalInput, IMultimodalParameters>,
                ModelResponse<MultimodalOutput, MultimodalTokenUsage>> MultimodalToolCallSecondRound =
                new(
                    new ModelRequest<MultimodalInput, IMultimodalParameters>()
                    {
                        Model = "qwen3.6-plus",
                        Input = new MultimodalInput
                        {
                            Messages = new List<MultimodalMessage>()
                            {
                                MultimodalMessage.User(
                                    new List<MultimodalMessageContent>()
                                    {
                                        MultimodalMessageContent.TextContent("杭州上海现在的天气如何")
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
                                                "GetWeather",
                                                "{\"location\":\"浙江省杭州市\"}")),
                                        new(
                                            "call_aa8b6311567847e197e6ca7f",
                                            "function",
                                            1,
                                            new FunctionCall(
                                                "GetWeather",
                                                "{\"location\":\"上海市\"}"))
                                    }),
                                MultimodalMessage.Tool(
                                    new List<MultimodalMessageContent>
                                    {
                                        MultimodalMessageContent.TextContent("\"大部多云\"")
                                    },
                                    "call_aa99ad078f294a3d81da41d7"),
                                MultimodalMessage.Tool(
                                    new List<MultimodalMessageContent> { MultimodalMessageContent.TextContent("\"大部多云\"") },
                                    "call_aa8b6311567847e197e6ca7f")
                            }
                        },
                        Parameters = new MultimodalParameters()
                        {
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
                    new List<ModelResponse<MultimodalOutput, MultimodalTokenUsage>>
                    {
                        new()
                        {
                            Output = MultimodalAssistantReply(
                                "用户需要编写一个基于图片的小故事，参考杭州和上海的天气，并且字数限制在 100 字以内。\n\n1.  **整合信息**：\n    *   **图片内容**：海边、沙滩、夕阳（或清晨的阳光）、金毛犬、年轻女性、握手动作、轻松愉快。\n    *   **天气信息**：杭州是“大部多云”，上海是“晴”。\n    *   **关联逻辑**：图片中的场景明显是海边（沙滩），而上海离海比较近（虽然图片不一定是在上海拍的，但在地理认知上上海和海边关联性更强，且上海是晴天，与图片中阳光明媚的氛围更吻合）。杭州是多云，可以作为故事的背景对比，比如“逃离”多云的杭州去上海看海。\n\n2.  **构思情节**：\n    *   主角：图中的女孩和金毛。\n    *   起因：杭州天气多云，有些沉闷，或者只是想去海边玩。\n    *   经过：带着金毛去了上海（或者在上海的海边），因为上海是晴天。\n    *   高潮/结局：在上海的金色沙滩上，享受阳光，和金毛握手互动，非常开心。\n\n3.  **草拟文本 (Draft 1)**：\n    杭州的大部多云让人有些慵懒，我们便驱车前往上海，去追逐那里的晴朗。当双脚踩上上海温热的沙滩，金色的阳光正好洒下。金毛犬兴奋地举起爪子和我握手，海风轻拂，这惬意的午后，是属于我们最好的时光。\n\n4.  **修改与润色 (Draft 2 - 更加精炼，强调天气对比)**：\n    告别杭州的多云，我们一路向东，奔赴上海的晴朗。金色的阳光洒满上海的海滩，海风温暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”。此刻没有城市的喧嚣，只有我们和这治愈的阳光，这就是最完美的周末。\n\n5.  **字数检查**：\n    “告别杭州的多云，我们一路向东，奔赴上海的晴朗。金色的阳光洒满上海的海滩，海风温暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”。此刻没有城市的喧嚣，只有我们和这治愈的阳光，这就是最完美的周末。” -> 大约 85 字。\n\n6.  **最终定稿**：\n    告别杭州的多云，我们驱车奔赴上海的晴朗。金色的夕阳洒满海滩，海风微暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”击掌。此刻远离喧嚣，只有我们和治愈的阳光，这就是"),
                            RequestId = "87e6587a-71a3-98ba-89a3-23151746e292",
                            Usage = new MultimodalTokenUsage()
                            {
                                InputTokensDetails =
                                    new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 388),
                                ImageTokens = 2503,
                                InputTokens = 2891,
                                OutputTokens = 556,
                                TotalTokens = 3447,
                                OutputTokensDetails = new MultimodalOutputTokenDetails(556, 0),
                                PromptTokensDetails = new MultimodalPromptTokenDetails()
                            }
                        },
                        new()
                        {
                            Output = MultimodalAssistantReply("最完美的周末。", "告别杭州"),
                            RequestId = "87e6587a-71a3-98ba-89a3-23151746e292",
                            Usage = new MultimodalTokenUsage()
                            {
                                InputTokensDetails =
                                    new MultimodalInputTokenDetails(ImageTokens: 2503, TextTokens: 388),
                                ImageTokens = 2503,
                                InputTokens = 2891,
                                OutputTokens = 565,
                                TotalTokens = 3456,
                                OutputTokensDetails = new MultimodalOutputTokenDetails(556, 9),
                                PromptTokensDetails = new MultimodalPromptTokenDetails()
                            }
                        },
                        new()
                        {
                            Output = MultimodalAssistantReply(text: "的多云，我们驱车奔赴上海的晴朗。金色的阳光洒满海滩，海风微暖。我坐在沙滩上，看着金毛兴奋地举起爪子与我“握手”击掌。此刻远离喧嚣，只有我们和治愈的暖阳，这就是最完美的周末。"),
                            RequestId = "87e6587a-71a3-98ba-89a3-23151746e292",
                            Usage = new MultimodalTokenUsage()
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
                        },
                    });

        private static MultimodalOutput MultimodalAssistantReply(
            string? reasoning = null,
            string? text = null,
            List<ToolCall>? toolCalls = null,
            string finishReason = "null")
        {
            var contents = new List<MultimodalMessageContent>();
            if (text != null)
            {
                contents.Add(MultimodalMessageContent.TextContent(text));
            }

            return new MultimodalOutput(
                new List<MultimodalChoice>
                {
                    new(finishReason, MultimodalMessage.Assistant(contents, reasoning, toolCalls))
                });
        }
    }
}
