using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.DecisionModel;

public class TicketTriageExample : DecisionModelSample
{
    /// <inheritdoc />
    public override string Description => "Triage a support ticket with the decision model (decision-model, workspace domain)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // The decision model is only served on the workspace domain, it needs its own client
        // configured with the workspace id.
        Console.WriteLine("Reading workspace id from environment variable DASHSCOPE_WORKSPACE_ID");
        var workspaceId = Environment.GetEnvironmentVariable("DASHSCOPE_WORKSPACE_ID", EnvironmentVariableTarget.Process)
                          ?? Environment.GetEnvironmentVariable("DASHSCOPE_WORKSPACE_ID", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(workspaceId))
        {
            Console.Write("WorkspaceId > ");
            workspaceId = Console.ReadLine();
        }

        Console.WriteLine("Reading key from environment variable DASHSCOPE_KEY");
        var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.Process)
                     ?? Environment.GetEnvironmentVariable("DASHSCOPE_KEY", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.Write("ApiKey > ");
            apiKey = Console.ReadLine();
        }

        var workspaceClient = new DashScopeClient(apiKey!, workspaceId: workspaceId);

        Console.WriteLine("Deciding...");
        var decision = await workspaceClient.OpenAiCompatibleGetSystemOneDecisionAsync(
            new SystemOneDecisionInput
            {
                Model = "decision-model",
                State = new
                {
                    ticket_id = "T-1001",
                    content = "订单支付后超过 24 小时仍未到账，用户无法继续使用核心服务，要求立即处理。"
                },
                Questions = new Dictionary<string, SystemOneQuestion>
                {
                    // pick one of the options
                    ["department"] = SystemOneQuestion.Choice(
                        "应该由哪个团队处理？",
                        new Dictionary<string, string>
                        {
                            ["billing"] = "支付、退款和账单问题",
                            ["technical"] = "产品故障和集成问题"
                        }),
                    // yes / no, the answer is the probability of yes
                    ["escalate"] = SystemOneQuestion.Noul("是否需要立即通知值班人员？"),
                    // an ordered scale
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
            });

        // the answers are keyed by the question id of the request
        var answers = decision.Answers;
        var department = answers["department"];
        Console.WriteLine($"Department: {department.Choice} (confidence {department.Confidence:F2})");
        Console.WriteLine("  " + string.Join(", ", department.Probabilities!.Select(p => $"{p.Key} {p.Value:F2}")));
        Console.WriteLine($"Escalate: P(yes) {answers["escalate"].Noul:F2}");
        var severity = answers["severity"];
        Console.WriteLine($"Severity: {severity.Score:F2} (confidence {severity.Confidence:F2})");
        foreach (var level in severity.Legend!)
        {
            Console.WriteLine($"  {level.Key}: {level.Value} ({severity.Probabilities![level.Key]:F2})");
        }

        Console.WriteLine($"Token usage: {decision.Usage?.InputTokens}, latency: {decision.LatencyMs}ms");
    }
}
