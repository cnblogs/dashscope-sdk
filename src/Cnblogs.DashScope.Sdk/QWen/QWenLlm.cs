﻿namespace Cnblogs.DashScope.Sdk.QWen;

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
    QWenCoder = 15,

    /// <summary>
    /// qwen-math-plus
    /// </summary>
    QWenMath = 16
}
