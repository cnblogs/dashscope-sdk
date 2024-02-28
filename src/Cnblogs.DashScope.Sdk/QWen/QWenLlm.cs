namespace Cnblogs.DashScope.Sdk.QWen;

/// <summary>
/// Available models for QWen.
/// </summary>
public enum QWenLlm
{
    /// <summary>
    /// qwen-turbo, input token limit is 6k.
    /// </summary>
    QWenTurbo = 1,

    /// <summary>
    /// qwen-plus, input token limit is 30k.
    /// </summary>
    QWenPlus = 2,

    /// <summary>
    /// qwen-max, input token limit is 6k.
    /// </summary>
    QWenMax = 3,

    /// <summary>
    /// qwen-max-1201, snapshot version of qwen-max, input token limit is 6k.
    /// </summary>
    QWenMax1201 = 4,

    /// <summary>
    /// qwen-max-longcontext, input token limit is 28k.
    /// </summary>
    QWenMaxLongContext = 5
}
