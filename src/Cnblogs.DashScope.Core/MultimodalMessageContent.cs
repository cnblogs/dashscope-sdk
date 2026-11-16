namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents one content of a <see cref="MultimodalMessage"/>.
/// </summary>
/// <param name="Image">Image url.</param>
/// <param name="Text">Text content.</param>
/// <param name="Audio">Audio url.</param>
/// <param name="Video">Video urls.</param>
/// <param name="MinPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
/// <param name="MaxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
/// <param name="EnableRotate">For qwen-vl-ocr only. Rotate before ocr.</param>
/// <param name="Fps">For video content, model will read the video by 1/fps seconds; for frame sequence, indicate that the frame is captured by 1/fps seconds.</param>
/// <param name="TotalPixels">For video content or frame sequence, the maximum total pixels of all frames (pixels per frame * frame count). Frames are scaled down when the limit is exceeded, but the pixels of a single frame always stay within [min_pixels, max_pixels]. Defaults to 134217728 for qwen3-vl-plus series models.</param>
/// <param name="MaxFrames">For video content or frame sequence, the maximum number of frames to extract. When the frame count calculated by <c>fps</c> exceeds this limit, frames are evenly sampled within it. Defaults to 2000 for qwen3-vl-plus series models.</param>
/// <param name="OcrResult">Extra data from OCR task.</param>
/// <param name="ImageHw">Size of input images after resizing, in [height, width] pixels. Only present in the response when <see cref="IMultimodalParameters.VlEnableImageHwOutput"/> is set, one entry per input image.</param>
/// <param name="CacheControl">Mark an explicit cache breakpoint on this content block, the model caches the prefix from the messages array to this content block.</param>
public record MultimodalMessageContent(
    string? Image = null,
    string? Text = null,
    string? Audio = null,
    MultimodalMessageVideoContent? Video = null,
    int? MinPixels = null,
    int? MaxPixels = null,
    bool? EnableRotate = null,
    float? Fps = null,
    int? TotalPixels = null,
    int? MaxFrames = null,
    MultimodalOcrResult? OcrResult = null,
    int[][]? ImageHw = null,
    CacheControlOptions? CacheControl = null)
{
    private const string OssSchema = "oss://";

    /// <summary>
    /// Represents an image content.
    /// </summary>
    /// <param name="url">Image url.</param>
    /// <param name="minPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
    /// <param name="maxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
    /// <param name="enableRotate">For OCR models only. Auto rotate images before OCR.</param>
    /// <returns></returns>
    public static MultimodalMessageContent ImageContent(
        string url,
        int? minPixels = null,
        int? maxPixels = null,
        bool? enableRotate = null)
    {
        return new MultimodalMessageContent(url, MinPixels: minPixels, MaxPixels: maxPixels, EnableRotate: enableRotate);
    }

    /// <summary>
    /// Represents an image content.
    /// </summary>
    /// <param name="bytes">Image binary to sent using base64 data uri.</param>
    /// <param name="mediaType">Image media type.</param>
    /// <param name="minPixels">For qwen-vl-ocr only. Minimal pixels for ocr task.</param>
    /// <param name="maxPixels">For qwen-vl-ocr only. Maximum pixels for ocr task.</param>
    /// <param name="enableRotate">For OCR models only. Auto rotate images before OCR.</param>
    /// <returns></returns>
    public static MultimodalMessageContent ImageContent(
        ReadOnlySpan<byte> bytes,
        string mediaType,
        int? minPixels = null,
        int? maxPixels = null,
        bool? enableRotate = null)
    {
        return ImageContent(
            $"data:{mediaType};base64,{Convert.ToBase64String(bytes)}",
            minPixels,
            maxPixels,
            enableRotate);
    }

    /// <summary>
    /// Represents a text content.
    /// </summary>
    /// <param name="text">Text content.</param>
    /// <param name="cacheControl">Mark an explicit cache breakpoint on the text content.</param>
    /// <returns></returns>
    public static MultimodalMessageContent TextContent(string text, CacheControlOptions? cacheControl = null)
    {
        return new MultimodalMessageContent(Text: text, CacheControl: cacheControl);
    }

    /// <summary>
    /// Represents an audio content.
    /// </summary>
    /// <param name="audioUrl">The url of the audio.</param>
    /// <returns></returns>
    public static MultimodalMessageContent AudioContent(string audioUrl)
    {
        return new MultimodalMessageContent(Audio: audioUrl);
    }

    /// <summary>
    /// Represents video contents.
    /// </summary>
    /// <param name="frames">The urls of the frames.</param>
    /// <param name="fps">The fps of the frame</param>
    /// <returns></returns>
    public static MultimodalMessageContent VideoFrames(IEnumerable<string> frames, int? fps = null)
    {
        return new MultimodalMessageContent(Video: MultimodalMessageVideoContent.FrameSequence(frames), Fps: fps);
    }

    /// <summary>
    /// Represents a video content.
    /// </summary>
    /// <param name="url">The url of the video.</param>
    /// <param name="fps">The fps for modal to capture frames by.</param>
    /// <returns></returns>
    public static MultimodalMessageContent VideoContent(string url, int? fps = null)
    {
        return new MultimodalMessageContent(Video: MultimodalMessageVideoContent.Video(url), Fps: fps);
    }

    internal bool IsOss()
        => Image?.StartsWith(OssSchema) == true
           || Audio?.StartsWith(OssSchema) == true
           || Video?.Urls.Any(v => v.StartsWith(OssSchema)) == true;
}
