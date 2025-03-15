namespace Cnblogs.DashScope.Core;

/// <summary>
/// One reference for application output.
/// </summary>
/// <param name="IndexId">The index id of the doc.</param>
/// <param name="Title">Text slice title.</param>
/// <param name="DocId">Unique id of the doc been referenced.</param>
/// <param name="DocName">Name of the doc been referenced.</param>
/// <param name="Text">Referenced content.</param>
/// <param name="Images">Image URLs beed referenced.</param>
/// <param name="PageNumber">Page numbers of referenced content belongs to.</param>
public record ApplicationDocReference(
    string IndexId,
    string Title,
    string DocId,
    string DocName,
    string Text,
    List<string>? Images,
    List<int>? PageNumber);
