using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Configuration for the <c>X-DashScope-DataInspection</c> request header, which further inspects
/// input and output content for violations using AI safety guardrails.
/// Can be set on the client (<see cref="DashScopeOptions"/>) or on a single request
/// (<see cref="ModelRequest{TInput, TParameter}"/>), request level configuration overrides client level.
/// </summary>
/// <param name="Input">The engine used to inspect input content, null to skip input inspection.</param>
/// <param name="Output">The engine used to inspect output content, null to skip output inspection.</param>
public sealed record DataInspectionOptions(string? Input = null, string? Output = null)
{
    /// <summary>
    /// The key used to pass request level configuration to the send pipeline.
    /// </summary>
    internal static readonly HttpRequestOptionsKey<DataInspectionOptions> RequestOptionKey = new(
        "Cnblogs.DashScope.DataInspection");

    private static readonly JsonSerializerOptions HeaderValueSerializationOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Do not inspect content, this is the default value. Can be used to disable client level inspection for a single request.
    /// </summary>
    public static DataInspectionOptions Disabled { get; } = new();

    /// <summary>
    /// Inspect input content with the AI safety guardrail.
    /// </summary>
    public static DataInspectionOptions CipInput { get; } = new(Input: "cip");

    /// <summary>
    /// Inspect output content with the AI safety guardrail.
    /// </summary>
    public static DataInspectionOptions CipOutput { get; } = new(Output: "cip");

    /// <summary>
    /// Inspect both input and output content with the AI safety guardrail.
    /// </summary>
    public static DataInspectionOptions CipInputAndOutput { get; } = new(Input: "cip", Output: "cip");

    /// <summary>
    /// Serialize this instance to the value of the <c>X-DashScope-DataInspection</c> header.
    /// </summary>
    /// <returns>The header value, or null when neither input nor output is inspected.</returns>
    public string? ToHeaderValue()
    {
        return Input is null && Output is null
            ? null
            : JsonSerializer.Serialize(this, HeaderValueSerializationOptions);
    }
}
