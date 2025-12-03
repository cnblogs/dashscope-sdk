namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The output of one image synthesis task.
    /// </summary>
    public record ImageSynthesisOutput : DashScopeTaskOutput
    {
    /// <summary>
    /// The generated image results.
    /// </summary>
    public List<ImageSynthesisResult>? Results { get; set; }
    }
}
