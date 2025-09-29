using System;
using System.Threading;

namespace Mindfulness
{
    class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through slow breaths. Clear your mind and focus on your breathing.")
        { }

        public override void Run()
        {
            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            bool breatheIn = true;

            while (DateTime.Now < end)
            {
                if (breatheIn)
                {
                    Console.Write("Breathe in... ");
                    ShowCountdown(4);
                    SmoothBreath(4, true);
                }
                else
                {
                    Console.Write("Breathe out...");
                    ShowCountdown(4);
                    SmoothBreath(4, false);
                }
                breatheIn = !breatheIn;
                Console.WriteLine();
            }
        }

        private void SmoothBreath(int seconds, bool inhaling)
        {
            seconds = ClampPositiveSeconds(seconds);
            int frames = seconds * 10;
            int maxSize = 10;
            for (int f = 0; f < frames; f++)
            {
                double t = (double)f / (frames - 1);
                double eased = 0.5 - 0.5 * Math.Cos(Math.PI * t);
                int size = inhaling
                    ? (int)Math.Round(1 + eased * (maxSize - 1))
                    : (int)Math.Round(maxSize - eased * (maxSize - 1));

                string bubble = new string('•', Math.Max(1, size));
                Console.Write(" " + bubble);
                Thread.Sleep(100);

                Console.Write(new string('\b', bubble.Length + 1));
                Console.Write(new string(' ', bubble.Length + 1));
                Console.Write(new string('\b', bubble.Length + 1));
            }
        }
    }
}
