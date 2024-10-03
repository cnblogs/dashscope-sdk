namespace Cnblogs.DashScope.Core;

/// <summary>
/// Model request for websocket.
/// </summary>
public interface IWsModelRequest
{
    /// <summary>
    /// Group name of the task.
    /// </summary>
    public string TaskGroup { get; }

    /// <summary>
    /// Name of the task.
    /// </summary>
    public string Task { get;  }

    /// <summary>
    /// Name of the function to call.
    /// </summary>
    public string Function { get; }
}
