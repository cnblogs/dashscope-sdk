using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class DashScopeMultipartContentTests
{
    public static TheoryData<string> Filenames
        => new()
        {
            "ascii.png",
            "中文文件名.png",
            "中英 mixed 文件名.png"
        };

    [Theory]
    [MemberData(nameof(Filenames))]
    public async Task ContentLength_ContainsNonAsciiFilename_MatchesSerializedBytesAsync(string filename)
    {
        // Arrange
        var form = DashScopeMultipartContent.Create("----testboundary");
        form.Add(new StringContent("some field value"));
        var fileContent = new StreamContent(new MemoryStream(new byte[] { 1, 2, 3, 4, 5 }));
        fileContent.Headers.TryAddWithoutValidation(
            "Content-Disposition",
            $"form-data; name=\"file\"; filename=\"{filename}\"");
        form.Add(fileContent);
        var expectedLength = form.Headers.ContentLength;

        // Act
        using var output = new MemoryStream();
        await form.CopyToAsync(output);

        // Assert
        Assert.True(
            expectedLength.HasValue,
            $"ContentLength was not computed (TryComputeLength returned false). Actual bytes: {output.Length}");
        var userMessage = $"Computed ContentLength {expectedLength} != actual bytes {output.Length}. "
                          + $"Serialized content: {Encoding.UTF8.GetString(output.ToArray())}";
        Assert.True(expectedLength == output.Length, userMessage);
    }

    [Fact]
    public void ContentLength_NotMaterializeContentLengthHeaderOfParts()
    {
        // Arrange
        var form = DashScopeMultipartContent.Create("----testboundary");
        form.Add(new StringContent("some field value"));

        // Act
        _ = form.Headers.ContentLength;

        // Assert
        Assert.All(
            form,
            part => Assert.False(
                part.Headers.Contains("Content-Length"),
                "Computing length should not add Content-Length header to parts, otherwise the serialized form would differ from the computed one."));
    }
}
