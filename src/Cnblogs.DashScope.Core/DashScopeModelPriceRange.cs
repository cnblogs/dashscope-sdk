namespace Cnblogs.DashScope.Core;

/// <summary>
/// The prices of one billing range of a model.
/// </summary>
/// <param name="RangeName">The billing range, e.g. <c>Default</c>, <c>32k&lt;input&lt;=128k</c>.</param>
/// <param name="Prices">The price items of the range.</param>
public record DashScopeModelPriceRange(string RangeName, List<DashScopeModelPrice> Prices);
