namespace Cnblogs.DashScope.Core;

/// <summary>
/// The output of a multimodal generation task, e.g. wan2.6 t2i tasks created via
/// <see cref="IDashScopeClient.CreateImageGenerationTaskAsync(ModelRequest{MultimodalInput, IMultimodalParameters}, CancellationToken)"/>.
/// </summary>
public record MultimodalTaskOutput : DashScopeTaskOutput
{
    /// <summary>
    /// The generated choices, available when <see cref="DashScopeTaskOutput.TaskStatus"/> is succeeded.
    /// </summary>
    public List<MultimodalChoice>? Choices { get; set; }
}
