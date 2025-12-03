using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Files
{
    public class FileUploadSample : FilesSample
    {
        /// <inheritdoc />
        public override string Description => "Upload File Sample";

        /// <inheritdoc />
        public override async Task RunAsync(IDashScopeClient client)
        {
            var json = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
            var file = new FileInfo("Lenna.jpg");
            Console.WriteLine("Uploading file...");
            var response = await client.UploadFilesAsync(
                "file-extract",
                new[] { new DashScopeUploadFileInput(file.OpenRead(), file.Name, "Lenna") });
            Console.WriteLine($"File uploaded, fileId: {response.Data.UploadedFiles[0].FileId}");

            await Task.Delay(1000);
            Console.WriteLine("Get file info...");
            var fileInfo = await client.GetFileAsync(response.Data.UploadedFiles[0].FileId);
            Console.WriteLine(JsonSerializer.Serialize(fileInfo.Data, json));

            await Task.Delay(1000);
            Console.WriteLine("List files...");
            var list = await client.ListFilesAsync(1, 2);
            Console.WriteLine(JsonSerializer.Serialize(list.Data.Files, json));

            await Task.Delay(1000);
            Console.Write("Delete file...");
            await client.DeleteFileAsync(response.Data.UploadedFiles[0].FileId);
            Console.WriteLine("Success");
        }
    }
}
