using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a research task from deep research model.
/// </summary>
public class DashScopeDeepResearchTask
{
    /// <summary>
    /// The id of the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Goal of the research.
    /// </summary>
    [JsonPropertyName("researchGoal")]
    public string? ResearchGoal { get; set; }

    /// <summary>
    /// Query string of the research.
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// The websites reference.
    /// </summary>
    [JsonPropertyName("webSites")]
    public List<DashScopeDeepResearchWebsiteRef>? WebSites { get; set; }

    /// <summary>
    /// The content from tool calls.
    /// </summary>
    [JsonPropertyName("learningMap")]
    public Dictionary<string, object>? LearningMap { get; set; }
}
