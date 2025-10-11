using System;
using System.Globalization;

public abstract class Activity
{
    private string _date;
    private int _length;

    public Activity(string date, int length)
    {
        _date = date;
        _length = length;
    }

    public string GetDate()
    {
        return _date;
    }

    public int GetLength()
    {
        return _length;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        CultureInfo ci = CultureInfo.InvariantCulture;

        return string.Format(ci,
            "{0} {1} ({2} min) - Distance: {3:0.0} km, Speed: {4:0.0} kph, Pace: {5:0.00} min per km",
            GetDate(), GetType().Name, GetLength(), GetDistance(), GetSpeed(), GetPace());
    }
}
