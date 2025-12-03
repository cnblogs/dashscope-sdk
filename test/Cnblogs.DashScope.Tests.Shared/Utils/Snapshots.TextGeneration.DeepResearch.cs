using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public static partial class Snapshots
    {
        public static partial class TextGeneration
        {
            public static partial class MessageFormat
            {
                private static readonly ModelRequest<TextGenerationInput, ITextGenerationParameters> DeepResearchRequest =
                    new()
                    {
                        Model = "qwen-deep-research",
                        Input = new TextGenerationInput
                        {
                            Messages = new List<TextChatMessage>
                            {
                                TextChatMessage.User("研究一下人工智能在教育中的应用"),
                                TextChatMessage.Assistant("请告诉我您希望重点研究人工智能在教育中的哪些具体应用场景？"),
                                TextChatMessage.User("我主要关注个性化学习方面")
                            }
                        },
                        Parameters = new TextGenerationParameters { ResultFormat = "message", IncrementalOutput = true }
                    };

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> DeepResearchTypingIncremental = new(
                        "deep-research-planning-type",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", "本研究聚焦于")
                                {
                                    Phase = "ResearchPlanning",
                                    Extra = new TextChatMessageExtra { DeepResearch = new DashScopeDeepResearchInfo() },
                                    Status = "typing",
                                },
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 4,
                            },
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34"
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchWebResearchStreamingQueriesIncremental = new(
                        "deep-research-web-research-streaming-queries",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "WebResearch",
                                    Status = "streamingQueries",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            Research = new DashScopeDeepResearchTask
                                            {
                                                Id = 1, Query = "人工智能"
                                            }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 54,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchWebResearchStreamingQueriesResearchGoalIncremental = new(
                        "deep-research-web-research-streaming-queries-research-goal",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "WebResearch",
                                    Status = "streamingQueries",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            Research = new DashScopeDeepResearchTask
                                            {
                                                Id = 1,
                                                Query = string.Empty,
                                                ResearchGoal = "深入理解人工智能"
                                            }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 63,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchWebResearchStreamingWebResultsIncremental = new(
                        "deep-research-web-research-streaming-web-results",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "WebResearch",
                                    Status = "streamingWebResult",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            Research = new DashScopeDeepResearchTask
                                            {
                                                Id = 1,
                                                WebSites = new List<DashScopeDeepResearchWebsiteRef>
                                                {
                                                    new(
                                                        "大数据技术下个性化在线教育互动式教学探索",
                                                        "摘要：随着在线教育在教育领域中的异军突起，提出了在线教育互动式教学个性化推荐系统构建模式，拟采用大数据的分析技术，通过对学生学习行为数据的收集和分析，综合分析 ",
                                                        "http://qks.cqu.edu.cn/html/gdjzjycn/2018/4/20180425.htm",
                                                        "https://img.alicdn.com/imgextra/i1/O1CN014C3hAp297DQsJYflo_!!6000000008020-73-tps-32-32.ico"),
                                                    new(
                                                        "人工智能赋能个性化学习：E-Learning推荐系统研究热点与展望",
                                                        "三是随着大规模开放在线课程的流行，个性化推荐逐步突破小规模而面向大规模学习者群体，重视通过对海量学习资源和过程数据的搜集和挖掘而提供个性化推荐。四 ",
                                                        "https://aidc.shisu.edu.cn/66/27/c11041a157223/page.htm",
                                                        string.Empty),
                                                    new(
                                                        "华东师大再次入选“人工智能+高等教育”应用场景典型案例",
                                                        "近日，教育部公布了第二批32个“人工智能+高等教育”应用场景典型案例，华东师范大学“大模型数字人赋能师范生实践教学能力提升”案例成功入选。",
                                                        "https://www.ecnu.edu.cn/info/1094/68108.htm",
                                                        string.Empty),
                                                    new(
                                                        "神策数据：在线教育行业12大核心场景案例全解析！",
                                                        "神策智能推荐基于用户与视频互动关系和视频本身的素材特征，为用户推出其最感兴趣的内容。智能推荐实现的方式有两种：一种是基于机器学习算法实现个性化推荐 ",
                                                        "https://www.rmlt.com.cn/2020/0908/592624.shtml",
                                                        "https://img.alicdn.com/imgextra/i4/O1CN01spKOuX1OaGcBIcIxs_!!6000000001721-73-tps-16-16.ico"),
                                                    new(
                                                        "突破智慧教育: 基于图学习的课程推荐系统",
                                                        "最后, 基于真实课程学习平台数据集, 以对比实验表明了离线推荐引擎相比其他主流推荐算法的先进性, 并基于两个典型用例分析验证了在线推荐系统面临工业场景需求的可用性.",
                                                        "https://www.jos.org.cn/josen/article/html/6629?st=article_issue",
                                                        "https://img.alicdn.com/imgextra/i3/O1CN01dSZGsI1aq5gkwZAJL_!!6000000003380-73-tps-16-16.ico"),
                                                    new(
                                                        "2024年人工智能+教育行业发展研究报告",
                                                        "纵观全球AI+教育产业的发展历程，AI技术变革推动全球AI+教育发展，个性化教与学逐步成为现实。 政策方面，UNESCO及全球各国政府共同关注AI+教育的机遇及风险； ",
                                                        "https://pdf.dfcfw.com/pdf/H3_AP202408051639144645_1.pdf?1723644716000.pdf",
                                                        "https://img.alicdn.com/imgextra/i4/O1CN01pnGD4c1PQ1N2QMnLP_!!6000000001834-73-tps-32-32.ico"),
                                                    new(
                                                        "面向在线智慧学习的教育数据挖掘技术研究",
                                                        "此外,学. 习平台通过智能分析学生的答题数据,向学生、教师. 反映学生的个性化学习情况,并进行有针对性的试. 题训练,旨在帮助学生提高学业水平. 随着这些不同类型在线教育 ",
                                                        "http://staff.ustc.edu.cn/~qiliuql/files/Publications/PRAI2018.pdf",
                                                        string.Empty)
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 97,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchWebResearchStreamingWebResultsLearningMapIncremental = new(
                        "deep-research-web-research-streaming-web-results-learning-map",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "WebResearch",
                                    Status = "streamingWebResult",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            Research = new DashScopeDeepResearchTask
                                            {
                                                Id = 2,
                                                LearningMap =
                                                    new Dictionary<string, string> { { "11", "该" } }
                                            }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 264,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchWebResearchWebResultFinishedIncremental = new(
                        "deep-research-web-research-web-result-finished",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "WebResearch",
                                    Status = "WebResultFinished",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            Research = new DashScopeDeepResearchTask { Id = 1 }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 97,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchKeepAliveIncremental = new(
                        "deep-research-keep-alive-type",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "KeepAlive",
                                    Status = "typing",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo()
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 97,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchAnswerReferenceIncremental = new(
                        "deep-research-answer-typing-reference",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "answer",
                                    Status = "typing",
                                    Content = "#",
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo
                                        {
                                            References = new List<DashScopeDeepResearchReference>
                                            {
                                                new(
                                                    "https://img.alicdn.com/imgextra/i3/O1CN01QA3ndK1maJQ8rZTo1_!!6000000004970-55-tps-32-32.svg",
                                                    "基于模型的推荐系统使用机器学习或深度学习模型来预测用户的兴趣。这些模型可以处理复杂的用户行为数据和内容特征，生成更精准的推荐。 实现方法• 矩阵分解 ",
                                                    1,
                                                    "基于人工智能的智能推荐系统：原理、实现与优化原创",
                                                    "https://blog.csdn.net/qq_74383080/article/details/148544524")
                                            }
                                        }
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 2347,
                            },
                        });

                public static readonly
                    RequestSnapshot<ModelRequest<TextGenerationInput, ITextGenerationParameters>,
                        ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
                    DeepResearchAnswerFinishedIncremental = new(
                        "deep-research-answer-finished",
                        DeepResearchRequest,
                        new ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>
                        {
                            RequestId = "eee227a4-d38f-4b8e-8c7f-167e76dbdc34",
                            Output = new TextGenerationOutput
                            {
                                Message = new TextChatMessage("assistant", string.Empty)
                                {
                                    Phase = "answer",
                                    Status = "finished",
                                    Content = string.Empty,
                                    Extra = new TextChatMessageExtra
                                    {
                                        DeepResearch = new DashScopeDeepResearchInfo()
                                    }
                                }
                            },
                            Usage = new TextGenerationTokenUsage
                            {
                                InputTokens = 652, OutputTokens = 8930,
                            },
                        });
            }
        }
    }
}
