using System.Diagnostics.CodeAnalysis;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents one content of a <see cref="MultimodalMessage"/>.
/// </summary>
/// <param name="Image">Image url.</param>
/// <param name="Text">Text content.</param>
public record MultimodalMessageContent(
    [StringSyntax(StringSyntaxAttribute.Uri)]
    string? Image = null,
    string? Text = null,
    [StringSyntax(StringSyntaxAttribute.Uri)]
    string? Audio = null);
