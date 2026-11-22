namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents output of multimodal generation.
/// </summary>
/// <param name="Choices">The generated message. Null for speech synthesis models, which
/// return audio in <paramref name="Audio"/> (qwen3-tts) or <paramref name="Data"/> (MiniMax speech) instead.</param>
/// <param name="SearchInfo">Web search details.</param>
/// <param name="ToolInfo">Outputs from the tool being called by model.</param>
/// <param name="TaskMetric">Metric for generation task, e.g. image generation.</param>
/// <param name="FinishReason">Generation finish reason of speech synthesis models
/// (qwen3-tts): "null" while generating, "stop" when the model output naturally ends or
/// a stop condition is triggered.</param>
/// <param name="Audio">The audio output of qwen3-tts speech synthesis models.</param>
/// <param name="BaseResp">The MiniMax style status of MiniMax speech synthesis models.</param>
/// <param name="Data">The synthesized audio data of MiniMax speech synthesis models.</param>
/// <param name="ExtraInfo">The extra information of MiniMax speech synthesis models.</param>
/// <param name="TraceId">The MiniMax side trace id of MiniMax speech synthesis models.</param>
/// <param name="InputSensitive">Whether the input audio of a MiniMax voice clone hit content moderation.</param>
/// <param name="InputSensitiveType">The moderation type of the input audio of a MiniMax
/// voice clone: 0 normal, 1 severe, 2 porn, 3 advertising, 4 prohibited, 5 abuse,
/// 6 violence / terrorism, 7 other.</param>
/// <param name="DemoAudio">The url of the audition audio of a MiniMax voice clone.</param>
public record MultimodalOutput(
    List<MultimodalChoice> Choices,
    DashScopeWebSearchInfo? SearchInfo = null,
    List<ToolInfoOutput>? ToolInfo = null,
    MultimodalTaskMetric? TaskMetric = null,
    string? FinishReason = null,
    SpeechSynthesisAudio? Audio = null,
    MinimaxBaseResp? BaseResp = null,
    MinimaxSpeechData? Data = null,
    MinimaxSpeechExtraInfo? ExtraInfo = null,
    string? TraceId = null,
    bool? InputSensitive = null,
    int? InputSensitiveType = null,
    string? DemoAudio = null);
