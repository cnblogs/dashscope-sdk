using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests.Utils;

internal class Cases
{
    public const string CustomModelName = "custom-model";
    public const string Prompt = "hello";
    public const string PromptAlter = "world";
    public const string Uuid = "33da8e6b-1309-9a44-be83-352165959608";
    public const string ImageUrl = "https://www.cnblogs.com/image.png";

    public static readonly List<TextChatMessage> TextMessages =
        [TextChatMessage.System("you are a helpful assistant"), TextChatMessage.User("hello")];
}
