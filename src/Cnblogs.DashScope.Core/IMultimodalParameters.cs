namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for multimodel generation request.
/// </summary>
public interface IMultimodalParameters
    : IProbabilityParameter,
        IIncrementalOutputParameter,
        IPenaltyParameter,
        IMaxTokenParameter,
        IStopTokenParameter,
        IThinkingParameter,
        IFunctionCallParameter,
        IStructuredOutputParameter,
        IWebSearchParameter,
        ICodeInterpreterParameter,
        IImageSynthesisParameters,
        IToolParameter
{
    /// <summary>
    /// Allow higher resolution for inputs. When setting to <c>true</c>, increases the maximum input token from 1280 to 16384. Defaults to <c>false</c>.
    /// </summary>
    bool? VlHighResolutionImages { get; set; }

    /// <summary>
    /// Include the size of input images after resizing in the response message content, in [height, width] pixels.
    /// Only available for qwen-vl models. When streaming, the size is returned in the last chunk. Defaults to <c>false</c>.
    /// </summary>
    bool? VlEnableImageHwOutput { get; set; }

    /// <summary>
    /// Options for speech recognition.
    /// </summary>
    AsrOptions? AsrOptions { get; set; }

    /// <summary>
    /// Format of the audio to recognize, e.g. wav, mp3 or opus. Required by recorded speech
    /// recognition of the Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash models.
    /// </summary>
    string? Format { get; set; }

    /// <summary>
    /// Sample rate of the audio to recognize in Hz, e.g. <c>"16000"</c> for 16 kHz.
    /// Recorded speech recognition of the Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash models.
    /// </summary>
    string? SampleRate { get; set; }

    /// <summary>
    /// ID of the pre-compiled vocabulary (hot word) list to use, which is created via the
    /// vocabulary API in advance and reused across requests. Recorded speech recognition
    /// of the Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash models.
    /// </summary>
    string? VocabularyId { get; set; }

    /// <summary>
    /// The instant hot words, which need no vocabulary list: the key is the hot word, the value
    /// is its weight. The weight is in range [1, 5] (the higher, the more the model tends to
    /// output the word) or 50 for super hot words, which have a much higher recall rate (at
    /// most 50 of them). Merged with the list of <see cref="VocabularyId"/> when both are
    /// set, at most 2000 words are used. Recorded speech recognition of the
    /// qwen-audio-3.x-asr-flash models.
    /// </summary>
    Dictionary<string, int>? Vocabulary { get; set; }

    /// <summary>
    /// Languages of the audio to recognize. Leave unset to let the model detect automatically.
    /// At most 4 values take effect for Qwen-Audio-3.x-ASR-Flash models, and only the first one
    /// takes effect for Fun-ASR-Flash models. Recorded speech recognition of the
    /// Fun-ASR-Flash / Qwen-Audio-3.x-ASR-Flash models.
    /// </summary>
    List<string>? LanguageHints { get; set; }

    /// <summary>
    /// Whether to enable speaker diarization, defaults to false. When enabled, speakers are
    /// told apart by <see cref="SpeechRecognitionSentence.SpeakerId"/>, all sentences are returned
    /// in <see cref="MultimodalOutput.Sentences"/>, and <see cref="KeepDialect"/> /
    /// <see cref="LanguageHints"/> stop taking effect. Recorded speech recognition of
    /// qwen-audio-3.1-asr-flash only.
    /// </summary>
    bool? SpeakerDiarizationEnabled { get; set; }

    /// <summary>
    /// Whether to keep dialect expressions in the result, defaults to false, which transcribes
    /// dialects into Mandarin text. Recorded speech recognition of qwen-audio-3.1-asr-flash only.
    /// </summary>
    bool? KeepDialect { get; set; }

    /// <summary>
    /// Extra configurations for OCR models.
    /// </summary>
    MultimodalOcrOptions? OcrOptions { get; set; }

    /// <summary>
    /// Negative prompt when doing image generation task.
    /// </summary>
    string? NegativePrompt { get; set; }

    /// <summary>
    /// Enable interleaved text-image output for wan image models (e.g. wan2.6-image).
    /// When <c>false</c> (default), the request runs in image editing mode, which requires 1~4 input images;
    /// when <c>true</c>, the model generates text-image interleaved content, with at most 1 input image.
    /// </summary>
    bool? EnableInterleave { get; set; }

    /// <summary>
    /// Enable thinking mode for wan2.7 image models.
    /// </summary>
    bool? ThinkingMode { get; set; }

    /// <summary>
    /// Enable sequential image group generation for wan2.7 image models. When enabled,
    /// <c>n</c> becomes the max number of generated images (1-12, defaults to 12).
    /// </summary>
    bool? EnableSequential { get; set; }

    /// <summary>
    /// Bounding boxes for interactive editing of wan2.7 image models. One list of boxes per input image
    /// (empty list when the image has no selection), each box is [x1, y1, x2, y2] in pixels.
    /// </summary>
    List<List<List<int>>>? BboxList { get; set; }

    /// <summary>
    /// Color palette for wan2.7 image models, ratios must sum up to 100.00%.
    /// </summary>
    List<MultimodalColorPalette>? ColorPalette { get; set; }

    /// <summary>
    /// How <c>prompt_extend</c> rewrites the positive prompt for qwen-image edit models:
    /// <c>direct</c> (default, DPE) or <c>agent</c> (APE, text-to-image only).
    /// </summary>
    string? PromptExtendMode { get; set; }

    /// <summary>
    /// The aspect ratio of the generated image: 16:9 (default), 9:16 or 1:1. Kling image models only.
    /// </summary>
    string? AspectRatio { get; set; }

    /// <summary>
    /// The resolution tier of the generated image: 1k (default), 2k or 4k (kling-v3-omni only).
    /// Kling image models only.
    /// </summary>
    string? Resolution { get; set; }

    /// <summary>
    /// The type of generated images for kling-v3-omni: single (default, independent images)
    /// or series (a storyboard series that keeps character/scene/narrative continuity).
    /// </summary>
    string? ResultType { get; set; }

    /// <summary>
    /// The number of images in series mode for kling-v3-omni, 2-9, defaults to 4.
    /// Only effective when <see cref="ResultType"/> is series.
    /// </summary>
    int? SeriesAmount { get; set; }
}
