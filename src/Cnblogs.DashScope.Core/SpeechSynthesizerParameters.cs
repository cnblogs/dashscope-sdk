namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Parameters for TTS task.
    /// </summary>
    public class SpeechSynthesizerParameters
    {
        /// <summary>
        /// Fixed to "PlainText"
        /// </summary>
        public string TextType { get; set; } = "PlainText";

        /// <summary>
        /// The voice to use.
        /// </summary>
        public string Voice { get; set; } = string.Empty;

        /// <summary>
        /// Output file format, can be pcm, wav or mp3.
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Output audio sample rate.
        /// </summary>
        public int? SampleRate { get; set; }

        /// <summary>
        /// Output audio volume, range between 0-100, defaults to 50.
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// Speech speed, range between 0.5~2.0, defaults to 1.0.
        /// </summary>
        public float? Rate { get; set; }

        /// <summary>
        /// Pitch of the voice, range between 0.5~2, defaults to 1.0.
        /// </summary>
        public float? Pitch { get; set; }

        /// <summary>
        /// Enable SSML, you can only send text once if enabled.
        /// </summary>
        public bool? EnableSsml { get; set; }
    }
}
