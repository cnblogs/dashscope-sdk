namespace Cnblogs.DashScope.Core;

/// <summary>
/// The result of a decision model call: the answer of every question of the request, keyed by
/// the question id.
/// </summary>
public class SystemOneDecision
{
    /// <summary>
    /// The model of the request.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The id of the request, for troubleshooting.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// The answers, keyed by the question id given in the request.
    /// </summary>
    public Dictionary<string, SystemOneAnswer> Answers { get; set; } = new();

    /// <summary>
    /// The token usage.
    /// </summary>
    public SystemOneUsage? Usage { get; set; }

    /// <summary>
    /// The time the server spent on the request, in milliseconds.
    /// </summary>
    public double? LatencyMs { get; set; }
}
