using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the generated response from the model.
/// </summary>
/// <remarks>
/// The gateway opens some streams (the web search of qwen3.8-omni-flash) with a default frame
/// that is the bare output without the envelope of a response: it is read as a response holding
/// that output, without a request id and without a usage.
/// </remarks>
/// <typeparam name="TOutput">The output type.</typeparam>
/// <typeparam name="TUsage">The usage type.</typeparam>
[JsonConverter(typeof(ModelResponseJsonConverterFactory))]
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
