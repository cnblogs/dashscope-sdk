namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The state of <see cref="DashScopeClientWebSocket"/>.
    /// </summary>
    public enum DashScopeWebSocketState
    {
        /// <summary>
        /// The socket has been created but not connected yet.
        /// </summary>
        Created,

        /// <summary>
        /// The socket has been connected and ready.
        /// </summary>
        Ready,

        /// <summary>
        /// The socket has a running task waiting to be finished.
        /// </summary>
        RunningTask,

        /// <summary>
        /// The socket has been closed.
        /// </summary>
        Closed
    }
}
