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
                SingleMessageWebSearchNoSse = new(
                    "single-generation-message-search",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-plus",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("阿里股价") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            EnableSearch = true,
                            SearchOptions = new TextGenerationSearchOptions()
                            {
                                EnableSource = true,
                                EnableCitation = true,
                                EnableSearchExtension = true,
                                CitationFormat = "[ref_<number>]",
                                ForcedSearch = true,
                                SearchStrategy = "standard",
                            }
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
                                            "截至2025年10月17日，阿里巴巴美股（BABA）的实时价格为167.05美元，较上个交易日收盘价165.09美元上涨1.19%[根据权威渠道的实时信息]。\n\n近期，多家券商上调了对阿里巴巴的目标股价。其中，摩根大通在2025年10月1日将阿里巴巴美股的目标价由170美元大幅上调至245美元，这是目前外资机构中的最高预测[ref_1][ref_3]。此外，大和证券、瑞银、花旗、高盛、摩根士丹利等也纷纷上调目标价并维持“买入”或类似评级[ref_1]。\n\n从市场表现来看，阿里巴巴股价在近期有所波动。例如，在2025年10月初，其美股价格一度接近189美元，随后有所回落[根据权威渠道的实时信息]。与此同时，港股方面，截至2025年10月3日收盘，阿里巴巴-SW（09988）报185.100港元，上涨2.000港元，涨幅1.09%[ref_2]。"),
                                    }
                                },
                            SearchInfo = new TextGenerationWebSearchInfo(
                                new List<TextGenerationWebSearchResult>()
                                {
                                    new(
                                        "无",
                                        "https://b.bdstatic.com/searchbox/mappconsole/image/20190805/1239163c-77cc-449e-b91f-9bb1d27e43a7.png",
                                        1,
                                        "各大券商不断调高 阿里 预期股价!1. 摩根大通:2025年10月1日,摩根大通发布报告将 阿里巴巴 (BABA.US)... - 雪球",
                                        "https://xueqiu.com/1692213155/355441155"),
                                    new(
                                        "无",
                                        "https://img.alicdn.com/imgextra/i3/O1CN0143d0Wi1XYHQYtbqJI_!!6000000002935-55-tps-32-32.svg",
                                        2,
                                        "阿里巴巴-SW(09988)_个股概览_股票价格_实时行情_走势图_新闻资讯_股评_财报_FinScope-AI让投资更简单",
                                        "https://gushitong.baidu.com/stock/hk-09988?code=09988&financeType=stock&market=hk&name=阿里巴巴-SW&subTab=2"),
                                    new(
                                        "无",
                                        "https://b.bdstatic.com/searchbox/mappconsole/image/20190805/1239163c-77cc-449e-b91f-9bb1d27e43a7.png",
                                        3,
                                        "截至目前为止,外资机构对",
                                        "https://xueqiu.com/9216592857/355356488"),
                                    new(
                                        "阿里巴巴集团",
                                        "https://static.alibabagroup.com/static/favicon.ico",
                                        4,
                                        "阿里巴巴投资者关系-阿里巴巴集团",
                                        "https://www.alibabagroup.com/redirect?path=/cn/ir/home"),
                                    new(
                                        "阿里巴巴集团",
                                        "https://static.alibabagroup.com/static/favicon.ico",
                                        5,
                                        "阿里巴巴投資者關係-阿里巴巴集團",
                                        "https://www.alibabagroup.com/zh-HK/investor-relations")
                                },
                                new List<TextGenerationWebSearchExtra>()
                                {
                                    new(
                                        "阿里巴巴美股：\n实时价格167.05USD\n上个交易日收盘价165.09USD\n日环比%1.19%\n月环比%-6.53\n日同比%66.33\n月同比%74.05\n历史价格列表[{\"date\":\"2025-10-17\",\"endPri\":\"167.050\"},{\"date\":\"2025-10-16\",\"endPri\":\"165.090\"},{\"date\":\"2025-10-15\",\"endPri\":\"165.910\"},{\"date\":\"2025-10-14\",\"endPri\":\"162.860\"},{\"date\":\"2025-10-13\",\"endPri\":\"166.810\"},{\"date\":\"2025-10-10\",\"endPri\":\"159.010\"},{\"date\":\"2025-10-09\",\"endPri\":\"173.680\"},{\"date\":\"2025-10-08\",\"endPri\":\"181.120\"},{\"date\":\"2025-10-07\",\"endPri\":\"181.330\"},{\"date\":\"2025-10-06\",\"endPri\":\"187.220\"},{\"date\":\"2025-10-03\",\"endPri\":\"188.030\"},{\"date\":\"2025-10-02\",\"endPri\":\"189.340\"},{\"date\":\"2025-10-01\",\"endPri\":\"182.780\"},{\"date\":\"2025-09-30\",\"endPri\":\"178.730\"},{\"date\":\"2025-09-29\",\"endPri\":\"179.900\"},{\"date\":\"2025-09-26\",\"endPri\":\"171.910\"},{\"date\":\"2025-09-25\",\"endPri\":\"175.470\"},{\"date\":\"2025-09-24\",\"endPri\":\"176.440\"},{\"date\":\"2025-09-23\",\"endPri\":\"163.080\"},{\"date\":\"2025-09-22\",\"endPri\":\"164.250\"},{\"date\":\"2025-09-19\",\"endPri\":\"162.810\"},{\"date\":\"2025-09-18\",\"endPri\":\"162.480\"},{\"date\":\"2025-09-17\",\"endPri\":\"166.170\"},{\"date\":\"2025-09-16\",\"endPri\":\"162.210\"},{\"date\":\"2025-09-15\",\"endPri\":\"158.040\"},{\"date\":\"2025-09-12\",\"endPri\":\"155.060\"},{\"date\":\"2025-09-11\",\"endPri\":\"155.440\"},{\"date\":\"2025-09-10\",\"endPri\":\"143.930\"},{\"date\":\"2025-09-09\",\"endPri\":\"147.100\"},{\"date\":\"2025-09-08\",\"endPri\":\"141.200\"}]\n\n",
                                        "stock")
                                })
                        },
                        RequestId = "cc2b017d-df02-4ad6-8942-858ad10a3f8a",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 2973,
                            OutputTokens = 266,
                            InputTokens = 2707,
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0),
                            Plugins = new TextGenerationPluginUsages(new TextGenerationSearchPluginUsage(1, "standard"))
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageWebSearchIncremental = new(
                    "single-generation-message-search",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>()
                    {
                        Model = "qwen-plus",
                        Input = new TextGenerationInput()
                        {
                            Messages = new List<TextChatMessage>() { TextChatMessage.User("杭州明天的天气") }
                        },
                        Parameters = new TextGenerationParameters()
                        {
                            ResultFormat = "message",
                            EnableSearch = true,
                            IncrementalOutput = true,
                            SearchOptions = new TextGenerationSearchOptions()
                            {
                                ForcedSearch = true,
                                EnableSource = true,
                                PrependSearchResult = true,
                                SearchStrategy = "standard"
                            }
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>()
                    {
                        Output = new TextGenerationOutput()
                        {
                            SearchInfo = new TextGenerationWebSearchInfo(
                                new List<TextGenerationWebSearchResult>()
                                {
                                    new(
                                        "厦门时空科技有限公司",
                                        "http://www.ip.cn/favicon.ico",
                                        1,
                                        "杭州市15天天气查询",
                                        "https://www.ip.cn/tianqi/zhejiang/hangzhou/15day.html"),
                                    new(
                                        "eastday",
                                        "https://img.alicdn.com/imgextra/i3/O1CN01kr9teP1wlRD8OH6TO_!!6000000006348-73-tps-16-16.ico",
                                        2,
                                        "杭州天气预报杭州2025年10月20日天气",
                                        "https://tianqi.eastday.com/tianqi/hangzhou/20251020.html"),
                                    new(
                                        "厦门时空科技有限公司",
                                        "http://www.ip.cn/favicon.ico",
                                        3,
                                        "杭州市2025年10月份天气查询",
                                        "https://www.ip.cn/tianqi/zhejiang/hangzhou/202510.html"),
                                    new(
                                        "无",
                                        string.Empty,
                                        4,
                                        "杭州",
                                        "http://www.suzhoutianqi114.com/hangzhou/10yuefen.html"),
                                    new(
                                        "eastday",
                                        "https://img.alicdn.com/imgextra/i3/O1CN01kr9teP1wlRD8OH6TO_!!6000000006348-73-tps-16-16.ico",
                                        5,
                                        ">杭州历史天气 ",
                                        "https://tianqi.eastday.com/lishi/hangzhou.html"),
                                },
                                null),
                            Choices = new List<TextGenerationChoice>()
                            {
                                new()
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "根据杭州市气象台2025年10月19日发布的天气预报，杭州明天（10月20日）的天气情况如下：\n\n*   **天气**：阴转多云\n*   **气温**：最高气温20℃，最低气温18℃\n*   **风力**：北风3级\n*   **空气质量**：优\n\n建议穿着单层棉麻面料的短套装、T恤衫等舒适的衣物。")
                                }
                            }
                        },
                        Usage = new TextGenerationTokenUsage()
                        {
                            TotalTokens = 810,
                            InputTokens = 709,
                            OutputTokens = 101,
                            Plugins =
                                new TextGenerationPluginUsages(new TextGenerationSearchPluginUsage(1, "standard")),
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
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
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> SingleMessageWithToolsIncremental =
                    new(
                        "single-generation-message-with-tools",
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
                                                    "call_cec4c19d27624537b583af",
                                                    "function",
                                                    0,
                                                    new FunctionCall(
                                                        "get_current_weather",
                                                        "{\"location\": \"浙江省杭州市\"}")),
                                                new(
                                                    "call_dxjdop3d27624537b583af",
                                                    "function",
                                                    1,
                                                    new FunctionCall(
                                                        "get_current_weather",
                                                        "{\"location\": \"上海市\"}")),
                                            }),
                                        TextChatMessage.Tool("浙江省杭州市 大部多云，摄氏 18 度", "call_cec4c19d27624537b583af"),
                                        TextChatMessage.Tool("上海市 多云转小雨，摄氏 19 度", "call_dxjdop3d27624537b583af")
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
                                ParallelToolCalls = true
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
                                            Message = TextChatMessage.Assistant("目前杭州和上海的天气情况如下：\n\n- **杭州**：大部多云，气温为18℃。\n- **上海**：多云转小雨，气温为19℃。\n\n请注意天气变化，出门携带雨具以防下雨。")
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
