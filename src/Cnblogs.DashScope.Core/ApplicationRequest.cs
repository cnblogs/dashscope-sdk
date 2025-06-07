using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Request body for an application all.
/// </summary>
/// <typeparam name="TBizParams">Type of the biz_content</typeparam>
public class ApplicationRequest<TBizParams> : IDashScopeWorkspaceConfig
    where TBizParams : class
{
    /// <summary>
    /// Content of this call.
    /// </summary>
    public ApplicationInput<TBizParams> Input { get; set; } = new();

    /// <summary>
    /// Optional configurations.
    /// </summary>
    public ApplicationParameters? Parameters { get; set; }

    /// <summary>
    /// Optional workspace id.
    /// </summary>
    [JsonIgnore]
    public string? WorkspaceId { get; set; }
}

/// <summary>
/// Request body for an application call with dictionary biz_content.
/// </summary>
public class ApplicationRequest : ApplicationRequest<Dictionary<string, object?>>
{
}
