using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.MultimodalEmbedding;

public abstract class MultimodalEmbeddingSample : ISample
{
    /// <inheritdoc />
    public string Group => "MultimodalEmbedding";

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task RunAsync(IDashScopeClient client);

    // the vectors come out normalized, the cosine similarity tells how close two contents are
    protected static float Cosine(float[] x, float[] y)
    {
        var dot = 0f;
        var normX = 0f;
        var normY = 0f;
        for (var i = 0; i < x.Length; i++)
        {
            dot += x[i] * y[i];
            normX += x[i] * x[i];
            normY += y[i] * y[i];
        }

        return dot / (MathF.Sqrt(normX) * MathF.Sqrt(normY));
    }
}
