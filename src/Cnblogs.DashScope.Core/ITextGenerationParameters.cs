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
        IThinkingParameter,
        IFunctionCallParameter,
        IStructuredOutputParameter,
        IWebSearchParameter,
        ICodeInterpreterParameter
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
    /// Include log possibilities in response.
    /// </summary>
    bool? Logprobs { get; set; }

    /// <summary>
    /// How many choices should be returned. Range: [0, 5]
    /// </summary>
    int? TopLogprobs { get; set; }

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
}
