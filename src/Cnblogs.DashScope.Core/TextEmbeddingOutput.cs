﻿namespace Cnblogs.DashScope.Core;

/// <summary>
/// Represents output of text embedding request.
/// </summary>
/// <param name="Embeddings">The embedding results.</param>
public record TextEmbeddingOutput(List<TextEmbeddingItem> Embeddings);
