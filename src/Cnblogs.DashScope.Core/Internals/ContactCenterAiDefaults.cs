using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Defaults for LingQue CCAI Conversation Analysis AIO (ContactCenterAI).
/// </summary>
internal static class ContactCenterAiDefaults
{
    /// <summary>
    /// Default public endpoint (China East 2 - Shanghai).
    /// </summary>
    public const string Endpoint = "contactcenterai.cn-shanghai.aliyuncs.com";

    /// <summary>
    /// Default region id.
    /// </summary>
    public const string RegionId = "cn-shanghai";

    /// <summary>
    /// OpenAPI version.
    /// </summary>
    public const string ApiVersion = "2024-06-03";

    /// <summary>
    /// ACS3 signature algorithm.
    /// </summary>
    public const string SignatureAlgorithm = "ACS3-HMAC-SHA256";

    /// <summary>
    /// JSON options for ContactCenterAI request/response bodies.
    /// Field names are controlled by <see cref="JsonPropertyNameAttribute"/>.
    /// </summary>
    public static readonly JsonSerializerOptions SerializationOptions =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
}
