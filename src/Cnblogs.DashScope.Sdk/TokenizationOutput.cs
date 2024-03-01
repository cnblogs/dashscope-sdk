namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// The output for tokenizer.
/// </summary>
/// <param name="TokenIds">The id of tokens.</param>
/// <param name="Tokens">The tokens.</param>
public record TokenizationOutput(List<int> TokenIds, List<string> Tokens);
