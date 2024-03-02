namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents a request for model generation.
/// </summary>
/// <typeparam name="TInput">The input type for this request.</typeparam>
public class ModelRequest<TInput>
    where TInput : class
{
    /// <summary>
    /// The model to use.
    /// </summary>
    public required string Model { get; init; }

    /// <summary>
    /// Input of this request.
    /// </summary>
    public required TInput Input { get; init; }
}

/// <summary>
/// Represents a request for model generation.
/// </summary>
/// <typeparam name="TInput">The input type for this request.</typeparam>
/// <typeparam name="TParameter">The option type for this request.</typeparam>
public class ModelRequest<TInput, TParameter> : ModelRequest<TInput>
    where TInput : class
    where TParameter : class
{
    /// <summary>
    /// Optional configuration of this request.
    /// </summary>
    public TParameter? Parameters { get; set; }
}
