namespace Cnblogs.DashScope.Core;

/// <summary>
/// One price item of a model.
/// </summary>
/// <param name="Type">What is billed, e.g. <c>input_token</c>, <c>output_token</c>,
/// <c>input_token_cache_read</c>, <c>image_number</c>.</param>
/// <param name="Price">The unit price.</param>
/// <param name="PriceUnit">The unit of the price, e.g. 每百万tokens, 每张.</param>
/// <param name="PriceName">The display name of the item, e.g. 输入, 输出.</param>
/// <param name="TimeBand">The time band the price applies to, e.g. <c>standard</c>, <c>peak</c>,
/// <c>offpeak</c>. null or empty when the price does not depend on the time.</param>
public record DashScopeModelPrice(
    string Type,
    string Price,
    string PriceUnit,
    string PriceName,
    string? TimeBand);
