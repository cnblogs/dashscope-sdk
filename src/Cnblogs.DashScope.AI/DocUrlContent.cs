namespace Microsoft.Extensions.AI;

public class DocUrlContent : AIContent
{

    public DocUrlContent(string[] urls)
    {
        DocUrl = urls;
    }

    public string[] DocUrl { get; set; }
}
