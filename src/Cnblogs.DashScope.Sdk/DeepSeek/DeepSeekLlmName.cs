namespace Cnblogs.DashScope.Sdk.DeepSeek;

internal static class DeepSeekLlmName
{
    public static string GetModelName(this DeepSeekLlm model)
    {
        return model switch
        {
            DeepSeekLlm.DeepSeekR1 => "deepseek-r1",
            DeepSeekLlm.DeepSeekV3 => "deepseek-v3",
            _ => ThrowHelper.UnknownModelName(nameof(model), model)
        };
    }
}
