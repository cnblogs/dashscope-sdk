namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the generated response from the model.
/// </summary>
/// <typeparam name="TOutput">The output type.</typeparam>
/// <typeparam name="TUsage">The usage type.</typeparam>
public record ModelResponse<TOutput, TUsage>
    where TOutput : class
    where TUsage : class
{
    /// <summary>
    /// The generated output for this request.
    /// </summary>
    public TOutput Output { get; init; } = null!;

    /// <summary>
    /// The token usage for this request.
    /// </summary>
    public TUsage? Usage { get; init; }

    /// <summary>
    /// The unique id of this request.
    /// </summary>
    public string RequestId { get; set; } = string.Empty;
}
