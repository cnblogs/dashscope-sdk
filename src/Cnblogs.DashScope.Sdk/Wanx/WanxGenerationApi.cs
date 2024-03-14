using Cnblogs.DashScope.Core;

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
        var input = new ImageSynthesisInput { Prompt = prompt, NegativePrompt = negativePrompt };
        var response = await dashScopeClient.CreateImageSynthesisTaskAsync(
            new ModelRequest<ImageSynthesisInput, IImageSynthesisParameters>
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

    /// <summary>
    /// Creates a wanx image generation task with given model and input.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="input">The input for image to be generate from.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The generated task.</returns>
    public static Task<DashScopeTaskOutput> CreateWanxImageGenerationTaskAsync(
        this IDashScopeClient dashScopeClient,
        WanxStyleRepaintModel model,
        ImageGenerationInput input,
        CancellationToken cancellationToken = default)
    {
        return dashScopeClient.CreateWanxImageGenerationTaskAsync(model.GetModelName(), input, cancellationToken);
    }

    /// <summary>
    /// Creates a wanx image generation task with given model and input.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="input">The input for image to be generate from.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>The generated task.</returns>
    public static async Task<DashScopeTaskOutput> CreateWanxImageGenerationTaskAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        ImageGenerationInput input,
        CancellationToken cancellationToken = default)
    {
        var response = await dashScopeClient.CreateImageGenerationTaskAsync(
            new ModelRequest<ImageGenerationInput> { Model = model, Input = input },
            cancellationToken);
        return response.Output;
    }

    /// <summary>
    /// Get wanx image generation task detail.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="taskId">The task id to query.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public static Task<DashScopeTask<ImageGenerationOutput, ImageGenerationUsage>>
        GetWanxImageGenerationTaskAsync(
            this IDashScopeClient dashScopeClient,
            string taskId,
            CancellationToken cancellationToken = default)
    {
        return dashScopeClient.GetTaskAsync<ImageGenerationOutput, ImageGenerationUsage>(taskId, cancellationToken);
    }

    /// <summary>
    /// Create wanx background generation task.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="input">The input that generate background images from.</param>
    /// <param name="parameters">Optional parameters for generation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public static async Task<DashScopeTaskOutput> CreateWanxBackgroundGenerationTaskAsync(
        this IDashScopeClient dashScopeClient,
        WanxBackgroundGenerationModel model,
        BackgroundGenerationInput input,
        BackgroundGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var response = await dashScopeClient.CreateBackgroundGenerationTaskAsync(
            new()
            {
                Model = model.GetModelName(),
                Input = input,
                Parameters = parameters
            },
            cancellationToken);
        return response.Output;
    }

    /// <summary>
    /// Create wanx background generation task.
    /// </summary>
    /// <param name="dashScopeClient">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="model">The model to use.</param>
    /// <param name="input">The input that generate background images from.</param>
    /// <param name="parameters">Optional parameters for generation.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public static async Task<DashScopeTaskOutput> CreateWanxBackgroundGenerationTaskAsync(
        this IDashScopeClient dashScopeClient,
        string model,
        BackgroundGenerationInput input,
        BackgroundGenerationParameters? parameters = null,
        CancellationToken cancellationToken = default)
    {
        var response = await dashScopeClient.CreateBackgroundGenerationTaskAsync(
            new()
            {
                Model = model,
                Input = input,
                Parameters = parameters
            },
            cancellationToken);
        return response.Output;
    }

    /// <summary>
    /// Query wanx background generation task status.
    /// </summary>
    /// <param name="client">The <see cref="IDashScopeClient"/>.</param>
    /// <param name="taskId">The task id to query.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns></returns>
    public static Task<DashScopeTask<BackgroundGenerationOutput, BackgroundGenerationUsage>>
        GetWanxBackgroundGenerationTaskAsync(
            this IDashScopeClient client,
            string taskId,
            CancellationToken cancellationToken = default)
    {
        return client.GetTaskAsync<BackgroundGenerationOutput, BackgroundGenerationUsage>(taskId, cancellationToken);
    }
}
