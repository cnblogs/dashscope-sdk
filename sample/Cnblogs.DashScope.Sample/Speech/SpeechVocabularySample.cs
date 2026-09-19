using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Sample;

namespace Cnblogs.DashScope.Sample.Speech;

public class SpeechVocabularySample : SpeechSample
{
    /// <inheritdoc />
    public override string Description => "Speech vocabulary CRUD (speech-biasing)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        const string targetModel = "paraformer-v2";
        const string prefix = "sdkdemo";

        var create = await client.CreateSpeechVocabularyAsync(
            targetModel,
            prefix,
            new[] { new SpeechVocabularyItem("阿里巴巴", 4, "zh"), new SpeechVocabularyItem("通义千问", 4, "zh") });
        var vocabularyId = create.Output.VocabularyId;
        Console.WriteLine($"Created vocabulary: {vocabularyId}");

        var list = await client.ListSpeechVocabulariesAsync(prefix, 0, 10);
        Console.WriteLine($"Listed {list.Output.VocabularyList?.Count ?? 0} vocabularies");

        var query = await client.GetSpeechVocabularyAsync(vocabularyId);
        Console.WriteLine($"Query status={query.Output.Status}, words={query.Output.Vocabulary?.Count}");

        await client.UpdateSpeechVocabularyAsync(
            vocabularyId,
            new[] { new SpeechVocabularyItem("百炼", 5, "zh") });
        Console.WriteLine("Updated vocabulary");

        Console.WriteLine($"Use vocabulary_id in transcription: {vocabularyId}");
        Console.WriteLine("Deleting vocabulary...");
        await client.DeleteSpeechVocabularyAsync(vocabularyId);
        Console.WriteLine("Deleted");
    }
}
