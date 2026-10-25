using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Default values for DashScope client.
/// </summary>
public static class DashScopeDefaults
{
    private const string DefaultRegionId = DashScopeRegionIds.Beijing;

    /// <summary>
    /// Default domain for DashScope API;
    /// </summary>
    public const string DefaultDomain = "dashscope.aliyuncs.com";

    /// <summary>
    /// Base address for HTTP API with given workspaceId.
    /// </summary>
    /// <param name="workspaceId">ID of your Bailian workspace.</param>
    /// <param name="regionId">The region of your workspace.</param>
    /// <returns></returns>
    public static string HttpBaseAddress(string? workspaceId = null, string? regionId = null)
        => $"https://{WorkspaceBaseDomain(workspaceId, regionId)}/api/v1/";

    /// <summary>
    /// Base address for Websocket API with given workspaceId.
    /// </summary>
    /// <param name="workspaceId">ID of your Bailian workspace.</param>
    /// <param name="regionId">The region of your workspace.</param>
    /// <returns></returns>
    public static string WebsocketBaseAddress(string? workspaceId = null, string? regionId = null)
        => $"wss://{WorkspaceBaseDomain(workspaceId, regionId)}/api-ws/v1/inference/";

    /// <summary>
    /// Default json serializer options.
    /// </summary>
    public static readonly JsonSerializerOptions SerializationOptions =
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

    private static string WorkspaceBaseDomain(string? workspaceId, string? regionId = null)
    {
        regionId ??= DefaultRegionId;
        return string.IsNullOrWhiteSpace(workspaceId)
            ? SharedBaseDomain(regionId)
            : $"{workspaceId}.{regionId}.maas.aliyuncs.com";
    }

    /// <summary>
    /// Only cn-beijing, ap-southeast-1 and cn-hongkong expose a shared domain that works
    /// without a workspace id, see docs/bailian for other regions a workspace is required.
    /// </summary>
    private static string SharedBaseDomain(string regionId)
        => regionId switch
        {
            DashScopeRegionIds.Singapore => "dashscope-intl.aliyuncs.com",
            DashScopeRegionIds.HongKong => "cn-hongkong.dashscope.aliyuncs.com",
            _ => DefaultDomain
        };
}
