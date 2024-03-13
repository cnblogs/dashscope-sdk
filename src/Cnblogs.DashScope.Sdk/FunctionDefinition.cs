using Json.Schema;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Definition of function that can be called by model.
/// </summary>
/// <param name="Name">The name of the function.</param>
/// <param name="Description">Descriptions about this function that help model to decide when to call this function.</param>
/// <param name="Parameters">The parameters JSON schema.</param>
public record FunctionDefinition(string Name, string Description, JsonSchema? Parameters);
