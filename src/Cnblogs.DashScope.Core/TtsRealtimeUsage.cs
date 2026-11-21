namespace Cnblogs.DashScope.Core;

/// <summary>
/// The billing usage of the TTS realtime API, returned in response.done events.
/// </summary>
/// <param name="Characters">The billed character count.</param>
/// <param name="InputTokens">The input token count. Not listed in the Bailian documentation yet.</param>
/// <param name="OutputTokens">The output token count. Not listed in the Bailian documentation yet.</param>
/// <param name="TotalTokens">The total token count. Not listed in the Bailian documentation yet.</param>
/// <param name="InputTokensDetails">The input token details. Not listed in the Bailian documentation yet.</param>
/// <param name="OutputTokensDetails">The output token details. Not listed in the Bailian documentation yet.</param>
public record TtsRealtimeUsage(
    int? Characters = null,
    int? InputTokens = null,
    int? OutputTokens = null,
    int? TotalTokens = null,
    TtsRealtimeTokenDetails? InputTokensDetails = null,
    TtsRealtimeTokenDetails? OutputTokensDetails = null);
