namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// Indicates the request has content inspection configuration.
/// </summary>
internal interface IDataInspectionConfig
{
    /// <summary>
    /// Content inspection configuration of the request, null means not configured and client level configuration applies.
    /// </summary>
    DataInspectionOptions? DataInspection { get; }
}
