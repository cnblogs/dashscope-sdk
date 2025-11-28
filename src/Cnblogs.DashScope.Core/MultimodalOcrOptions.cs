namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for OCR model.
/// </summary>
public class MultimodalOcrOptions
{
    /// <summary>
    /// Name of the task.
    /// </summary>
    /// <example>
    /// Some task example: "text_recognition", "key_information_extraction", "document_parsing", "table_parsing"
    /// </example>
    public string? Task { get; set; }

    /// <summary>
    /// Config for the task.
    /// </summary>
    public MultimodalOcrTaskConfig? TaskConfig { get; set; }
}
