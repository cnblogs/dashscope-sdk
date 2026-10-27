namespace Cnblogs.DashScope.Tests.Shared.Utils;

public record AiClientSnapshot<TRequest, TResponse>(TRequest RequestModel, TResponse ResponseModel)
    where TRequest : class
    where TResponse : class;
