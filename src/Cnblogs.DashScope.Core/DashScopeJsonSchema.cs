namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the JSON schema for LLM response.
/// </summary>
/// <param name="Name">The name of the schema</param>
/// <param name="Schema">The schema object.</param>
/// <param name="Strict">Whether follow the schema strictly.</param>
public record DashScopeJsonSchema(string Name, object Schema, bool? Strict = null);
