namespace Cnblogs.DashScope.Core;

/// <summary>
/// The error detail of an error event from the TTS realtime API.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
public record TtsRealtimeError(string? Code = null, string? Message = null);
