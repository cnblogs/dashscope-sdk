using System.Text;
using Cnblogs.DashScope.Sample;
using Cnblogs.DashScope.Sdk;
using Cnblogs.DashScope.Sdk.QWen;

const string apiKey = "your api key";
var dashScopeClient = new DashScopeClient(apiKey);

Console.WriteLine("Choose the sample you want to run:");
foreach (var sampleType in Enum.GetValues<SampleType>())
{
    Console.WriteLine($"{(int)sampleType}.{sampleType.GetDescription()}");
}

Console.Write("Choose an option: ");
var type = (SampleType)int.Parse(Console.ReadLine()!);

string userInput;
switch (type)
{
    case SampleType.TextCompletion:
        Console.WriteLine("Prompt > ");
        userInput = Console.ReadLine()!;
        await TextCompletionAsync(userInput);
        break;
    case SampleType.TextCompletionSse:
        Console.WriteLine("Prompt > ");
        userInput = Console.ReadLine()!;
        await TextCompletionStreamAsync(userInput);
        break;
    case SampleType.ChatCompletion:
        await ChatStreamAsync();
        break;
}

return;

// text completion
async Task TextCompletionAsync(string prompt)
{
    var response = await dashScopeClient.GetQWenCompletionAsync(QWenLlm.QWenMax, prompt);
    Console.WriteLine(response.Output.Text);
}

// text completion stream
async Task TextCompletionStreamAsync(string prompt)
{
    var stream = dashScopeClient.GetQWenCompletionStreamAsync(
        QWenLlm.QWenMax,
        prompt,
        new TextGenerationParameters { IncrementalOutput = true });
    await foreach (var modelResponse in stream)
    {
        Console.Write(modelResponse.Output.Text);
    }
}

async Task ChatStreamAsync()
{
    var history = new List<ChatMessage>();
    while (true)
    {
        Console.Write("user > ");
        var input = Console.ReadLine()!;
        history.Add(new ChatMessage("user", input));
        var stream = dashScopeClient.GetQWenChatStreamAsync(
            QWenLlm.QWenMax,
            history,
            new TextGenerationParameters { IncrementalOutput = true, ResultFormat = ResultFormats.Message });
        var role = string.Empty;
        var message = new StringBuilder();
        await foreach (var modelResponse in stream)
        {
            var chunk = modelResponse.Output.Choices![0];
            if (string.IsNullOrEmpty(role) && string.IsNullOrEmpty(chunk.Message.Role) == false)
            {
                role = chunk.Message.Role;
                Console.Write(chunk.Message.Role + " > ");
            }

            message.Append(chunk.Message.Content);
            Console.Write(chunk.Message.Content);
        }

        Console.WriteLine();
        history.Add(new ChatMessage(role, message.ToString()));
    }

    // ReSharper disable once FunctionNeverReturns
}
