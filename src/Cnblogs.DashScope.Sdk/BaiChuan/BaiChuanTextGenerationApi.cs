using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.BaiChuan;

/// <summary>
/// BaiChuan LLM generation apis, doc: https://help.aliyun.com/zh/dashscope/developer-reference/api-details-2
/// </summary>
public static class BaiChuanTextGenerationApi
{
    /// <summary>
    /// Get text completion from baichuan model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="llm">The llm to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetBaiChuanTextCompletionAsync(
        this IDashScopeClient client,
        BaiChuanLlm llm,
        string prompt)
    {
        return client.GetBaiChuanTextCompletionAsync(llm.GetModelName(), prompt);
    }

    /// <summary>
    /// Get text completion from baichuan model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="llm">The llm to use.</param>
    /// <param name="prompt">The prompt to generate completion from.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetBaiChuanTextCompletionAsync(
        this IDashScopeClient client,
        string llm,
        string prompt)
    {
        return client.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = llm,
                Input = new TextGenerationInput { Prompt = prompt },
                Parameters = null
            });
    }

    /// <summary>
    /// Get text completion from baichuan model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="llm">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <param name="resultFormat">Can be 'text' or 'message', defaults to 'text'. Call <see cref="ResultFormats"/> to get available options.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetBaiChuanTextCompletionAsync(
        this IDashScopeClient client,
        BaiChuan2Llm llm,
        IEnumerable<TextChatMessage> messages,
        string? resultFormat = null)
    {
        return client.GetBaiChuanTextCompletionAsync(llm.GetModelName(), messages, resultFormat);
    }

    /// <summary>
    /// Get text completion from baichuan model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="llm">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <param name="resultFormat">Can be 'text' or 'message', defaults to 'text'. Call <see cref="ResultFormats"/> to get available options.</param>
    /// <returns></returns>
    public static Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>> GetBaiChuanTextCompletionAsync(
        this IDashScopeClient client,
        string llm,
        IEnumerable<TextChatMessage> messages,
        string? resultFormat = null)
    {
        return client.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = llm,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = string.IsNullOrEmpty(resultFormat) == false
                    ? new TextGenerationParameters { ResultFormat = resultFormat }
                    : null
            });
    }
}
