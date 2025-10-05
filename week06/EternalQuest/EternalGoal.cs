using System;

public class EternalGoal : Goal
{
    private int _streak;
    private DateTime? _lastRecordedDate;

    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _streak = 0;
        _lastRecordedDate = null;
    }

    public int GetStreak() => _streak;

    public override void RecordEvent()
    {
        DateTime today = DateTime.Today;

        if (_lastRecordedDate == null)
        {
            _streak = 1;
        }
        else
        {
            var last = _lastRecordedDate.Value.Date;
            if (last == today) { }
            else if (last.AddDays(1) == today) { _streak += 1; }
            else { _streak = 1; }
        }

        _lastRecordedDate = today;
    }

    public override bool IsComplete() => false;

    public override string GetDetailsString()
    {
        return base.GetDetailsString() + $" — Streak: {_streak} day(s)";
    }

    public override string GetStringRepresentation()
    {
        long ticks = _lastRecordedDate?.Ticks ?? 0L;
        return $"EternalGoal|{GetShortName()}|{GetDescription()}|{GetPoints()}|{_streak}|{ticks}";
    }

    public static EternalGoal FromParts(string name, string desc, int pts, int streak, long ticks)
    {
        var eg = new EternalGoal(name, desc, pts);
        eg._streak = streak;
        eg._lastRecordedDate = ticks == 0 ? (DateTime?)null : new DateTime(ticks).Date;
        return eg;
    }
}
