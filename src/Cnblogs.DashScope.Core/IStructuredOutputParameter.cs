namespace Cnblogs.DashScope.Core;

/// <summary>
/// Parameters for structured output.
/// </summary>
public interface IStructuredOutputParameter
{
    /// <summary>
    /// The format of response message, must be <c>text</c> or <c>json_object</c>
    /// </summary>
    /// <remarks>
    /// This property is not ResultFormat(which is 'message' or 'text'). Be sure not to confuse them.
    /// </remarks>
    /// <example>
    /// Set response format to <c>json_object</c>.
    /// <code>
    ///     parameter.ResponseFormat = DashScopeResponseFormat.Json;
    /// </code>
    /// </example>
    DashScopeResponseFormat? ResponseFormat { get; }
}
