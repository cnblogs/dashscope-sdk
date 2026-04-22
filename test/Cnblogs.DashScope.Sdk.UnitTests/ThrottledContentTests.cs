using System.Diagnostics;
using System.Reflection;
using Cnblogs.DashScope.Core.Internals;

// ReSharper disable RedundantArgumentDefaultValue
namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ThrottledContentTests
{
    private const int NoLimit = -1;

    private static readonly MethodInfo TryComputeLengthMethod =
        typeof(ThrottledContent).GetMethod(
            "TryComputeLength",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    [Fact]
    public void Constructor_NullContent_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new ThrottledContent(null!));
    }

    [Fact]
    public void Constructor_NegativeMaxSpeed_NoLimit()
    {
        var inner = new StringContent("hello");
        using var content = new ThrottledContent(inner, NoLimit);

        Assert.NotNull(content.Headers.ContentType);
    }

    [Fact]
    public void Constructor_HeadersCopied()
    {
        var inner = new StringContent("test");
        inner.Headers.Add("X-Custom", "value");
        using var content = new ThrottledContent(inner);

        Assert.True(content.Headers.Contains("X-Custom"));
    }

    [Fact]
    public async Task SerializeToStream_NoLimit_DataPreserved()
    {
        var data = new byte[100_000];
        Random.Shared.NextBytes(data);
        var inner = new ByteArrayContent(data);
        using var content = new ThrottledContent(inner, NoLimit);
        using var ms = new MemoryStream();

        await content.CopyToAsync(ms);

        Assert.Equal(data, ms.ToArray());
    }

    [Fact]
    public async Task SerializeToStream_Throttled_DataPreserved()
    {
        var data = new byte[10_000];
        Random.Shared.NextBytes(data);
        var inner = new ByteArrayContent(data);
        using var content = new ThrottledContent(inner, 1_000_000);
        using var ms = new MemoryStream();

        await content.CopyToAsync(ms);

        Assert.Equal(data, ms.ToArray());
    }

    [Fact]
    public async Task SerializeToStream_Throttled_TakesLongerThanNoLimit()
    {
        var data = new byte[50_000];
        Random.Shared.NextBytes(data);

        // No limit
        var inner1 = new ByteArrayContent(data);
        using var content1 = new ThrottledContent(inner1, NoLimit);
        using var ms1 = new MemoryStream();
        var sw1 = Stopwatch.StartNew();
        await content1.CopyToAsync(ms1);
        sw1.Stop();

        // Throttled to 50KB/s (~1s for 50KB)
        var inner2 = new ByteArrayContent(data);
        using var content2 = new ThrottledContent(inner2, 50_000);
        using var ms2 = new MemoryStream();
        var sw2 = Stopwatch.StartNew();
        await content2.CopyToAsync(ms2);
        sw2.Stop();

        Assert.True(sw2.ElapsedMilliseconds > sw1.ElapsedMilliseconds + 500);
        Assert.Equal(data, ms2.ToArray());
    }

    [Fact]
    public void TryComputeLength_KnownLength_ReturnsTrue()
    {
        var inner = new ByteArrayContent(new byte[] { 1, 2, 3 });
        using var content = new ThrottledContent(inner);

        var result = (bool)TryComputeLengthMethod.Invoke(content, new object[] { 0L })!;

        Assert.True(result);
    }

    [Fact]
    public void TryComputeLength_StreamWithNoLength_ReturnsFalse()
    {
        // Use a stream that CanSeek=false so StreamContent can't determine length
        var inner = new StreamContent(new CustomNonSeekableStream());
        using var content = new ThrottledContent(inner);

        var result = (bool)TryComputeLengthMethod.Invoke(content, new object[] { 0L })!;

        Assert.False(result);
    }

    [Fact]
    public async Task Dispose_DisposesInnerContent()
    {
        var inner = new StringContent("test");
        var content = new ThrottledContent(inner);

        content.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => inner.ReadAsStringAsync());
    }

    [Fact]
    public async Task SerializeToStream_EmptyData_NoError()
    {
        var inner = new ByteArrayContent(Array.Empty<byte>());
        using var content = new ThrottledContent(inner, 1000);
        using var ms = new MemoryStream();

        await content.CopyToAsync(ms);

        Assert.Equal(0, ms.Length);
    }

    [Fact]
    public async Task SerializeToStream_SmallDataThrottled_DataPreservedAsync()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var inner = new ByteArrayContent(data);
        using var content = new ThrottledContent(inner, 100);
        using var ms = new MemoryStream();

        await content.CopyToAsync(ms);

        Assert.Equal(data, ms.ToArray());
    }

    private class CustomNonSeekableStream : Stream
    {
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count) => 0;
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override void Flush() { }
    }
}
