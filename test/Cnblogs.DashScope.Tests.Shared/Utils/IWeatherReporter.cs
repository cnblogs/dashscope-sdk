using System.ComponentModel;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

/// <summary>
/// Test interface for substitution
/// </summary>
public interface IWeatherReporter
{
    string GetWeather(string location);
}
