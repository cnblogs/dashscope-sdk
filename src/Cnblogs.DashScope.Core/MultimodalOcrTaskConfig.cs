namespace Cnblogs.DashScope.Core;

/// <summary>
/// Configuration of OCR task.
/// </summary>
public class MultimodalOcrTaskConfig
{
    /// <summary>
    /// The resulting JSON schema, value should be empty string.
    /// </summary>
    /// <example>
    /// <code>
    ///     var schema = new Dictionary&lt;string, object&gt;()
    ///     {
    ///         {
    ///             "收件人信息",
    ///             new Dictionary&lt;string, object&gt;()
    ///             {
    ///                 "收件人姓名", string.Empty,
    ///                 "收件人电话号码", string.Empty,
    ///                 "收件人地址", string.Empty
    ///             }
    ///         }
    ///     }
    /// </code>
    /// </example>
    public Dictionary<string, object>? ResultSchema { get; set; }
}
