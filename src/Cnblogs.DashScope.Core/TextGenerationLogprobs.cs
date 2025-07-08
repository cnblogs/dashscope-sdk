namespace Cnblogs.DashScope.Core;

/// <summary>
/// Possibilities of token choices.
/// </summary>
/// <param name="Content">The choices with their possibility.</param>
public record TextGenerationLogprobs(List<TextGenerationLogprobContent> Content);
