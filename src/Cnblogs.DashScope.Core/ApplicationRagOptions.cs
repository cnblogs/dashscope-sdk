namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for RAG application.
/// </summary>
public class ApplicationRagOptions
{
    /// <summary>
    /// The pipelines to search from.
    /// </summary>
    public IEnumerable<string>? PipelineIds { get; set; }

    /// <summary>
    /// The ids of file to reference from.
    /// </summary>
    public IEnumerable<string>? FileIds { get; set; }

    /// <summary>
    /// Metadata filter for non-structured files.
    /// </summary>
    public Dictionary<string, string>? MetadataFilter { get; set; }

    /// <summary>
    /// Tag filter for non-structured files.
    /// </summary>
    public IEnumerable<string>? Tags { get; set; }

    /// <summary>
    /// Filter for structured files.
    /// </summary>
    public Dictionary<string, object>? StructuredFilter { get; set; }

    /// <summary>
    /// File ids for current session.
    /// </summary>
    public IEnumerable<string>? SessionFileIds { get; set; }
}
