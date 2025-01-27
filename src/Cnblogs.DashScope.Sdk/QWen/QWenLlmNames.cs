namespace Cnblogs.DashScope.Sdk.QWen;

internal static class QWenLlmNames
{
    public static string GetModelName(this QWenLlm llm)
    {
        return llm switch
        {
            QWenLlm.QWenTurbo => "qwen-turbo",
            QWenLlm.QWenPlus => "qwen-plus",
            QWenLlm.QWenMax => "qwen-max",
            QWenLlm.QWenMax1201 => "qwen-max-1201",
            QWenLlm.QWenMaxLongContext => "qwen-max-longcontext",
            QWenLlm.QWen1_5_72BChat => "qwen1.5-72b-chat",
            QWenLlm.QWen1_5_14BChat => "qwen1.5-14b-chat",
            QWenLlm.QWen1_5_7BChat => "qwen1.5-7b-chat",
            QWenLlm.QWen72BChat => "qwen-72b-chat",
            QWenLlm.QWen14BChat => "qwen-14b-chat",
            QWenLlm.QWen7BChat => "qwen-7b-chat",
            QWenLlm.QWen1_8BLongContextChat => "qwen-1.8b-longcontext-chat",
            QWenLlm.QWen1_8Chat => "qwen-1.8b-chat",
            QWenLlm.QWenLong => "qwen-long",
            QWenLlm.QWenCoderPlus => "qwen-coder-plus",
            QWenLlm.QWenCoderPlusLatest => "qwen-coder-plus-latest",
            QWenLlm.QWenCoderTurbo => "qwen-coder-turbo",
            QWenLlm.QWenCoderTurboLatest => "qwen-coder-turbo-latest",
            QWenLlm.QWenMath => "qwen-math-plus",
            QWenLlm.QWenMathLatest => "qwen-math-plus-latest",
            QWenLlm.QWenMaxLatest => "qwen-max-latest",
            QWenLlm.QWenPlusLatest => "qwen-plus-latest",
            QWenLlm.QWenTurboLatest => "qwen-turbo-latest",
            QWenLlm.QwQ32BPreview => "qwq-32b-preview",
            QWenLlm.QwQ72BPreview => "qwq-72b-preview",
            _ => ThrowHelper.UnknownModelName(nameof(llm), llm)
        };
    }
}
