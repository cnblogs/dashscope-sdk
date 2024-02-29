﻿using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk.TextEmbedding;

internal static class TextEmbeddingModelNames
{
    public static string GetModelName(this TextEmbeddingModel model)
    {
        return model switch
        {
            TextEmbeddingModel.TextEmbeddingV1 => "text-embedding-v1",
            TextEmbeddingModel.TextEmbeddingV2 => "text-embedding-v2",
            _ => ThrowHelper.UnknownModelName(nameof(model), model)
        };
    }
}
