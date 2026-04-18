namespace Cnblogs.DashScope.Core;

/// <summary>
/// Extra info when <see cref="SearchOptions.EnableSearchExtension"/> is true.
/// </summary>
/// <param name="Result">The results from extension tools.</param>
/// <param name="Tool">The name of the tools.</param>
public record DashScopeWebSearchExtra(string Result, string Tool);
