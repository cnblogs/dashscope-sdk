namespace Cnblogs.DashScope.Core;

/// <summary>
/// A single message for application call.
/// </summary>
/// <param name="Role">The role of this message belongs to.</param>
/// <param name="Content">The content of the message.</param>
public record ApplicationMessage(string Role, string Content)
{
    /// <summary>
    /// Creates a user message.
    /// </summary>
    /// <param name="content">Content of the message.</param>
    /// <returns></returns>
    public static ApplicationMessage User(string content) => new("user", content);

    /// <summary>
    /// Creates a system message.
    /// </summary>
    /// <param name="content">Content of the message.</param>
    /// <returns></returns>
    public static ApplicationMessage System(string content) => new("system", content);

    /// <summary>
    /// Creates a assistant message.
    /// </summary>
    /// <param name="content">Content of the message.</param>
    /// <returns></returns>
    public static ApplicationMessage Assistant(string content) => new("assistant", content);
}
