namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

internal class Cases
{
    public const string CustomModelName = "custom-model";
    public const string Prompt = "hello";

    public static readonly List<ChatMessage> TextMessages =
        [new("system", "you are a helpful assistant"), new("user", "hello")];
}
