using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents one choice of most possibility alternative tokens.
    /// </summary>
    /// <param name="Token">The token content.</param>
    /// <param name="Bytes">The token content in UTF-8 byte array.</param>
    /// <param name="Logprob">Possibility, <c>null</c> when possibility is too low.</param>
    public record TextGenerationTopLogprobContent(
        string Token,
        [property: JsonConverter(typeof(ByteArrayLiteralConvertor))]
        byte[] Bytes,
        float? Logprob);
}
