using System.Diagnostics;
using System.Net;

namespace Cnblogs.DashScope.Core.Internals;

internal class ThrottledContent : HttpContent
{
    private const int DefaultBufferSize = 81920;    // 80KB
    private const int MinimumBufferSize = 4096;     // 4KB
    private readonly int _maxBytesPerSecond;
    private readonly int _bufferSize;

    internal HttpContent InnerContent { get; }

    /// <summary>
    /// Creates a throttled HttpContent
    /// </summary>
    /// <param name="content">Raw HttpContent</param>
    /// <param name="maxBytesPerSecond">Maximum upload speed, -1 means no limit.</param>
    public ThrottledContent(HttpContent content, int maxBytesPerSecond = -1)
    {
        InnerContent = content ?? throw new ArgumentNullException(nameof(content));
        _maxBytesPerSecond = maxBytesPerSecond;

        _bufferSize = maxBytesPerSecond < 0
            ? DefaultBufferSize
            : Math.Clamp(maxBytesPerSecond / 10, MinimumBufferSize, DefaultBufferSize);

        foreach (var header in content.Headers)
        {
            Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
    }

    /// <inheritdoc />
    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        return SerializeToStreamAsync(stream, context, CancellationToken.None);
    }

    /// <inheritdoc />
    protected override async Task SerializeToStreamAsync(
        Stream stream,
        TransportContext? context,
        CancellationToken cancellationToken)
    {
        if (_maxBytesPerSecond < 0)
        {
            await InnerContent.CopyToAsync(stream, cancellationToken);
            return;
        }

        await using var originalStream = await InnerContent.ReadAsStreamAsync(cancellationToken);
        var buffer = new byte[_bufferSize];
        int bytesRead;

        var stopwatch = Stopwatch.StartNew();
        long totalBytesSent = 0;

        while ((bytesRead = await originalStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            await stream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
            totalBytesSent += bytesRead;

            var expectedTimeInSeconds = (double)totalBytesSent / _maxBytesPerSecond;
            var expectedTimeInMs = expectedTimeInSeconds * 1000;

            var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;

            if (expectedTimeInMs > elapsedMs)
            {
                var delayMs = (int)(expectedTimeInMs - elapsedMs);
                if (delayMs > 0)
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
            }
        }
    }

    protected override bool TryComputeLength(out long length)
    {
        length = InnerContent.Headers.ContentLength ?? -1;
        return length != -1;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            InnerContent.Dispose();
        }

        base.Dispose(disposing);
    }
}
