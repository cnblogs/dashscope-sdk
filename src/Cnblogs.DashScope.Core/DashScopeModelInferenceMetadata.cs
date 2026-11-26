namespace Cnblogs.DashScope.Core;

/// <summary>
/// The input and output modalities of a model.
/// </summary>
/// <param name="RequestModality">The input modalities: <c>Text</c>, <c>Image</c>, <c>Audio</c>, <c>Video</c>.</param>
/// <param name="ResponseModality">The output modalities: <c>Text</c>, <c>Image</c>, <c>Audio</c>, <c>Video</c>.</param>
/// <param name="InferenceMode">The inference mode of the model, e.g. <c>app</c>. Only reported for a few models.</param>
public record DashScopeModelInferenceMetadata(
    List<string> RequestModality,
    List<string> ResponseModality,
    string? InferenceMode);
