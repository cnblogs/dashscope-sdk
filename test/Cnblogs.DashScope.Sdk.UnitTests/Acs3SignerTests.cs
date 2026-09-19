using System.Security.Cryptography;
using System.Text;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class Acs3SignerTests
{
    [Fact]
    public void ComputeSignature_EmptyBody_MatchesSha256EmptyAndIsDeterministic()
    {
        var emptyHash = Convert.ToHexString(SHA256.HashData(Array.Empty<byte>())).ToLowerInvariant();
        Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", emptyHash);

        var headers = new Dictionary<string, string>
        {
            ["host"] = "contactcenterai.cn-shanghai.aliyuncs.com",
            ["x-acs-action"] = "AnalyzeConversation",
            ["x-acs-content-sha256"] = emptyHash,
            ["x-acs-date"] = "2024-06-03T12:00:00Z",
            ["x-acs-signature-nonce"] = "fixed-nonce-001",
            ["x-acs-version"] = "2024-06-03",
            ["content-type"] = "application/json; charset=utf-8"
        };

        var first = Acs3Signer.ComputeSignature(
            "POST",
            "/ws-demo/ccai/app/app-demo/analyze_conversation",
            string.Empty,
            headers,
            Array.Empty<byte>(),
            "LTAI5tTestAccessKeyId",
            "TestAccessKeySecret");

        var second = Acs3Signer.ComputeSignature(
            "POST",
            "/ws-demo/ccai/app/app-demo/analyze_conversation",
            string.Empty,
            headers,
            Array.Empty<byte>(),
            "LTAI5tTestAccessKeyId",
            "TestAccessKeySecret");

        Assert.Equal(first.Signature, second.Signature);
        Assert.StartsWith("ACS3-HMAC-SHA256 Credential=LTAI5tTestAccessKeyId,SignedHeaders=", first.Authorization);
        Assert.Contains("Signature=" + first.Signature, first.Authorization);
        Assert.Contains("content-type;host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version", first.Authorization);
    }

    [Fact]
    public void ComputeSignature_WithBody_IncludesPayloadHash()
    {
        var body = Encoding.UTF8.GetBytes("{\"stream\":false}");
        var payloadHash = Convert.ToHexString(SHA256.HashData(body)).ToLowerInvariant();
        var headers = new Dictionary<string, string>
        {
            ["host"] = "contactcenterai.cn-shanghai.aliyuncs.com",
            ["x-acs-action"] = "AnalyzeConversation",
            ["x-acs-content-sha256"] = payloadHash,
            ["x-acs-date"] = "2024-06-03T12:00:00Z",
            ["x-acs-signature-nonce"] = "fixed-nonce-002",
            ["x-acs-version"] = "2024-06-03",
            ["content-type"] = "application/json; charset=utf-8"
        };

        var signed = Acs3Signer.ComputeSignature(
            "POST",
            "/ws/ccai/app/app/analyze_conversation",
            string.Empty,
            headers,
            body,
            "ak",
            "sk");

        Assert.Contains(payloadHash, signed.CanonicalRequest);
        Assert.Equal(64, signed.Signature.Length);
    }

    [Fact]
    public void GetCanonicalQueryString_SortsAndEncodes()
    {
        var uri = new Uri("https://example.com/path?b=2&a=1&c=");
        var canonical = Acs3Signer.GetCanonicalQueryString(uri);
        Assert.Equal("a=1&b=2&c=", canonical);
    }

    [Fact]
    public void PercentEncode_EncodesSpecialCharacters()
    {
        Assert.Equal("a%20b", Acs3Signer.PercentEncode("a b"));
        Assert.Equal("%2A", Acs3Signer.PercentEncode("*"));
        Assert.Equal("~", Acs3Signer.PercentEncode("~"));
    }
}
