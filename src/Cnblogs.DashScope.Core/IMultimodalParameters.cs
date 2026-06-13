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
}
