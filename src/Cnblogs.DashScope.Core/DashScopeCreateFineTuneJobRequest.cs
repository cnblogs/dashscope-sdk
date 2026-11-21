namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents the request payload for creating a fine-tune job via <see cref="IDashScopeClient.CreateFineTuneJobAsync"/>.
/// </summary>
public class DashScopeCreateFineTuneJobRequest
{
    /// <summary>
    /// The base model to fine-tune, e.g. <c>facechain-finetune</c>.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// The training files, 1~10 items, either public urls or file ids uploaded via
    /// <see cref="IDashScopeClient.UploadFilesAsync"/> (or both).
    /// </summary>
    public List<string> TrainingFileIds { get; set; } = new();
}
