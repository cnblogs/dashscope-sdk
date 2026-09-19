namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for LingQue CCAI Conversation Analysis AIO (ContactCenterAI) client.
/// </summary>
public class ContactCenterAiOptions
{
    /// <summary>
    /// AccessKey ID used for ACS3 signing.
    /// </summary>
    public string AccessKeyId { get; set; } = string.Empty;

    /// <summary>
    /// AccessKey Secret used for ACS3 signing.
    /// </summary>
    public string AccessKeySecret { get; set; } = string.Empty;

    /// <summary>
    /// Optional STS security token.
    /// </summary>
    public string? SecurityToken { get; set; }

    /// <summary>
    /// API endpoint host for LingQue CCAI (ContactCenterAI ROA).
    /// Defaults to the Shanghai public endpoint.
    /// Do not set this to a Bailian/MaaS workspace host such as
    /// <c>{workspaceId}.cn-beijing.maas.aliyuncs.com</c> — that host is for DashScope app APIs, not CCAI.
    /// </summary>
    public string Endpoint { get; set; } = "contactcenterai.cn-shanghai.aliyuncs.com";

    /// <summary>
    /// Region id, defaults to cn-shanghai (matches the default ContactCenterAI endpoint).
    /// </summary>
    public string RegionId { get; set; } = "cn-shanghai";

    /// <summary>
    /// Request timeout.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(2);
}
