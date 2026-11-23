namespace Cnblogs.DashScope.Core;

/// <summary>
/// The voice clone options of a live translation session, only effective when
/// <see cref="LiveTranslateSession.EnableVoiceClone"/> is true.
/// </summary>
/// <param name="Frequency">How often the voice is cloned: never (use the pre-cloned voice id,
/// set <see cref="LiveTranslateSession.Voice"/> to it), once (clone once from the input audio at
/// the beginning, suitable for a single speaker) or always (clone before every output, following
/// the input dynamically, suitable for multiple speakers). Set Voice to "default" for once /
/// always.</param>
public record LiveTranslateVoiceCloneOptions(
    string? Frequency = null);
