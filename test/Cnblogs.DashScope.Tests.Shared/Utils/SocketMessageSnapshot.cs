namespace Cnblogs.DashScope.Tests.Shared.Utils
{
    public record SocketMessageSnapshot(string GroupName, string MessageName)
    {
        public string GetMessageJson()
        {
            return File.ReadAllText(Path.Combine("RawHttpData", $"socket-{GroupName}.{MessageName}.json"));
        }
    }

    public record SocketMessageSnapshot<TMessage>(string GroupName, string MessageName, TMessage Message)
        : SocketMessageSnapshot(GroupName, MessageName);
}
