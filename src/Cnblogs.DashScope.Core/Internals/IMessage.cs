namespace Cnblogs.DashScope.Core.Internals
{
    internal interface IMessage<out TContent>
        where TContent : class
    {
        /// <summary>
        /// Must be one of <c>system</c>, <c>user</c> or <c>assistant</c>.
        /// </summary>
        string Role { get; }

        /// <summary>
        /// The content of message.
        /// </summary>
        TContent Content { get; }
    }
}
