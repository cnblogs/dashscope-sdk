namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for an application all.
/// </summary>
/// <typeparam name="TBizParams">Type of the biz_content</typeparam>
public class ApplicationRequest<TBizParams>
    where TBizParams : class
{
    /// <summary>
    /// Content of this call.
    /// </summary>
    public required ApplicationInput<TBizParams> Input { get; init; }

    /// <summary>
    /// Optional configurations.
    /// </summary>
    public ApplicationParameters? Parameters { get; init; }
}

/// <summary>
/// Request body for an application call with dictionary biz_content.
/// </summary>
public class ApplicationRequest : ApplicationRequest<Dictionary<string, object?>>;
