namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents output of multi-model generation.
/// </summary>
/// <param name="Choices">The generated message.</param>
/// <param name="SearchInfo">Web search details.</param>
/// <param name="ToolInfo">Outputs from the tool being called by model.</param>
public record MultimodalOutput(
    List<MultimodalChoice> Choices,
    DashScopeWebSearchInfo? SearchInfo = null,
    List<ToolInfoOutput>? ToolInfo = null);
