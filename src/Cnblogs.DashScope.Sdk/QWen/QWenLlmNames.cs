using Cnblogs.DashScope.Sdk.Internals;

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
            _ => ThrowHelper.UnknownModelName(nameof(llm), llm)
        };
    }
}
