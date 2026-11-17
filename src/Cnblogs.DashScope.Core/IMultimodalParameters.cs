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
}
