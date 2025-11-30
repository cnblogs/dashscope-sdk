using System.Runtime.CompilerServices;
using System.Web;

namespace Cnblogs.DashScope.Core.Internals;

internal class QueryStringBuilder
{
    private readonly List<KeyValuePair<string, string>> _items = new();

    public QueryStringBuilder Add<T>(T? value, [CallerArgumentExpression("value")] string? key = null)
        => value switch
        {
            null => Add(key, null),
            Enum e => Add(key, e.ToString("D")),
            _ => Add(key, value.ToString())
        };

    private QueryStringBuilder Add(string? parameterName, string? value)
    {
        ArgumentNullException.ThrowIfNull(parameterName);
        if (string.IsNullOrEmpty(value))
        {
            return this;
        }

        _items.Add(new KeyValuePair<string, string>(parameterName, value));
        return this;
    }

    public string Build()
    {
        if (_items.Count == 0)
        {
            return string.Empty;
        }

        var partial = string.Join(
            '&',
            _items.Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));
        return $"?{partial}";
    }
}
