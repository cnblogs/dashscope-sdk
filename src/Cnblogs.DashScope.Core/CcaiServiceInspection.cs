using System.Text.Json.Serialization;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Service quality inspection configuration.
/// </summary>
public class CcaiServiceInspection
{
    /// <summary>
    /// Inspection dimensions.
    /// </summary>
    [JsonPropertyName("inspectionContents")]
    public required List<CcaiInspectionContent> InspectionContents { get; set; }

    /// <summary>
    /// Detailed introduction of inspection goals.
    /// </summary>
    [JsonPropertyName("inspectionIntroduction")]
    public required string InspectionIntroduction { get; set; }

    /// <summary>
    /// Scene introduction, e.g. insurance sales.
    /// </summary>
    [JsonPropertyName("sceneIntroduction")]
    public required string SceneIntroduction { get; set; }
}
