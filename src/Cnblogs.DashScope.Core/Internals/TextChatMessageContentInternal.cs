namespace Cnblogs.DashScope.Core.Internals;

internal class TextChatMessageContentInternal
{
    public string Type { get; set; } = "text";
    public string? Text { get; set; }
    public List<string>? DocUrl { get; set; }
    public string? FileParsingStrategy { get; set; }
}
