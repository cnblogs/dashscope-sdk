using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.Llama2;

/// <summary>
/// Extensions for llama2 text generation, docs: https://help.aliyun.com/zh/dashscope/developer-reference/api-details-11
/// </summary>
public static class Llama2TextGenerationApi
{
    /// <summary>
    /// Get text completion from llama2 model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <param name="resultFormat">Can be 'text' or 'message'. Call <see cref="ResultFormats"/> to get available options.</param>
    /// <returns></returns>
    public static async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetLlama2TextCompletionAsync(
            this IDashScopeClient client,
            Llama2Model model,
            IEnumerable<TextChatMessage> messages,
            string? resultFormat = null)
    {
        return await client.GetLlama2TextCompletionAsync(model.GetModelName(), messages, resultFormat);
    }

    /// <summary>
    /// Get text completion from llama2 model.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model name.</param>
    /// <param name="messages">The context messages.</param>
    /// <param name="resultFormat">Can be 'text' or 'message'. Call <see cref="ResultFormats"/> to get available options.</param>
    /// <returns></returns>
    public static async Task<ModelResponse<TextGenerationOutput, TextGenerationTokenUsage>>
        GetLlama2TextCompletionAsync(
            this IDashScopeClient client,
            string model,
            IEnumerable<TextChatMessage> messages,
            string? resultFormat = null)
    {
        return await client.GetTextCompletionAsync(
            new ModelRequest<TextGenerationInput, ITextGenerationParameters>
            {
                Model = model,
                Input = new TextGenerationInput { Messages = messages },
                Parameters = resultFormat != null
                    ? new TextGenerationParameters { ResultFormat = resultFormat }
                    : null
            });
    }
}
