namespace Cnblogs.DashScope.Sdk.UnitTests;

public class MockHttpMessageHandler : HttpMessageHandler
{
    /// <inheritdoc />
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(MockSend(request, cancellationToken));
    }

    /// <inheritdoc />
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return MockSend(request, cancellationToken);
    }

    public virtual HttpResponseMessage MockSend(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // ready for mock.
        throw new InvalidOperationException();
    }
}
