namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token usage of a decision model call.
/// </summary>
/// <param name="InputTokens">The input token count, the model generates no output token.</param>
public record SystemOneUsage(int InputTokens);
