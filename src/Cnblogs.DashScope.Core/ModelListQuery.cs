namespace Cnblogs.DashScope.Core;

/// <summary>
/// The filters and the page of a model list request, see
/// https://docs.bailian.console.aliyun.com/zh/model-studio/list-models. A filter that is left unset
/// is not sent, the service then returns every model of the page.
/// </summary>
public class ModelListQuery
{
    /// <summary>
    /// The model name to search for, e.g. <c>qwen</c>. The match is fuzzy.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The model id to look up, e.g. <c>qwen3.8-max</c>. The match is exact.
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// The language of the model names, descriptions and price names: <c>zh-CN</c> (default) or <c>en-US</c>.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// The page to return, starting from 1. Defaults to 1.
    /// </summary>
    public int? PageNo { get; set; }

    /// <summary>
    /// The number of models per page, at most 200. Defaults to 20.
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// The model authors to return, e.g. <c>qwen</c>, <c>deepseek</c>, <c>moonshot-ai</c>.
    /// </summary>
    public List<string>? Providers { get; set; }

    /// <summary>
    /// The inference service providers to return, e.g. <c>aliyun-bailian</c> for the models hosted by
    /// Bailian, or the third party supplying a model directly (e.g. <c>zhipu-ai</c>).
    /// </summary>
    public List<string>? InferenceProviders { get; set; }

    /// <summary>
    /// The capabilities to return, a model is returned when it supports any of them, e.g. <c>TG</c>
    /// (text generation), <c>Reasoning</c>, <c>VU</c> (visual understanding), <c>IG</c> (image generation).
    /// </summary>
    public List<string>? Capabilities { get; set; }

    /// <summary>
    /// The features to return, e.g. <c>function-calling</c>, <c>structured-outputs</c>, <c>web-search</c>.
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// Return the models whose context window is smaller than this value.
    /// </summary>
    public int? ContextWindow { get; set; }

    /// <summary>
    /// The service site to return, e.g. <c>global</c>, <c>international</c>, <c>asia-pacific-china</c>.
    /// </summary>
    public string? ServiceSite { get; set; }

    /// <summary>
    /// The use cases the models must support: <c>inference</c> (default) or <c>deploy</c>.
    /// </summary>
    public List<string>? Supports { get; set; }

    /// <summary>
    /// The deployment methods to return, e.g. <c>ptu</c>.
    /// </summary>
    public List<string>? DeploymentMethods { get; set; }

    /// <summary>
    /// The PTU service tiers to return, requires <c>ptu</c> in <see cref="DeploymentMethods"/>.
    /// </summary>
    public List<string>? DeploymentPtuServiceTiers { get; set; }
}
