using System.Net;
using System.Text;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

/// <summary>
/// Content that can be read once only, like the content of a streamed HTTP response. The fixtures
/// are built from strings, which can be read again and again, so the tests use this content to make
/// sure the SDK reads the body of a failed request once.
/// </summary>
public sealed class SingleReadStreamContent : HttpContent
{
    private readonly byte[] _bytes;
    private bool _consumed;

    public SingleReadStreamContent(string content)
    {
        _bytes = Encoding.UTF8.GetBytes(content);
    }

    /// <inheritdoc />
    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        ThrowIfConsumed();
        _consumed = true;
        await stream.WriteAsync(_bytes);
    }

    /// <inheritdoc />
    protected override bool TryComputeLength(out long length)
    {
        // the length of a streamed response is not known before it is read
        length = 0;
        return false;
    }

    /// <inheritdoc />
    protected override Task<Stream> CreateContentReadStreamAsync()
    {
        ThrowIfConsumed();
        _consumed = true;
        return Task.FromResult<Stream>(new MemoryStream(_bytes, false));
    }

    private void ThrowIfConsumed()
    {
        if (_consumed)
        {
            throw new InvalidOperationException("The stream was already consumed. It cannot be read again.");
        }
    }
}
