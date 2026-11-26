namespace Cnblogs.DashScope.Core;

/// <summary>
/// A model of Bailian.
/// </summary>
/// <param name="Model">The model id used in the API calls, e.g. <c>qwen3.8-max</c>.</param>
/// <param name="Name">The display name of the model.</param>
/// <param name="Description">What the model does.</param>
/// <param name="Provider">The author of the model, e.g. <c>qwen</c>, <c>deepseek</c>, <c>moonshot-ai</c>.</param>
/// <param name="Capabilities">What the model can do, e.g. <c>TG</c> (text generation), <c>Reasoning</c>,
/// <c>VU</c> (visual understanding), <c>Multimodal-Omni</c>, <c>IG</c> (image generation).</param>
/// <param name="InferenceProvider">Who serves the inference: <c>aliyun-bailian</c> for the models hosted by
/// Bailian, otherwise the third party supplying the model directly (e.g. <c>zhipu-ai</c>).</param>
/// <param name="Features">The features the model supports, e.g. <c>function-calling</c>,
/// <c>structured-outputs</c>, <c>web-search</c>, <c>cache</c>.</param>
/// <param name="Prices">The prices of the model, one entry per billing range.</param>
/// <param name="PublishedTime">When the model was published, in <c>yyyy-MM-dd HH:mm:ss</c>.</param>
/// <param name="InferenceMetadata">The input and output modalities of the model.</param>
/// <param name="ModelInfo">The context window and the token limits of the model.</param>
/// <param name="EquivalentSnapshot">The snapshot model a version alias points to, e.g.
/// <c>qwen3.7-plus</c> points to <c>qwen3.7-plus-2026-05-26</c>. null when the model is not an alias.</param>
/// <param name="InferenceOfflineInfo">The retirement schedule of the model. null when no retirement is scheduled.</param>
public record DashScopeModel(
    string Model,
    string Name,
    string Description,
    string Provider,
    List<string> Capabilities,
    string InferenceProvider,
    List<string> Features,
    List<DashScopeModelPriceRange> Prices,
    string? PublishedTime,
    DashScopeModelInferenceMetadata InferenceMetadata,
    DashScopeModelInfo ModelInfo,
    string? EquivalentSnapshot,
    DashScopeModelOfflineInfo? InferenceOfflineInfo);
