namespace Cnblogs.DashScope.Sdk.Internals;

internal interface IMessage<TContent>
    where TContent : class
{
    /// <summary>
    /// Must be one of <c>system</c>, <c>user</c> or <c>assistant</c>.
    /// </summary>
    public string Role { get; }

    /// <summary>
    /// The content of message.
    /// </summary>
    public TContent Content { get; }
}
