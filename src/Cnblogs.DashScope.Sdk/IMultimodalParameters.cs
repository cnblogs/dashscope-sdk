namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Optional parameters for multi-model generation request.
/// </summary>
public interface IMultimodalParameters : IProbabilityParameter, ISeedParameter, IIncrementalOutputParameter;
