using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for synchronous Fun-ASR-Flash / Qwen-Audio ASR Flash recognition.
/// </summary>
public class SpeechRecognitionInput : IDashScopeOssUploadConfig
{
    /// <summary>
    /// Messages including the audio to recognize and optional context.
    /// </summary>
    public IReadOnlyList<SpeechRecognitionMessage> Messages { get; set; } = Array.Empty<SpeechRecognitionMessage>();

    /// <inheritdoc />
    public bool EnableOssResolve() => Messages.Any(m => m.IsOss());
}
