﻿namespace Cnblogs.DashScope.Core;

/// <summary>
/// Marks parameter accepts presence_penalty and repetition_penalty  options.
/// </summary>
public interface IPenaltyParameter
{
    /// <summary>
    /// Increasing the repetition penalty can reduce the amount of repetition in the model’s output. A value of 1.0 indicates no penalty, with the default set at 1.1.
    /// </summary>
    public float? RepetitionPenalty { get; }

    /// <summary>
    /// Control the content repetition in text generated by the model.
    /// </summary>
    /// <remarks>Must be in [-2.0, 2.0]. Use higher penalty for batter creativity.</remarks>
    public float? PresencePenalty { get; }
}
