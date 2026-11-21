namespace Cnblogs.DashScope.Core;

/// <summary>
/// The voice effects of a MiniMax speech synthesis request. Supported audio formats:
/// mp3, wav and flac in non-streaming mode, mp3 in streaming mode.
/// </summary>
public class MinimaxVoiceModify
{
    /// <summary>
    /// The pitch shift (deep / bright), range [-100, 100], lower is deeper.
    /// </summary>
    public int? Pitch { get; set; }

    /// <summary>
    /// The intensity (powerful / soft), range [-100, 100], lower is more powerful.
    /// </summary>
    public int? Intensity { get; set; }

    /// <summary>
    /// The timbre brightness (mellow / crisp), range [-100, 100], lower is mellower.
    /// </summary>
    public int? Timbre { get; set; }

    /// <summary>
    /// The sound effect: spacious_echo, auditorium_echo, lofi_telephone or robotic.
    /// </summary>
    public string? SoundEffects { get; set; }
}
