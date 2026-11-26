namespace Cnblogs.DashScope.AI.UnitTests;

/// <summary>
/// A timer of <see cref="FakeTimeProvider"/>, it fires from <see cref="FakeTimeProvider.Advance"/>.
/// </summary>
internal sealed class FakeTimer(FakeTimeProvider clock, TimerCallback callback, object? state) : ITimer
{
    private TimeSpan _period = Timeout.InfiniteTimeSpan;
    private DateTimeOffset? _dueAt;
    private bool _disposed;

    /// <inheritdoc />
    public bool Change(TimeSpan dueTime, TimeSpan period)
    {
        _dueAt = dueTime == Timeout.InfiniteTimeSpan ? null : clock.Now + dueTime;
        _period = period;
        return !_disposed;
    }

    /// <summary>
    /// Fires the callback when the timer fell due.
    /// </summary>
    public void FireIfDue(DateTimeOffset now)
    {
        if (_dueAt is not { } dueAt || dueAt > now)
        {
            return;
        }

        // a periodic timer fires once here whatever the clock jumped, and is due again after its period
        _dueAt = _period == Timeout.InfiniteTimeSpan ? null : now + _period;
        callback(state);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _disposed = true;
        _dueAt = null;
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}
