namespace Cnblogs.DashScope.Sdk.Llama2;

internal static class Llama2ModelNames
{
    public static string GetModelName(this Llama2Model model)
    {
        return model switch
        {
            Llama2Model.Chat7Bv2 => "llama2-7b-chat-v2",
            Llama2Model.Chat13Bv2 => "llama2-13b-chat-v2",
            _ => throw new ArgumentOutOfRangeException(
                nameof(model),
                model,
                "Unknown model type, please use the overload that accepts a string ‘model’ parameter.")
        };
    }
}
