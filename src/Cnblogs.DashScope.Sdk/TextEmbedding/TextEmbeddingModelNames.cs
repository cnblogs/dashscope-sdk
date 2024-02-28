namespace Cnblogs.DashScope.Sdk.TextEmbedding;

internal static class TextEmbeddingModelNames
{
    public static string GetModelName(this TextEmbeddingModel model)
    {
        return model switch
        {
            TextEmbeddingModel.TextEmbeddingV1 => "text-embedding-v1",
            TextEmbeddingModel.TextEmbeddingV2 => "text-embedding-v2",
            _ => throw new ArgumentOutOfRangeException(
                nameof(model),
                model,
                "Unknown model type, please use the overload with ‘model’ as a string type.")
        };
    }
}
