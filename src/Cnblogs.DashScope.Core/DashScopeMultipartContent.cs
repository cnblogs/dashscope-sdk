using System.Buffers;
using System.Net;
using System.Reflection;
using System.Text;

namespace Cnblogs.DashScope.Core;

internal class DashScopeMultipartContent : MultipartContent
{
    private const string CrLf = "\r\n";
    private static readonly TryComputeLengthHandler? PartLengthHandler = CreatePartLengthHandler();
    private readonly string _boundary;

    private DashScopeMultipartContent(string boundary)
        : base("form-data", boundary)
    {
        _boundary = boundary;
    }

    /// <inheritdoc />
    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        // Write start boundary.
        await EncodeStringToStreamAsync(stream, "--" + _boundary + CrLf);

        // Write each nested content.
        var output = new MemoryStream();
        var contentIndex = 0;
        foreach (var content in this)
        {
            output.SetLength(0);
            SerializeHeadersToStream(output, content, writeDivider: contentIndex != 0);
            output.Position = 0;
            await output.CopyToAsync(stream);
            await content.CopyToAsync(stream, context);
            contentIndex++;
        }

        // Write footer boundary.
        await EncodeStringToStreamAsync(stream, CrLf + "--" + _boundary + "--" + CrLf);
    }

    /// <inheritdoc />
    protected override bool TryComputeLength(out long length)
    {
        // The base implementation counts headers by characters instead of bytes, resulting in a wrong length
        // when headers contain multi-byte characters (e.g. non-ascii filename).
        var handler = PartLengthHandler;
        if (handler == null)
        {
            // Reflection is unavailable (e.g. trimmed), degrade to the inaccurate base implementation.
            return base.TryComputeLength(out length);
        }

        long totalSize = 0;
        var contentIndex = 0;
        foreach (var content in this)
        {
            if (handler(content, out var partLength) == false)
            {
                length = 0;
                return false;
            }

            // Start boundary or divider.
            totalSize += _boundary.Length + (contentIndex == 0 ? 4 : 6);

            // Headers, counted in bytes to match SerializeHeadersToStream.
            foreach (var headerPair in content.Headers.NonValidated)
            {
                var headerValueEncoding = HeaderEncodingSelector?.Invoke(headerPair.Key, content)
                                          ?? Encoding.UTF8;

                totalSize += Encoding.UTF8.GetByteCount(headerPair.Key);
                totalSize += 2; // ": "
                var delim = string.Empty;
                foreach (var value in headerPair.Value)
                {
                    totalSize += Encoding.UTF8.GetByteCount(delim);
                    totalSize += headerValueEncoding.GetByteCount(value);
                    delim = ", ";
                }

                totalSize += 2; // CRLF
            }

            totalSize += 2; // blank line between headers and content

            // Content.
            totalSize += partLength;
            contentIndex++;
        }

        // Footer boundary.
        totalSize += _boundary.Length + 8;

        length = totalSize;
        return true;
    }

    private void SerializeHeadersToStream(Stream stream, HttpContent content, bool writeDivider)
    {
        // Add divider.
        if (writeDivider)
        {
            WriteToStream(stream, CrLf + "--");
            WriteToStream(stream, _boundary);
            WriteToStream(stream, CrLf);
        }

        // Add headers.
        foreach (var headerPair in content.Headers.NonValidated)
        {
            var headerValueEncoding = HeaderEncodingSelector?.Invoke(headerPair.Key, content)
                                      ?? Encoding.UTF8;

            WriteToStream(stream, headerPair.Key);
            WriteToStream(stream, ": ");
            var delim = string.Empty;
            foreach (var value in headerPair.Value)
            {
                WriteToStream(stream, delim);
                WriteToStream(stream, value, headerValueEncoding);
                delim = ", ";
            }

            WriteToStream(stream, CrLf);
        }

        WriteToStream(stream, CrLf);
    }

    private static void WriteToStream(Stream stream, string content) => WriteToStream(stream, content, Encoding.UTF8);

    private static void WriteToStream(Stream stream, string content, Encoding encoding)
    {
        const int stackallocThreshold = 1024;

        var maxLength = encoding.GetMaxByteCount(content.Length);

        byte[]? rentedBuffer = null;
        var buffer = maxLength <= stackallocThreshold
            ? stackalloc byte[stackallocThreshold]
            : (rentedBuffer = ArrayPool<byte>.Shared.Rent(maxLength));

        try
        {
            var written = encoding.GetBytes(content, buffer);
            stream.Write(buffer.Slice(0, written));
        }
        finally
        {
            if (rentedBuffer != null)
            {
                ArrayPool<byte>.Shared.Return(rentedBuffer);
            }
        }
    }

    private static ValueTask EncodeStringToStreamAsync(Stream stream, string input)
    {
        var buffer = Encoding.UTF8.GetBytes(input);
        return stream.WriteAsync(new ReadOnlyMemory<byte>(buffer));
    }

    private static TryComputeLengthHandler? CreatePartLengthHandler()
    {
        // HttpContent.TryComputeLength is protected internal, so it can only be invoked from within
        // System.Net.Http or via reflection.
        var method = typeof(HttpContent).GetMethod(
            "TryComputeLength",
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            types: new[] { typeof(long).MakeByRefType() },
            modifiers: null);
        if (method == null)
        {
            return null;
        }

        return (TryComputeLengthHandler)Delegate.CreateDelegate(typeof(TryComputeLengthHandler), method);
    }

    private delegate bool TryComputeLengthHandler(HttpContent content, out long length);

    public static DashScopeMultipartContent Create()
    {
        return Create(Guid.NewGuid().ToString());
    }

    internal static DashScopeMultipartContent Create(string boundary)
    {
        var content = new DashScopeMultipartContent(boundary);
        content.Headers.ContentType = null;
        content.Headers.TryAddWithoutValidation(
            "Content-Type",
            $"multipart/form-data; boundary={boundary}");
        return content;
    }
}
