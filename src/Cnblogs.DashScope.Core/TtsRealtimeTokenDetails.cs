namespace Cnblogs.DashScope.Core;

/// <summary>
/// The token details of the TTS realtime API usage. Not listed in the Bailian documentation yet.
/// </summary>
/// <param name="TextTokens">The text token count.</param>
/// <param name="AudioTokens">The audio token count, present in output details only.</param>
public record TtsRealtimeTokenDetails(int? TextTokens = null, int? AudioTokens = null);
