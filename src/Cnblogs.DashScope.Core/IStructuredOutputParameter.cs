namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for structured output.
/// </summary>
public interface IStructuredOutputParameter
{
    /// <summary>
    /// The format of response message, must be <c>text</c>, <c>json_object</c> or <c>json_schema</c>
    /// </summary>
    /// <remarks>
    /// This property is not ResultFormat(which is 'message' or 'text'). Be sure not to confuse them.
    /// </remarks>
    /// <example>
    /// Set response format to <c>json_object</c>.
    /// <code>
    ///     parameter.ResponseFormat = DashScopeResponseFormat.JsonObject;
    /// </code>
    /// Or use <c>json_schema</c> for strict structured output.
    /// <code>
    ///     parameter.ResponseFormat = DashScopeResponseFormat.JsonSchema(new DashScopeJsonSchema("answer", schema, Strict: true));
    /// </code>
    /// </example>
    DashScopeResponseFormat? ResponseFormat { get; set; }
}
