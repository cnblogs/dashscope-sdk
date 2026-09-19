using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Cnblogs.DashScope.Core.Internals;

/// <summary>
/// ACS3-HMAC-SHA256 signer for Alibaba Cloud ROA-style OpenAPI.
/// </summary>
internal static class Acs3Signer
{
    /// <summary>
    /// Signs an HTTP request in place by adding ACS3 headers and Authorization.
    /// </summary>
    public static void Sign(
        HttpRequestMessage request,
        string accessKeyId,
        string accessKeySecret,
        string action,
        string version,
        string host,
        byte[]? body,
        DateTimeOffset? date = null,
        string? signatureNonce = null)
    {
        var payloadHash = HexEncode(SHA256.HashData(body ?? Array.Empty<byte>()));
        var dateValue = (date ?? DateTimeOffset.UtcNow).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
        var nonce = signatureNonce ?? Guid.NewGuid().ToString("N");

        var headers = new SortedDictionary<string, string>(StringComparer.Ordinal)
        {
            ["host"] = host,
            ["x-acs-action"] = action,
            ["x-acs-content-sha256"] = payloadHash,
            ["x-acs-date"] = dateValue,
            ["x-acs-signature-nonce"] = nonce,
            ["x-acs-version"] = version
        };

        if (request.Content?.Headers.ContentType is { } contentType)
        {
            headers["content-type"] = contentType.ToString();
        }
        else if (body is { Length: > 0 })
        {
            headers["content-type"] = "application/json; charset=utf-8";
        }

        if (request.Headers.TryGetValues("x-acs-security-token", out var tokens))
        {
            headers["x-acs-security-token"] = tokens.First();
        }

        var signedHeaders = string.Join(';', headers.Keys);
        var canonicalHeaders = string.Concat(headers.Select(h => $"{h.Key}:{h.Value.Trim()}\n"));
        var canonicalUri = GetCanonicalUri(request.RequestUri!);
        var canonicalQuery = GetCanonicalQueryString(request.RequestUri!);

        var canonicalRequest =
            $"{request.Method.Method.ToUpperInvariant()}\n{canonicalUri}\n{canonicalQuery}\n{canonicalHeaders}\n{signedHeaders}\n{payloadHash}";

        var stringToSign =
            $"{ContactCenterAiDefaults.SignatureAlgorithm}\n{HexEncode(SHA256.HashData(Encoding.UTF8.GetBytes(canonicalRequest)))}";

        var signature = HexEncode(HmacSha256(Encoding.UTF8.GetBytes(accessKeySecret), stringToSign));
        var authorization =
            $"{ContactCenterAiDefaults.SignatureAlgorithm} Credential={accessKeyId},SignedHeaders={signedHeaders},Signature={signature}";

        foreach (var header in headers)
        {
            if (header.Key is "host" or "content-type")
            {
                continue;
            }

            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (headers.TryGetValue("content-type", out var ct) && request.Content is not null)
        {
            request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(ct);
        }

        request.Headers.TryAddWithoutValidation("Host", host);
        request.Headers.TryAddWithoutValidation("Authorization", authorization);
    }

    /// <summary>
    /// Builds canonical request components for unit testing.
    /// </summary>
    internal static (string CanonicalRequest, string StringToSign, string Signature, string Authorization)
        ComputeSignature(
            string method,
            string canonicalUri,
            string canonicalQuery,
            IReadOnlyDictionary<string, string> headers,
            byte[]? body,
            string accessKeyId,
            string accessKeySecret)
    {
        var payloadHash = HexEncode(SHA256.HashData(body ?? Array.Empty<byte>()));
        var sorted = new SortedDictionary<string, string>(StringComparer.Ordinal);
        foreach (var header in headers)
        {
            sorted[header.Key.ToLowerInvariant()] = header.Value.Trim();
        }

        var signedHeaders = string.Join(';', sorted.Keys);
        var canonicalHeaders = string.Concat(sorted.Select(h => $"{h.Key}:{h.Value}\n"));
        var canonicalRequest =
            $"{method.ToUpperInvariant()}\n{canonicalUri}\n{canonicalQuery}\n{canonicalHeaders}\n{signedHeaders}\n{payloadHash}";
        var stringToSign =
            $"{ContactCenterAiDefaults.SignatureAlgorithm}\n{HexEncode(SHA256.HashData(Encoding.UTF8.GetBytes(canonicalRequest)))}";
        var signature = HexEncode(HmacSha256(Encoding.UTF8.GetBytes(accessKeySecret), stringToSign));
        var authorization =
            $"{ContactCenterAiDefaults.SignatureAlgorithm} Credential={accessKeyId},SignedHeaders={signedHeaders},Signature={signature}";
        return (canonicalRequest, stringToSign, signature, authorization);
    }

    internal static string GetCanonicalUri(Uri uri)
    {
        var path = uri.AbsolutePath;
        if (string.IsNullOrEmpty(path))
        {
            return "/";
        }

        return string.Join('/', path.Split('/', StringSplitOptions.None).Select(PercentEncode));
    }

    internal static string GetCanonicalQueryString(Uri uri)
    {
        if (string.IsNullOrEmpty(uri.Query) || uri.Query == "?")
        {
            return string.Empty;
        }

        var query = uri.Query.TrimStart('?');
        var pairs = query.Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(part =>
            {
                var idx = part.IndexOf('=');
                if (idx < 0)
                {
                    return (Name: PercentEncode(part), Value: string.Empty);
                }

                return (Name: PercentEncode(Uri.UnescapeDataString(part[..idx])),
                    Value: PercentEncode(Uri.UnescapeDataString(part[(idx + 1)..])));
            })
            .OrderBy(p => p.Name, StringComparer.Ordinal)
            .ThenBy(p => p.Value, StringComparer.Ordinal);

        return string.Join('&', pairs.Select(p => $"{p.Name}={p.Value}"));
    }

    internal static string PercentEncode(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var encoded = Uri.EscapeDataString(value)
            .Replace("+", "%20", StringComparison.Ordinal)
            .Replace("*", "%2A", StringComparison.Ordinal)
            .Replace("%7E", "~", StringComparison.Ordinal);
        return encoded;
    }

    private static byte[] HmacSha256(byte[] key, string data)
    {
        using var hmac = new HMACSHA256(key);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    }

    private static string HexEncode(byte[] data)
        => Convert.ToHexString(data).ToLowerInvariant();
}
