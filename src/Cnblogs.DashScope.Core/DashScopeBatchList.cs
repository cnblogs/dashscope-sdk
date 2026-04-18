namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represent one page of <see cref="DashScopeBatch"/>.
/// </summary>
/// <param name="Object">Fixed at 'list'.</param>
/// <param name="Data">Batches of current page.</param>
/// <param name="FirstId">ID of the first item of current page.</param>
/// <param name="LastId">ID of the last item of current page.</param>
/// <param name="HasMore">Has more page.</param>
public record DashScopeBatchList(
    string Object,
    List<DashScopeBatch>? Data,
    string? FirstId,
    string? LastId,
    bool HasMore);
