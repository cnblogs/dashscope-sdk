using System.Text;

namespace Cnblogs.DashScope.AI;

internal class DashScopeStreamingToolCall
{
    public string? Id { get; set; } = null;
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public StringBuilder Arguments { get; init; } = new();
}
