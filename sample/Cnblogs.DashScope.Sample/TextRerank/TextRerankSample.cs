using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.TextRerank;

public abstract class TextRerankSample : ISample
{
    /// <inheritdoc />
    public string Group => "TextRerank";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);
}
