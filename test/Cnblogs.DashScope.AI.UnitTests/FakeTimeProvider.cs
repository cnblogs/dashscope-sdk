namespace Cnblogs.DashScope.AI.UnitTests;

/// <summary>
/// A clock the tests move by hand: to let the retry interval of the model lookups pass, or to time
/// a lookup out. The timers created through the clock fire when the time is advanced.
/// </summary>
internal sealed class FakeTimeProvider : TimeProvider
{
    private readonly List<FakeTimer> _timers = [];

    public DateTimeOffset Now { get; private set; } = new(2026, 11, 26, 12, 0, 0, TimeSpan.Zero);

    /// <inheritdoc />
    public override DateTimeOffset GetUtcNow() => Now;

    /// <summary>
    /// Moves the clock forward and fires the timers that fell due.
    /// </summary>
    public void Advance(TimeSpan interval)
    {
        Now += interval;
        foreach (var timer in _timers)
        {
            timer.FireIfDue(Now);
        }
    }

    /// <inheritdoc />
    public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        var timer = new FakeTimer(this, callback, state);
        _timers.Add(timer);
        timer.Change(dueTime, period);
        return timer;
    }
}
