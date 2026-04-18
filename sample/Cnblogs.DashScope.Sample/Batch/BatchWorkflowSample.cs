using System.Diagnostics;
using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Batch;

public class BatchWorkflowSample : BatchesSample
{
    /// <inheritdoc />
    public override string Description => "Create Batch with jsonl files";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var jsonl =
            """
            {"custom_id":"1","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"你好！有什么可以帮助你的吗？"}]}}
            {"custom_id":"2","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"What is 2+2?"}]}}
            """;
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonl));
        Console.WriteLine("Uploading jsonl files...");
        var inputFile = await client.OpenAiCompatibleUploadFileAsync(stream, "test_model.jsonl", "batch");
        var cleanupList = new List<string>() { inputFile.Id.Value };
        Console.WriteLine($"File uploaded, id: {inputFile.Id.Value}");
        Console.WriteLine("Creating batch job...");
        var batch = await client.OpenAiCompatibleCreateBatchAsync(new DashScopeCreateBatchRequest()
        {
            InputFileId = inputFile.Id.Value,
            CompletionWindow = "24h",
            Endpoint = "/v1/chat/ds-test",
            Metadata = new DashScopeBatchMetadata()
            {
                DsName = "Test job"
            }
        });
        Console.WriteLine($"Batch created, id: {batch.Id}");

        Console.WriteLine("Waiting for job to finish...");
        var timer = Stopwatch.StartNew();
        while (timer.Elapsed.TotalSeconds < 300)
        {
            batch = await client.OpenAiCompatibleGetBatchAsync(batch.Id);
            if (batch.Status != "completed")
            {
                Console.WriteLine($"[{timer.Elapsed.TotalSeconds}s] Batch Status: {batch.Status}");
                await Task.Delay(5000);
            }
            else
            {
                break;
            }
        }

        if (batch.Status != "completed")
        {
            Console.WriteLine("Batch job not finished within 300s");
        }
        else if (batch.OutputFileId == null)
        {
            Console.WriteLine("Batch job did not produce valid output file");
        }
        else
        {
            Console.WriteLine("Batch job finished, downloading result...");
            await using var result = await client.OpenAiCompatibleGetFileContentAsync(batch.OutputFileId);
            using var streamReader = new StreamReader(result);
            var texts = await streamReader.ReadToEndAsync();
            Console.WriteLine("File content: ");
            Console.WriteLine(texts);
            cleanupList.Add(batch.OutputFileId);
        }

        Console.WriteLine("Cleaning up...");
        foreach (var fileId in cleanupList)
        {
            var deletion = await client.OpenAiCompatibleDeleteFileAsync(fileId);
            Console.WriteLine(deletion.Deleted ? $"Deleted: {deletion.Id}" : $"Deletion failed: {deletion.Id}");
        }
    }
}
