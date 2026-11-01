using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Available formats for <see cref="IStructuredOutputParameter"/>.<see cref="IStructuredOutputParameter.ResponseFormat"/>
/// </summary>
public record DashScopeResponseFormat(
    string Type,
    [property: JsonPropertyName("json_schema")] DashScopeJsonSchema? Schema = null)
{
    /// <summary>
    /// Output should be text.
    /// </summary>
    public static readonly DashScopeResponseFormat Text = new("text");

    /// <summary>
    /// Output should be valid JSON object.
    /// </summary>
    public static readonly DashScopeResponseFormat JsonObject = new("json_object");

    /// <summary>
    /// Output should be valid JSON object.
    /// </summary>
    [Obsolete("Prefer using JsonObject instead.")]
    public static readonly DashScopeResponseFormat Json = JsonObject;

    /// <summary>
    /// Output should be valid JSON that strictly follows the given schema.
    /// </summary>
    /// <param name="schema">The schema to follow.</param>
    /// <returns>A <see cref="DashScopeResponseFormat"/> with type <c>json_schema</c>.</returns>
    public static DashScopeResponseFormat JsonSchema(DashScopeJsonSchema schema) => new("json_schema", schema);
}
