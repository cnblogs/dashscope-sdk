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
            _ => throw new ArgumentOutOfRangeException(
                nameof(llm),
                llm,
                "Unknown model type, please use the overload that accepts a string ‘model’ parameter.")
        };
    }
}
