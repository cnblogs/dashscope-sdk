namespace Cnblogs.DashScope.Core;

/// <summary>
/// Optional parameters for multi-model generation request.
/// </summary>
public interface IMultimodalParameters
    : IProbabilityParameter, ISeedParameter, IIncrementalOutputParameter, IPenaltyParameter, IMaxTokenParameter,
        IStopTokenParameter
{
    /// <summary>
    /// Allow higher resolution for inputs. When setting to <c>true</c>, increases the maximum input token from 1280 to 16384. Defaults to <c>false</c>.
    /// </summary>
    public bool? VlHighResolutionImages { get; }
}
