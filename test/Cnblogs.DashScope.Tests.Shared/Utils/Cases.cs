using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public class Cases
    {
        public const string CustomModelName = "custom-model";
        public const string Prompt = "hello";
        public const string PromptAlter = "world";
        public const string Uuid = "33da8e6b-1309-9a44-be83-352165959608";
        public const string ImageUrl = "https://www.cnblogs.com/image.png";

        public static readonly List<TextChatMessage> TextMessages =
            new() { TextChatMessage.System("you are a helpful assistant"), TextChatMessage.User("hello") };
    }
}
