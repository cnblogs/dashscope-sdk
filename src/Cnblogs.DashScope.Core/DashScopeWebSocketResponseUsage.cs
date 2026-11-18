namespace Cnblogs.DashScope.Core;

/// <summary>
/// Usage info of websocket task.
/// </summary>
/// <param name="Characters">Current character usage count. Available in speech synthesis tasks.</param>
/// <param name="Duration">Billed audio duration in seconds. Available in speech recognition tasks.</param>
public record DashScopeWebSocketResponseUsage(int Characters = 0, int? Duration = null);
