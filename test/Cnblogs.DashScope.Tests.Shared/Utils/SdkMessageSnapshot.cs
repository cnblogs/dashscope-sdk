namespace Cnblogs.DashScope.Tests.Shared.Utils;

public record SdkMessageSnapshot<TRequest, TResponse>(TRequest Request, List<TResponse> Response)
    where TRequest : class
    where TResponse : class;
