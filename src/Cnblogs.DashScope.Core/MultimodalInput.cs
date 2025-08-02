using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents inputs of a multi-model generation request.
/// </summary>
public class MultimodalInput : IDashScopeOssUploadConfig
{
    /// <summary>
    /// The messages of context, model will generate from last user message.
    /// </summary>
    public IEnumerable<MultimodalMessage> Messages { get; set; } = Array.Empty<MultimodalMessage>();

    /// <inheritdoc />
    public bool EnableOssResolve() => Messages.Any(m => m.IsOss());
}
