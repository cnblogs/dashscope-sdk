namespace Cnblogs.DashScope.Core;

/// <summary>
/// The text generation options.
/// </summary>
public interface ITextGenerationParameters
    : IIncrementalOutputParameter,
        ISeedParameter,
        IProbabilityParameter,
        IPenaltyParameter,
        IMaxTokenParameter,
        IStopTokenParameter,
        IThinkingParameter
{
    /// <summary>
    /// The format of the result, must be <c>text</c> or <c>message</c>.
    /// </summary>
    /// <remarks>
    /// <c>text</c> - original text format.
    /// <para><c>message</c> - OpenAI compatible message format</para>
    /// </remarks>
    /// <example>
    /// Sets <c>result_format</c> to <c>message</c>
    /// <code>
    ///     parameter.ResultFormat = ResultFormats.Message;
    /// </code>
    /// </example>
    string? ResultFormat { get; }

    /// <summary>
    /// The format of response message, must be <c>text</c> or <c>json_object</c>
    /// </summary>
    /// <remarks>
    /// This property is not <see cref="ResultFormat"/>. Be sure not to confuse them.
    /// </remarks>
    /// <example>
    /// Set response format to <c>json_object</c>.
    /// <code>
    ///     parameter.ResponseFormat = DashScopeResponseFormat.Json;
    /// </code>
    /// </example>
    DashScopeResponseFormat? ResponseFormat { get; }

    /// <summary>
    /// Enable internet search when generation. Defaults to false.
    /// </summary>
    bool? EnableSearch { get; }

    /// <summary>
    /// Search options. <see cref="EnableSearch"/> should set to true.
    /// </summary>
    TextGenerationSearchOptions? SearchOptions { get; set; }

    /// <summary>
    /// Include log possibilities in response.
    /// </summary>
    bool? Logprobs { get; set; }

    /// <summary>
    /// How many choices should be returned. Range: [0, 5]
    /// </summary>
    int? TopLogprobs { get; set; }

    /// <summary>
    /// Available tools for model to call.
    /// </summary>
    IEnumerable<ToolDefinition>? Tools { get; }

    /// <summary>
    /// Behavior when choosing tools.
    /// </summary>
    ToolChoice? ToolChoice { get; }

    /// <summary>
    /// Whether to enable parallel tool calling
    /// </summary>
    bool? ParallelToolCalls { get; }

    /// <summary>
    /// Options when using QWen-MT models.
    /// </summary>
    TextGenerationTranslationOptions? TranslationOptions { get; set; }

    /// <summary>
    /// Cache options when using qwen-coder models.
    /// </summary>
    CacheControlOptions? CacheControl { get; set; }

    /// <summary>
    /// How many choices should model generates
    /// </summary>
    int? N { get; set; }

    /// <summary>
    /// Set logic bias for tokens, -100=ban the token, 100=must choose the token(will causing model looping this token)
    /// </summary>
    /// <remarks>
    ///     About available token list, use this link: https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250908/xtsxix/logit_bias_id%E6%98%A0%E5%B0%84%E8%A1%A8.json
    ///     or visit the official doc for more information: https://help.aliyun.com/zh/model-studio/role-play
    /// </remarks>
    Dictionary<string, int>? LogitBias { get; set; }

    /// <summary>
    /// Allow model to call internal Python interpreter. Can not use with tools.
    /// </summary>
    bool? EnableCodeInterpreter { get; set; }
}
