namespace Cnblogs.DashScope.Core;

/// <summary>
/// The MiniMax style status of a MiniMax speech synthesis response.
/// </summary>
public class MinimaxBaseResp
{
    /// <summary>
    /// The MiniMax status code, 0 for success. Non-zero values follow the MiniMax error
    /// code list, e.g. 1001 timeout, 1002 rate limited, 1004 unauthorized, 2013 invalid input.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// The status message.
    /// </summary>
    public string? StatusMsg { get; set; }
}
