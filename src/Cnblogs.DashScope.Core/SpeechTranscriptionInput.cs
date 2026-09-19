using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Input for asynchronous speech transcription (Paraformer / Fun-ASR filetrans).
/// </summary>
public class SpeechTranscriptionInput : IDashScopeOssUploadConfig
{
    private const string OssSchema = "oss://";

    /// <summary>
    /// Audio/video file URLs to transcribe. Only one URL is supported per request.
    /// </summary>
    public IReadOnlyList<string> FileUrls { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Optional dialogue context for Fun-ASR / Qwen-Audio filetrans models to improve recognition accuracy.
    /// </summary>
    public IReadOnlyList<SpeechTranscriptionContextMessage>? Context { get; set; }

    /// <inheritdoc />
    public bool EnableOssResolve() => FileUrls.Any(u => u.StartsWith(OssSchema, StringComparison.Ordinal));
}
