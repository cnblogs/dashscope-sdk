namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for an application all.
/// </summary>
/// <typeparam name="TBizContent">Type of the biz_content</typeparam>
public class ApplicationRequest<TBizContent>
    where TBizContent : class
{
    /// <summary>
    /// Content of this call.
    /// </summary>
    public required ApplicationInput<TBizContent> Input { get; init; }

    /// <summary>
    /// Optional configurations.
    /// </summary>
    public required ApplicationParameters? Parameters { get; init; }
}

/// <summary>
/// Request body for an application call with dictionary biz_content.
/// </summary>
public class ApplicationRequest : ApplicationRequest<Dictionary<string, object?>>;
