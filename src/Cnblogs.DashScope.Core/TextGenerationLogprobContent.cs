using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents a possible choice of token.
/// </summary>
/// <param name="Token">Token content.</param>
/// <param name="Bytes">Token content in UTF-8 byte array.</param>
/// <param name="Logprob">Possibility, <c>null</c> when it's too low.</param>
/// <param name="TopLogprobs">The most possible alternatives.</param>
public record TextGenerationLogprobContent(
    string Token,
    [property: JsonConverter(typeof(ByteArrayLiteralConvertor))]
    byte[] Bytes,
    float? Logprob,
    List<TextGenerationTopLogprobContent> TopLogprobs);
