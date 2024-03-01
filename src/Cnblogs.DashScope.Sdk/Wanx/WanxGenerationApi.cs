namespace Cnblogs.DashScope.Sdk.Wanx;

/// <summary>
/// Wanx painting model
/// </summary>
public static class WanxGenerationApi
{
    /// <summary>
    /// Create a wanx image synthesis task
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="prompt">The prompt to generate image from.</param>
    /// <param name="negativePrompt">The negative prompt to generate image from.</param>
    /// <param name="parameters">Optional parameters of image generation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>A pending task, use the TaskId to query for status.</returns>
    public static Task<DashScopeTaskOutput> CreateWanxImageSynthesisTaskAsync(
        this IDashScopeClient dashScopeClient,
        WanxModel model,
        string prompt,
        string? negativePrompt = null,
        ImageSynthesisParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.CreateWanxImageSynthesisTaskAsync(
            model.GetModelName(),
            prompt,
            negativePrompt,
            parameters,
            cancellationToken);
    }

    /// <summary>
    /// Create a wanx image synthesis task
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="prompt">The prompt to generate image from.</param>
    /// <param name="negativePrompt">The negative prompt to generate image from.</param>
    /// <param name="parameters">Optional parameters of image generation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>A pending task, use the TaskId to query for status.</returns>
    public static async Task<DashScopeTaskOutput> CreateWanxImageSynthesisTaskAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        string prompt,
        string? negativePrompt = null,
        ImageSynthesisParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var input = new ImageSynthesisInput() { Prompt = prompt, NegativePrompt = negativePrompt };
        var response = await dashScopeClient.CreateImageSynthesisTaskAsync(
            new ModelRequest<ImageSynthesisInput, ImageSynthesisParameters>()
            {
                Model = model,
                Input = input,
                Parameters = parameters
            },
            cancellationToken);
        return response.Output;
    }

    /// <summary>
    /// Query for task status of image synthesis task.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="taskId">The task id.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The task status.</returns>
    public static Task<DashScopeTask<ImageSynthesisOutput, ImageSynthesisUsage>> GetWanxImageSynthesisTaskAsync(
        this IDashScopeClient dashScopeClient,
        string taskId,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTaskAsync<ImageSynthesisOutput, ImageSynthesisUsage>(taskId, cancellationToken);
    }
}
