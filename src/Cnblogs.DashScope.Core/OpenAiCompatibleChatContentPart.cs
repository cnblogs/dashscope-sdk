namespace Cnblogs.DashScope.Core;

/// <summary>
/// A content part of an OpenAI compatible chat message: text, image_url, input_audio or
/// video_url. Which types a model accepts varies.
/// </summary>
/// <param name="Type">The type of the part: text, image_url, input_audio or video_url.</param>
/// <param name="Text">The text. text parts.</param>
/// <param name="ImageUrl">The image. image_url parts.</param>
/// <param name="InputAudio">The audio. input_audio parts.</param>
/// <param name="VideoUrl">The video. video_url parts.</param>
public record OpenAiCompatibleChatContentPart(
    string Type,
    string? Text = null,
    OpenAiCompatibleChatImageUrl? ImageUrl = null,
    OpenAiCompatibleChatInputAudio? InputAudio = null,
    OpenAiCompatibleChatVideoUrl? VideoUrl = null);
