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
    QWenMaxLongContext = 5,

    /// <summary>
    /// qwen1.5-72b-chat, input token limit is 30k.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    QWen1_5_72BChat = 6,

    /// <summary>
    /// qwen1.5-14b-chat, input token limit is 6k.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    QWen1_5_14BChat = 7,

    /// <summary>
    /// qwen1.5-7b-chat, input token limit is 6k.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    QWen1_5_7BChat = 8,

    /// <summary>
    /// qwen-72b-chat, input token limit is 30k.
    /// </summary>
    QWen72BChat = 9,

    /// <summary>
    /// qwen-14b-chat, input token limit is 6k.
    /// </summary>
    QWen14BChat = 10,

    /// <summary>
    /// qwen-7b-chat, input token limit is 6k.
    /// </summary>
    QWen7BChat = 11,

    /// <summary>
    /// qwen-1.8b-longcontext-chat, input token limit is 30k.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    QWen1_8BLongContextChat = 12,

    /// <summary>
    /// qwen-1.8b-chat, input token limit is 6k.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    QWen1_8Chat = 13,

    /// <summary>
    /// qwen-long, input limit 10,000,000 token
    /// </summary>
    QWenLong = 14,

    /// <summary>
    /// qwen-coder-turbo
    /// </summary>
    QWenCoderTurbo = 15,

    /// <summary>
    /// qwen-math-plus
    /// </summary>
    QWenMath = 16,

    /// <summary>
    /// qwen-coder-plus
    /// </summary>
    QWenCoderPlus = 17,

    /// <summary>
    /// qwen-max-latest
    /// </summary>
    QWenMaxLatest = 18,

    /// <summary>
    /// qwen-turbo-latest
    /// </summary>
    QWenTurboLatest = 19,

    /// <summary>
    /// qwen-plus-latest
    /// </summary>
    QWenPlusLatest = 20,

    /// <summary>
    /// qwq-32b-preview
    /// </summary>
    QwQ32BPreview = 21,

    /// <summary>
    /// qwen-math-plus-latest
    /// </summary>
    QWenMathLatest = 22,

    /// <summary>
    /// qwen-coder-plus-latest
    /// </summary>
    QWenCoderPlusLatest = 23,

    /// <summary>
    /// qwen-coder-turbo-latest
    /// </summary>
    QWenCoderTurboLatest = 24,

    /// <summary>
    /// qvq-72b-preview
    /// </summary>
    QvQ72BPreview = 25,

    /// <summary>
    /// qwq-32b
    /// </summary>
    QwQ32B = 26
}
