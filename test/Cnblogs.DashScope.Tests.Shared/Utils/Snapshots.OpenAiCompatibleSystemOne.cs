using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class OpenAiCompatibleSystemOne
    {
        // The ticket triage example of the official docs: an object state and the three question
        // types (choice / noul / score) in one request. The endpoint is only served on the
        // workspace domain, the request line keeps the {WorkspaceId} placeholder.
        public static readonly RequestSnapshot<SystemOneDecisionInput, SystemOneDecision>
            TicketTriageNoSse = new(
                "openai-compatible-system-one",
                new SystemOneDecisionInput
                {
                    Model = "decision-model",
                    State = new Dictionary<string, string>
                    {
                        ["ticket_id"] = "T-1001",
                        ["content"] = "订单支付后超过 24 小时仍未到账，用户无法继续使用核心服务，要求立即处理。"
                    },
                    Questions = new Dictionary<string, SystemOneQuestion>
                    {
                        ["department"] = SystemOneQuestion.Choice(
                            "应该由哪个团队处理？",
                            new Dictionary<string, string>
                            {
                                ["billing"] = "支付、退款和账单问题",
                                ["technical"] = "产品故障和集成问题"
                            }),
                        ["escalate"] = SystemOneQuestion.Noul("是否需要立即通知值班人员？"),
                        ["severity"] = SystemOneQuestion.Score(
                            "这个问题有多严重？",
                            new List<string>
                            {
                                "轻微问题，不影响功能",
                                "部分功能受影响，但存在替代方案",
                                "核心功能不可用，没有替代方案",
                                "造成严重业务或安全影响"
                            })
                    }
                },
                new SystemOneDecision
                {
                    Model = "decision-model",
                    RequestId = "57e81fc8-753f-9140-b0f2-fb64314a67fa",
                    Answers = new Dictionary<string, SystemOneAnswer>
                    {
                        ["department"] = new SystemOneAnswer
                        {
                            Type = "choice",
                            Choice = "billing",
                            Confidence = 0.9956051111221313,
                            Probabilities = new Dictionary<string, double>
                            {
                                ["billing"] = 0.9978025555610657,
                                ["technical"] = 0.002197492402046919
                            }
                        },
                        // a noul answer has no probabilities and no confidence
                        ["escalate"] = new SystemOneAnswer { Type = "noul", Noul = 0.9970996379852295 },
                        ["severity"] = new SystemOneAnswer
                        {
                            Type = "score",
                            Score = 2.0205498412251472,
                            Confidence = 0.8286660062149167,
                            Legend = new Dictionary<string, string>
                            {
                                ["0"] = "轻微问题，不影响功能",
                                ["1"] = "部分功能受影响，但存在替代方案",
                                ["2"] = "核心功能不可用，没有替代方案",
                                ["3"] = "造成严重业务或安全影响"
                            },
                            Probabilities = new Dictionary<string, double>
                            {
                                ["0"] = 0.007220739033073187,
                                ["1"] = 0.0609506294131279,
                                ["2"] = 0.8358867764472961,
                                ["3"] = 0.09594188630580902
                            }
                        }
                    },
                    Usage = new SystemOneUsage(380),
                    LatencyMs = 105.8
                });

        // A conversation (an array state) judged by a single noul question that describes its
        // yes / no answers
        public static readonly RequestSnapshot<SystemOneDecisionInput, SystemOneDecision>
            NoulNoSse = new(
                "openai-compatible-system-one-noul",
                new SystemOneDecisionInput
                {
                    Model = "decision-model",
                    State = new List<object>
                    {
                        new { role = "user", content = "东西收到了，但是坏了一个角，怎么申请退款？" },
                        new { role = "assistant", content = "非常抱歉给您带来不便，请提供订单号。" }
                    },
                    Questions = new Dictionary<string, SystemOneQuestion>
                    {
                        ["refund"] = SystemOneQuestion.Noul(
                            "用户是否在索要退款？",
                            new Dictionary<string, string>
                            {
                                ["true"] = "用户明确要求退货或退款",
                                ["false"] = "用户只是咨询，没有提出退款要求"
                            })
                    }
                },
                new SystemOneDecision
                {
                    Model = "decision-model",
                    RequestId = "abf45fc8-5377-97eb-9288-ca120838b0cd",
                    Answers = new Dictionary<string, SystemOneAnswer>
                    {
                        ["refund"] = new SystemOneAnswer { Type = "noul", Noul = 0.9941666126251221 }
                    },
                    Usage = new SystemOneUsage(122),
                    LatencyMs = 386.9
                });

        // An invalid question. The endpoint lives under /compatible-mode, but its errors take
        // the DashScope shape (code / message / request_id) instead of the OpenAI one.
        public static readonly RequestSnapshot<SystemOneDecisionInput, DashScopeError>
            InvalidQuestionNoSse = new(
                "openai-compatible-system-one-invalid-question",
                new SystemOneDecisionInput
                {
                    Model = "decision-model",
                    State = "这段评论是否在索要退款？",
                    Questions = new Dictionary<string, SystemOneQuestion>
                    {
                        ["refund"] = new SystemOneQuestion("foo", "是否在索要退款？", null)
                    }
                },
                new DashScopeError
                {
                    Code = "InvalidParameter",
                    Message =
                    "<400> InternalError.Algo.InvalidParameter: [QUESTION_INVALID] Invalid question at /questions/refund: invalid value.",
                    RequestId = "537f751e-16a4-913b-beb8-8f7dcf6fe893"
                });

        // The endpoint is only served on the workspace domain: the shared domain answers 404
        // with an empty body, so there is no error detail to map.
        public static readonly RequestSnapshot<SystemOneDecisionInput, DashScopeError?> SharedDomainNoSse = new(
                "openai-compatible-system-one-shared-domain",
                TicketTriageNoSse.RequestModel,
                null);
    }
}
