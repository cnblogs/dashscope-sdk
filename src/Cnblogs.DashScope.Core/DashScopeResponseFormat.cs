namespace Cnblogs.DashScope.Core;

/// <summary>
/// Available formats for <see cref="DashScopeResponseFormat"/>
/// </summary>
public record DashScopeResponseFormat(string Type)
{
    /// <summary>
    /// Output should be text.
    /// </summary>
    public static readonly DashScopeResponseFormat Text = new("text");

    /// <summary>
    /// Output should be json object.
    /// </summary>
    public static readonly DashScopeResponseFormat Json = new("json_object");
}
