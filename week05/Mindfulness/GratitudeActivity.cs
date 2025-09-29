using System;
using System.Collections.Generic;

namespace Mindfulness
{
    class GratitudeActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "List moments from today you’re grateful for.",
            "List people who support you (and why).",
            "List blessings you often overlook.",
            "List recent answers to prayer or guidance you felt."
        };

        private readonly HashSet<int> _usedPromptIdxs = new();

        public GratitudeActivity() : base(
            "Gratitude Activity",
            "This activity helps you broaden gratitude by listing blessings and supports in your life.")
        { }

        public override void Run()
        {
            string prompt = DrawWithoutRepeat(_prompts, _usedPromptIdxs);

            Console.WriteLine("Prompt:\n");
            Console.WriteLine($"  — {prompt}\n");

            Console.WriteLine("We will begin shortly...");
            ShowCountdown(5, "Starting in");

            Console.WriteLine("Write as many as you can. Press ENTER after each item.\n");

            List<string> items = new();
            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);

            while (DateTime.Now < end)
            {
                if (Console.KeyAvailable)
                {
                    string? line = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        items.Add(line.Trim());
                    }
                }
                else
                {
                    ShowSpinner(1);
                }
            }

            Console.WriteLine($"\nWonderful! You listed {items.Count} gratitude item(s).");
            ShowSpinner(2);

            LogSession($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Gratitude items: {items.Count}");
        }
    }
}
