using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Region enum to region id parser.
/// </summary>
public static class DashScopeRegionParser
{
    /// <summary>
    /// Get region id from region enum.
    /// </summary>
    /// <param name="region">The region enum.</param>
    /// <returns>Region ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="region"/> is not a defined enum value.</exception>
    public static string ToRegionId(this DashScopeRegion region)
    {
        return region switch
        {
            DashScopeRegion.Beijing => DashScopeRegionIds.Beijing,
            DashScopeRegion.Singapore => DashScopeRegionIds.Singapore,
            DashScopeRegion.HongKong => DashScopeRegionIds.HongKong,
            DashScopeRegion.UsEast => DashScopeRegionIds.UsEast,
            DashScopeRegion.Germany => DashScopeRegionIds.Germany,
            DashScopeRegion.Japan => DashScopeRegionIds.Japan,
            _ => throw new ArgumentOutOfRangeException(nameof(region), region, "Unknown DashScope region")
        };
    }
}
