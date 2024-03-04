using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk.BaiChuan;

internal static class BaiChuanLlmName
{
    public static string GetModelName(this BaiChuanLlm llm)
    {
        return llm switch
        {
            BaiChuanLlm.BaiChuan7B => "baichuan-7b-v1",
            _ => ThrowHelper.UnknownModelName(nameof(llm), llm)
        };
    }

    public static string GetModelName(this BaiChuan2Llm llm)
    {
        return llm switch
        {
            BaiChuan2Llm.BaiChuan2_7BChatV1 => "baichuan2-7b-chat-v1",
            BaiChuan2Llm.BaiChuan2_13BChatV1 => "baichuan2-13b-chat-v1",
            _ => ThrowHelper.UnknownModelName(nameof(llm), llm)
        };
    }
}
