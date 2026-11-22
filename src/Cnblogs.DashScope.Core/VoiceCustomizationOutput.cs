namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a voice customization request. Which members are present depends on
/// <see cref="VoiceCustomizationInput.Action"/>: create_voice returns
/// <see cref="VoiceId"/> (and <see cref="PreviewAudio"/> for designed voices),
/// list_voice returns <see cref="VoiceList"/>, query_voice returns the detail members,
/// update_voice and delete_voice return an empty output.
/// </summary>
/// <param name="VoiceId">The id of the created voice.</param>
/// <param name="PreviewAudio">The preview audio of the designed voice. Designing only;
/// observed as an empty object when cloning.</param>
/// <param name="VoiceList">The voices returned by list_voice.</param>
/// <param name="ResourceLink">The url of the reference audio. query_voice of cloned voices.</param>
/// <param name="GmtCreate">The creation time of the voice. query_voice.</param>
/// <param name="GmtModified">The last modification time of the voice. query_voice.</param>
/// <param name="Status">The voice status: DEPLOYING (reviewing / processing),
/// OK (approved and usable) or UNDEPLOYED (rejected). query_voice and list_voice.</param>
/// <param name="TargetModel">The model driving the voice. query_voice.</param>
/// <param name="VoicePrompt">The text describing the voice. query_voice and list_voice
/// of designed voices.</param>
/// <param name="PreviewText">The text of the preview audio. query_voice and list_voice
/// of designed voices.</param>
/// <param name="PageIndex">The page index of list_voice. Undocumented pagination echo.</param>
/// <param name="PageSize">The page size of list_voice. Undocumented pagination echo.</param>
/// <param name="TotalCount">The total count of voices matching the list_voice filter.
/// Undocumented pagination echo.</param>
public record VoiceCustomizationOutput(
    string? VoiceId = null,
    VoicePreviewAudio? PreviewAudio = null,
    List<VoiceCustomizationVoice>? VoiceList = null,
    string? ResourceLink = null,
    string? GmtCreate = null,
    string? GmtModified = null,
    string? Status = null,
    string? TargetModel = null,
    string? VoicePrompt = null,
    string? PreviewText = null,
    int? PageIndex = null,
    int? PageSize = null,
    int? TotalCount = null);
