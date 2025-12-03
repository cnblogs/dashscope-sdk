namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Options for speech recognition.
    /// </summary>
    public class AsrOptions
    {
        /// <summary>
        /// Language of the audio. Values in: zh, en, ja, de, ko, ru, fr, pt, ar, it, es.
        /// </summary>
        /// <remarks>You can only set exactly 1 language. Leave this value as <c>null</c> if the audio file contains multiple languages.</remarks>
        public string? Language { get; set; }

        /// <summary>
        /// Enable inverse text normalization(ITN).
        /// </summary>
        public bool? EnableItn { get; set; }

        /// <summary>
        /// Return language identify result in response.
        /// </summary>
        /// <remarks>If <see cref="Language"/> is set, this will return the value of <see cref="Language"/>.</remarks>
        public bool? EnableIld { get; set; }
    }
}
