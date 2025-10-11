using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        List<Activity> activities = new List<Activity>();

        Running run = new Running("03 Nov 2025", 30, 4.8);
        Cycling bike = new Cycling("05 Nov 2025", 45, 20.0);
        Swimming swim = new Swimming("06 Nov 2025", 25, 40);

        activities.Add(run);
        activities.Add(bike);
        activities.Add(swim);

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
