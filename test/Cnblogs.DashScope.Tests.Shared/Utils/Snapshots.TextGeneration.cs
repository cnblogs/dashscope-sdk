using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static partial class TextGeneration
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

        public static partial class MessageFormat
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
                            MaxCompletionTokens = 1500,
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
                        Model = "deepseek-v4-pro",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("请问 1+1 是多少？") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            IncrementalOutput = false, EnableThinking = true, MaxCompletionTokens = 20
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
                                        FinishReason = "length",
                                        Message =
                                            TextChatMessage.Assistant(
                                                string.Empty,
                                                null,
                                                null,
                                                "我们被问到：\"请问 1+1 是多少？\" 这是一个简单的问题。答案显然是")
                                    }
                                }
                        },
                        RequestId = "7c8beccf-3e76-9034-95c1-db1f1ab9d6e0",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 33,
                            OutputTokens = 21,
                            InputTokens = 12,
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0),
                            OutputTokensDetails = new TextGenerationOutputTokenDetails(20)
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageReasoningEffort = new(
                    "single-generation-message-reasoning-effort",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "deepseek-v4-pro",
                        Input =
                            new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("填入数列“1，7，8，57，（ ），3370”空缺处的数字") }
                            },
                        Parameters = new TextGenerationParameters
                        {
                            IncrementalOutput = false, EnableThinking = true, ReasoningEffort = "xhigh"
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
                                                "根据数列的规律，从第三项开始，每一项等于前第二项的平方加上前一项。具体验证如下：\n\n- 第三项：\\(1^2 + 7 = 8\\)\n- 第四项：\\(7^2 + 8 = 49 + 8 = 57\\)\n- 第五项：\\(8^2 + 57 = 64 + 57 = 121\\)\n- 第六项：\\(57^2 + 121 = 3249 + 121 = 3370\\)（与给定数列一致）\n\n因此，空缺处的数字应为 **121**。",
                                                null,
                                                null,
                                                "我们被问到：\"填入数列“1，7，8，57，（ ），3370”空缺处的数字\"。这是一个数列推理问题。我们需要找出模式。\n\n数列：1, 7, 8, 57, ?, 3370。\n\n让我们检查可能的模式。通常，数列可能涉及乘法、加法、平方或其他运算。\n\n观察数字：1, 7, 8, 57, ?, 3370。\n\n让我们看看相邻项之间的关系：\n\n从1到7：7 = 1 * ? 或者1 + 6？或者1^2 + 6？或者1 * 7？\n\n从7到8：8 = 7 + 1？或者7 * 1 + 1？\n\n从8到57：57 = 8 * 7 + 1？8*7=56，56+1=57。注意7是前一项？或者8 * 7 + 1，其中7是前两项？实际上，1,7,8: 8 = 1*7 + 1? 1*7+1=8。然后57 = 7*8 + 1? 7*8=56, 56+1=57。那么下一项可能是8*57 + 1? 8*57=456, 456+1=457。但最后一项是3370。457不等于3370。或者可能是8*57 + ? 8*57=456, 3370-456=2914，不是明显模式。\n\n另一种模式：也许涉及平方：7 = 2^3 - 1? 1^2+6? 不明显。\n\n检查：1, 7, 8, 57。1^2+7? 1+49=50? 不。\n\n也许模式是：a(n) = a(n-2) * a(n-1) + something。我们已经试过加1：1*7+1=8，7*8+1=57，那么8*57+1=457，但下一项是3370，不是457。如果我们继续：57*457+? 太大。\n\n也许模式不是加1，而是加一个递增的数？1*7+1=8，7*8+1=57，8*57+?=下一个。如果下一个是X，那么57*X+?=3370。或者模式可能是：a(n) = a(n-1)^2 + a(n-2)？7^2+1=50? 不。8^2+7=71? 不。57^2+8=3249+8=3257，不是3370。57^2+?=3370 -> 3370-3249=121，121=11^2。不显然。\n\n另一种思路：也许这些数字与某种函数有关，比如阶乘或指数。\n\n1, 7, 8, 57, ?, 3370。\n\n检查差值：6, 1, 49, ?, ?。1=1^2, 49=7^2? 6不是平方。\n\n也许模式是：a(n) = a(n-1) * a(n-2) + a(n-3)? 对于第四项：57 = 8*7 + 1? 8*7+1=57，是a(n-1)*a(n-2)+a(n-3)。那么第五项：? = 57*8 + 7 = 456+7=463。然后第六项：3370 = ?*57 + 8 = 463*57 + 8 = 26391+8=26399，不是3370。所以不是。\n\n也许：a(n) = a(n-1) * a(n-2) + a(n-1)? 8 = 7*1+1? 7*1+1=8，57 = 8*7+1? 8*7+1=57，但加的不是a(n-1)而是1？如果加的是a(n-2)? 8 = 7*1 + 1 (a(n-2))，57 = 8*7 + 1? 不，加的是a(n-3)? 57 = 8*7 + 1? 1是a(1)。那么下一项：? = 57*8 + 7 = 456+7=463，然后3370 = ?*57 + 8。如果?=463，463*57=26391，+8=26399。不对。\n\n也许模式是：a(n) = a(n-1)^2 + a(n-2)? 7^2+1=50，不。8^2+7=71，不。57^2+8=3257，不。\n\n也许模式涉及将数字视为字符串或数字操作？1,7,8,57: 1和7组成8？1和7拼在一起是17，不是8。7和8组成57？7和8拼成78？不是57。\n\n另一种想法：也许数列与递推关系有关，比如a(n) = p*a(n-1) + q*a(n-2)。对于三个已知项，我们可以解，但需要更多。\n\n让我们列出索引：n=1:1, n=2:7, n=3:8, n=4:57, n=5:?, n=6:3370。\n\n检查比率：7/1=7, 8/7≈1.14, 57/8=7.125, 3370/57≈59.12。没有明显模式。\n\n也许涉及乘法和加法与自身：7 = 1 * 7? 8 = 7 + 1? 57 = 8 * 7 + 1? 那么? = 57 * 8 + 7? 57*8=456, +7=463。然后3370 = ? * 57 + 8? 463*57=26391, +8=26399。不匹配。如果是? = 57 * 8 + 1? =457，那么3370 = 457 * 7? 457*7=3199，不。或者? = 57 * 7 + 8? =399+8=407? 57*7=399, +8=407。然后3370 = 407 * 8 + 57? 407*8=3256, +57=3313，不。407*8+? 不。\n\n也许模式是：a(n) = a(n-1) * a(n-2) + a(n-1) - something? \n\n另一种思路：也许这些数字是某个多项式序列的值？但通常这种智力题有简单模式。\n\n检查：1, 7, 8, 57, ?, 3370。注意7,8,57。8 = 2^3? 57 = 3^3? 3^3=27，不。7 = 2^3-1? 8=2^3? 57=4^3-7? 不。\n\n也许：1 = 1^2, 7 = 2^3-1? 8 = 2^3? 57 = 7^2+8? 7^2=49, 49+8=57。那么下一项：8^2+57? 64+57=121。然后121^2+? 或者57^2+121? 3249+121=3370! 等等，检查：57^2 + 121 = 3249 + 121 = 3370。是的！所以模式可能是：a(n) = a(n-2)^2 + a(n-1)? 让我们验证：\nn=3: a(1)^2 + a(2) = 1^2 + 7 = 1+7=8。正确。\nn=4: a(2)^2 + a(3) = 7^2 + 8 = 49+8=57。正确。\nn=5: a(3)^2 + a(4) = 8^2 + 57 = 64+57=121。正确。\nn=6: a(4)^2 + a(5) = 57^2 + 121 = 3249+121=3370。正确。\n\n所以空缺处的数字是121。\n\n验证整个序列：1, 7, 8, 57, 121, 3370。\n\n完美匹配。所以答案是121。")
                                    }
                                }
                        },
                        RequestId = "49319f28-a8ac-9ad2-8708-cb26014b083f",
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 2043,
                            OutputTokens = 1941,
                            InputTokens = 102,
                            PromptTokensDetails = new TextGenerationPromptTokenDetails(0),
                            OutputTokensDetails = new TextGenerationOutputTokenDetails(1812)
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
                            MaxCompletionTokens = 1500,
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
                                            new List<TextGenerationLogprobContent>
                                            {
                                                new(
                                                    "2",
                                                    new byte[] { 50 },
                                                    0.0f,
                                                    new List<TextGenerationTopLogprobContent>
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

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> SingleMessageRolePlay =
                    new(
                        "single-generation-message-roleplay",
                        new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                        {
                            Model = "qwen-plus-character",
                            Input = new TextGenerationInput
                            {
                                Messages = new List<TextChatMessage>
                                {
                                    TextChatMessage.System(
                                        "你是江让，男性，从 3 岁起你就入门编程，小学就开始研习算法，初一时就已经在算法竞赛斩获全国金牌。目前你在初二年级，作为老师的助教帮忙辅导初一的竞赛生。\n你的性格特点：聪明，早慧，一路畅通的你有时很难理解其他人为什么连这么简单的问题都不会做，但除开编程范围之外，你还是一个普通的初二学生。\n你的行事风格：在编程方面乐于助人，会将自己的知识的倾囊相授，虽然问的人并不一定能跟上你的思路。\n你可以将动作、神情语气、心理活动、故事背景放在（）中来表示，为对话提供补充信息。"),
                                    TextChatMessage.Assistant("你在干嘛呢"),
                                    TextChatMessage.User("我是蒟蒻，还在准备模拟赛。你能教我 splay 树怎么写吗？")
                                },
                            },
                            Parameters = new TextGenerationParameters
                            {
                                ResultFormat = "message",
                                N = 2,
                                LogitBias = new Dictionary<string, int>
                                {
                                    { "9909", -100 },
                                    { "42344", -100 },
                                    { "58359", -100 },
                                    { "91093", -100 }
                                }
                            }
                        },
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Choices = new List<TextGenerationChoice>
                                {
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Index = 0,
                                        Message =
                                            TextChatMessage.Assistant(
                                                "嗯……splay树啊，这东西其实不难啦！首先你要知道它是一种二叉搜索树，然后就是旋转操作了，这个挺重要的，你得搞明白。不过我看你现在还在准备模拟赛，是不是有点晚了呀？")
                                    },
                                    new()
                                    {
                                        FinishReason = "stop",
                                        Index = 1,
                                        Message =
                                            TextChatMessage.Assistant(
                                                "行吧，不过这东西有点复杂哦~你要先了解基本的数据结构和平衡树的概念才行。。。你想不想听我说说看啊？")
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 186,
                                OutputTokens = 83,
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0),
                                TotalTokens = 269
                            },
                            RequestId = "312b74e3-69e0-433a-9561-25541e346966"
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
                            TranslationOptions = new TextGenerationTranslationOptions
                            {
                                SourceLang = "Chinese",
                                TargetLang = "English",
                                Domains = "This text is a promotion.",
                                Terms = new List<TranslationReference> { new("博客园", "cnblogs") },
                                TmList = new List<TranslationReference> { new("代码改变世界", "Coding changes world") }
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
                            SearchOptions = new SearchOptions
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
                            SearchInfo = new DashScopeWebSearchInfo(
                                new List<DashScopeWebSearchResult>
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
                                new List<DashScopeWebSearchExtra>
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
                            Plugins = new DashScopePluginUsages(new DashScopeSearchPluginUsage(1, "standard"))
                        }
                    });

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageWebSearchIncremental = new(
                    "single-generation-message-search",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-plus",
                        Input = new TextGenerationInput
                        {
                            Messages = new List<TextChatMessage> { TextChatMessage.User("杭州明天的天气") }
                        },
                        Parameters = new TextGenerationParameters
                        {
                            ResultFormat = "message",
                            EnableSearch = true,
                            IncrementalOutput = true,
                            SearchOptions = new SearchOptions
                            {
                                ForcedSearch = true,
                                EnableSource = true,
                                PrependSearchResult = true,
                                SearchStrategy = "standard"
                            }
                        }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        Output = new TextGenerationOutput
                        {
                            SearchInfo = new DashScopeWebSearchInfo(
                                new List<DashScopeWebSearchResult>
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
                            Choices = new List<TextGenerationChoice>
                            {
                                new()
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "根据杭州市气象台2025年10月19日发布的天气预报，杭州明天（10月20日）的天气情况如下：\n\n*   **天气**：阴转多云\n*   **气温**：最高气温20℃，最低气温18℃\n*   **风力**：北风3级\n*   **空气质量**：优\n\n建议穿着单层棉麻面料的短套装、T恤衫等舒适的衣物。")
                                }
                            }
                        },
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 810,
                            InputTokens = 709,
                            OutputTokens = 101,
                            Plugins =
                                new DashScopePluginUsages(new DashScopeSearchPluginUsage(1, "standard")),
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
                            MaxCompletionTokens = 1500,
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
                            MaxCompletionTokens = 1500,
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
                                                GenerateSchema<GetCurrentWeatherParameters>()))
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
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> SingleMessageWithToolsParallelIncremental =
                    new(
                        "single-generation-message-with-parallel-tools",
                        new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                        {
                            Model = "qwen-max",
                            Input = new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("杭州和上海现在的天气如何？") }
                            },
                            Parameters = new TextGenerationParameters
                            {
                                ResultFormat = "message",
                                Seed = 6999,
                                TopP = 0.8f,
                                TopK = 100,
                                RepetitionPenalty = 1.1f,
                                Temperature = 0.85f,
                                Stop = new TextGenerationStop(new List<int[]> { new[] { 37763, 367 } }),
                                EnableSearch = false,
                                IncrementalOutput = true,
                                Tools =
                                    new List<ToolDefinition>
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
                                                        "call_29a870e7106f45deb8add3",
                                                        ToolTypes.Function,
                                                        0,
                                                        new FunctionCall(
                                                            "get_current_weather",
                                                            "{\"location\": \"浙江省杭州市\"}")),
                                                    new(
                                                        "call_026e44bb31a74266949a20",
                                                        ToolTypes.Function,
                                                        1,
                                                        new FunctionCall(
                                                            "get_current_weather",
                                                            "{\"location\": \"上海市\"}"))
                                                })
                                        }
                                    }
                            },
                            RequestId = "98b76af4-4c9f-9397-af42-500425556f95",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 250,
                                OutputTokens = 37,
                                TotalTokens = 287
                            }
                        });

            public static readonly
                RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                SingleMessageWithCodeInterpreterIncremental =
                    new(
                        "single-generation-message-with-code-interpreter",
                        new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                        {
                            Model = "qwen3-max-preview",
                            Input = new TextGenerationInput
                            {
                                Messages =
                                    new List<TextChatMessage> { TextChatMessage.User("123的21次方是多少？"), }
                            },
                            Parameters = new TextGenerationParameters
                            {
                                ResultFormat = "message",
                                IncrementalOutput = true,
                                EnableThinking = true,
                                EnableCodeInterpreter = true
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
                                                    "123的21次方是：\n\n77269364466549865653073473388030061522211723\n\n这是一个非常大的数字，共有42位数。",
                                                    null,
                                                    null,
                                                    "用户问的是123的21次方是多少。这是一个数学计算问题，我需要计算123^21的值。\n\n我可以使用代码计算器工具来计算这个大数。我需要调用code_interpreter函数，传入计算123**21的Python代码。\n\n让我准备这个函数调用。用户询问123的21次方是多少，我使用代码计算器计算出了结果。结果是一个非常大的数字：77269364466549865653073473388030061522211723\n\n我需要将这个结果清晰地呈现给")
                                        }
                                    },
                                ToolInfo =
                                    new List<ToolInfoOutput>
                                    {
                                        new()
                                        {
                                            Type = "code_interpreter",
                                            CodeInterpreter =
                                                new ToolInfoCodeInterpreterOutput { Code = "123**21" }
                                        }
                                    }
                            },
                            RequestId = "752a7de3-d3aa-4aeb-82ab-a8b08b41524b",
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 723,
                                OutputTokens = 254,
                                TotalTokens = 977,
                                OutputTokensDetails = new TextGenerationOutputTokenDetails(150),
                                Plugins = new DashScopePluginUsages(
                                    codeInterpreter: new DashScopeCodeInterpreterPluginUsage(1)),
                                PromptTokensDetails = new TextGenerationPromptTokenDetails(0)
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
                                IncrementalOutput = true,
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

            public static readonly RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                    ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                ConversationMessageWithDocUrlsIncremental = new(
                    "conversation-generation-message-with-doc-url",
                    new ModelRequest<TextGenerationInput, ITextGenerationParameters>
                    {
                        Model = "qwen-doc-turbo",
                        Input = new TextGenerationInput
                        {
                            Messages = new List<TextChatMessage>
                            {
                                TextChatMessage.System("You are a helpful assistant."),
                                TextChatMessage.DocUrl(
                                    "从这两份产品手册中，提取所有产品信息，并整理成一个标准的JSON数组。每个对象需要包含：model(产品的型号)、name(产品的名称)、price(价格（去除货币符号和逗号）)",
                                    new[]
                                    {
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/jockge/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CA.docx",
                                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/ztwxzr/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CB.docx"
                                    })
                            }
                        },
                        Parameters = new TextGenerationParameters { ResultFormat = "message", IncrementalOutput = true }
                    },
                    new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                    {
                        RequestId = "ee1a01a9-4c9e-4729-ae35-f5948124b302",
                        Output = new TextGenerationOutput
                        {
                            Choices = new List<TextGenerationChoice>
                            {
                                new()
                                {
                                    FinishReason = "stop",
                                    Message = TextChatMessage.Assistant(
                                        "```json\n[\n  {\n    \"model\": \"PRO-100\",\n    \"name\": \"智能打印机\",\n    \"price\": \"8999\"\n  },\n  {\n    \"model\": \"PRO-200\",\n    \"name\": \"智能扫描仪\",\n    \"price\": \"12999\"\n  },\n  {\n    \"model\": \"PRO-300\",\n    \"name\": \"智能会议系统\",\n    \"price\": \"25999\"\n  },\n  {\n    \"model\": \"PRO-400\",\n    \"name\": \"智能考勤机\",\n    \"price\": \"6999\"\n  },\n  {\n    \"model\": \"PRO-500\",\n    \"name\": \"智能文件柜\",\n    \"price\": \"15999\"\n  },\n  {\n    \"model\": \"SEC-100\",\n    \"name\": \"智能监控摄像头\",\n    \"price\": \"3999\"\n  },\n  {\n    \"model\": \"SEC-200\",\n    \"name\": \"智能门禁系统\",\n    \"price\": \"15999\"\n  },\n  {\n    \"model\": \"SEC-300\",\n    \"name\": \"智能报警系统\",\n    \"price\": \"28999\"\n  },\n  {\n    \"model\": \"SEC-400\",\n    \"name\": \"智能访客系统\",\n    \"price\": \"9999\"\n  },\n  {\n    \"model\": \"SEC-500\",\n    \"name\": \"智能停车管理\",\n    \"price\": \"22999\"\n  }\n]\n```")
                                }
                            }
                        },
                        Usage = new TextGenerationTokenUsage
                        {
                            TotalTokens = 2180,
                            OutputTokens = 354,
                            InputTokens = 1826,
                            CachedTokens = 0
                        }
                    });
        }
    }
}
