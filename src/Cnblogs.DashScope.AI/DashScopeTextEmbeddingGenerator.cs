using System.Diagnostics.CodeAnalysis;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sdk.TextEmbedding;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.AI
{
    /// <summary>
    /// An <see cref="IEmbeddingGenerator{TInput,TEmbedding}"/> for a DashScope client.
    /// </summary>
    public sealed class DashScopeTextEmbeddingGenerator
        : IEmbeddingGenerator<string, Embedding<float>>
    {
        private readonly IDashScopeClient _dashScopeClient;
        private readonly string _modelId;
        private readonly TextEmbeddingParameters _parameters;

        /// <summary>
        /// Initialize a new instance of the <see cref="DashScopeTextEmbeddingGenerator"/> class.
        /// </summary>
        /// <param name="dashScopeClient">The underlying client.</param>
        /// <param name="modelId">The model name used to generate embedding.</param>
        /// <param name="dimensions">The number of dimensions produced by the generator.</param>
        public DashScopeTextEmbeddingGenerator(IDashScopeClient dashScopeClient, string modelId, int? dimensions = null)
        {
            ArgumentNullException.ThrowIfNull(dashScopeClient);
            ArgumentNullException.ThrowIfNull(modelId);

            _dashScopeClient = dashScopeClient;
            _modelId = modelId;
            _parameters = new TextEmbeddingParameters { Dimension = dimensions };
        }

        /// <inheritdoc />
        public async Task<GeneratedEmbeddings<Embedding<float>>> GenerateAsync(
            IEnumerable<string> values,
            EmbeddingGenerationOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = ToParameters(options) ?? _parameters;
            var rawResponse =
                await _dashScopeClient.GetTextEmbeddingsAsync(_modelId, values, parameters, cancellationToken);
            var embeddings = rawResponse.Output.Embeddings.Select(
                e => new Embedding<float>(e.Embedding) { ModelId = _modelId, CreatedAt = DateTimeOffset.Now });
            var rawUsage = rawResponse.Usage;
            var usage = rawUsage != null
                ? new UsageDetails { InputTokenCount = rawUsage.TotalTokens, TotalTokenCount = rawUsage.TotalTokens }
                : null;
            return new GeneratedEmbeddings<Embedding<float>>(embeddings)
            {
                Usage = usage,
                AdditionalProperties =
                    new AdditionalPropertiesDictionary { { nameof(rawResponse.RequestId), rawResponse.RequestId } }
            };
        }

        /// <inheritdoc />
        public object? GetService(Type serviceType, object? serviceKey = null)
        {
            return
                serviceKey is not null ? null :
                serviceType == typeof(IDashScopeClient) ? _dashScopeClient :
                serviceType.IsInstanceOfType(this) ? this :
                null;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Nothing to dispose. Implementation required for the IEmbeddingGenerator interface.
        }

        [return: NotNullIfNotNull(nameof(options))]
        private static TextEmbeddingParameters? ToParameters(EmbeddingGenerationOptions? options)
        {
            if (options is null)
            {
                return null;
            }

            return new TextEmbeddingParameters
            {
                Dimension = options.Dimensions,
                OutputType =
                    options.AdditionalProperties?.GetValueOrDefault(nameof(TextEmbeddingParameters.OutputType)) as string,
                TextType =
                    options.AdditionalProperties?.GetValueOrDefault(nameof(TextEmbeddingParameters.TextType)) as string,
            };
        }
    }
}
