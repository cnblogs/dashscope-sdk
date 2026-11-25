using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.UniTranslation;

public class UniTranslationDocumentExample : UniTranslationSample
{
    /// <inheritdoc />
    public override string Description => "Translate a document asynchronously (qwen-mt-uni, submit and poll)";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        Console.WriteLine("Submitting the translation task...");
        var task = await client.CreateUniTranslationTaskAsync(
            new ModelRequest<UniTranslationInput>
            {
                Model = "qwen-mt-uni",
                Input = new UniTranslationInput
                {
                    FileUrl = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250224/ghwuqu/%E5%91%98%E5%B7%A5%E4%BF%A1%E6%81%AF%E8%A1%A8.xlsx",
                    TargetLang = "en"
                }
            });

        var taskId = task.Output.TaskId!;
        Console.WriteLine($"Task submitted: {taskId}");

        // poll until the task leaves the queue / finishes
        DashScopeTask<UniTranslationOutput, UniTranslationUsage> result;
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            result = await client.GetUniTranslationTaskAsync(taskId);
            Console.WriteLine($"Task status: {result.Output.TaskStatus}");
            if (result.Output.TaskStatus is not (DashScopeTaskStatus.Pending or DashScopeTaskStatus.Running))
            {
                break;
            }
        }

        // a translation that fails still ends with the status Succeeded, check Success to tell it apart
        if (result.Output.Success == false)
        {
            Console.WriteLine($"Translation failed ({result.Output.Code}): {result.Output.Message}");
            return;
        }

        Console.WriteLine($"Token usage: {result.Usage?.TotalTokens}");
        Console.WriteLine($"Translated file url (valid for 24 hours): {result.Output.Data?.TranslatedFileUrl}");
    }
}
