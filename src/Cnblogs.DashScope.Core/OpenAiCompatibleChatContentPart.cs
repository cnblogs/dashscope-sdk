namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of an OpenAI compatible chat message: text, image_url, input_audio,
/// video_url or video. Which types a model accepts varies.
/// </summary>
/// <param name="Type">The type of the part: text, image_url, input_audio, video_url or video.</param>
/// <param name="Text">The text. text parts.</param>
/// <param name="ImageUrl">The image. image_url parts.</param>
/// <param name="InputAudio">The audio. input_audio parts.</param>
/// <param name="VideoUrl">The video. video_url parts.</param>
/// <param name="Video">The urls of the frames of a video, in playback order. video parts.</param>
/// <param name="Fps">The frame rate that tells the model how much time passes between two
/// frames, range [0.1, 10], defaults to 2. video parts, and video_url parts of the models
/// that understand the time of a video.</param>
public record OpenAiCompatibleChatContentPart(
    string Type,
    string? Text = null,
    OpenAiCompatibleChatImageUrl? ImageUrl = null,
    OpenAiCompatibleChatInputAudio? InputAudio = null,
    OpenAiCompatibleChatVideoUrl? VideoUrl = null,
    List<string>? Video = null,
    float? Fps = null);
