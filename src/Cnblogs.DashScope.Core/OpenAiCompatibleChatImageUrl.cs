namespace Cnblogs.DashScope.Core;

/// <summary>
/// The image of an image_url content part.
/// </summary>
/// <param name="Url">The url of the image, or a base64 data uri.</param>
public record OpenAiCompatibleChatImageUrl(
    string? Url = null);
