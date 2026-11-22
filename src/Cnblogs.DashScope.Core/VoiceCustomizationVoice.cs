namespace Cnblogs.DashScope.Core;

/// <summary>
/// A voice in the list returned by the list_voice action.
/// </summary>
/// <param name="VoiceId">The id of the voice, usable as the voice of synthesis requests.</param>
/// <param name="GmtCreate">The creation time.</param>
/// <param name="GmtModified">The last modification time.</param>
/// <param name="Status">The voice status: DEPLOYING (reviewing / processing),
/// OK (approved and usable) or UNDEPLOYED (rejected).</param>
/// <param name="TargetModel">The model driving the voice. Undocumented field.</param>
/// <param name="VoicePrompt">The text describing the voice. Designed voices only.</param>
/// <param name="PreviewText">The text of the preview audio. Designed voices only.</param>
public record VoiceCustomizationVoice(
    string? VoiceId = null,
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null,
    string? TargetModel = null,
    string? VoicePrompt = null,
    string? PreviewText = null);
