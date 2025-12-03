using System.Buffers;
using System.Net;
using System.Text;

namespace Cnblogs.DashScope.Core
{
    internal class DashScopeMultipartContent : MultipartContent
    {
        private const string CrLf = "\r\n";
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
            var success = base.TryComputeLength(out length);
            return success;
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
}
