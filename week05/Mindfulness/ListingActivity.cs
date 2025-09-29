using System;
using System.Collections.Generic;

namespace Mindfulness
{
    class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private readonly HashSet<int> _usedPromptIdxs = new();

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by listing as many items as you can for a prompt.")
        { }

        public override void Run()
        {
            string prompt = DrawWithoutRepeat(_prompts, _usedPromptIdxs);

            Console.WriteLine("Prompt:\n");
            Console.WriteLine($"  — {prompt}\n");

            Console.WriteLine("You will have a few seconds to think before you start listing.");
            ShowCountdown(5, "Starting in");

            Console.WriteLine("Begin listing (press ENTER after each item).");
            Console.WriteLine($"You have {DurationSeconds} seconds. Go!\n");

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

            Console.WriteLine($"\nTime! You listed {items.Count} item(s).");
            ShowSpinner(2);

            LogSession($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Listing items: {items.Count}");
        }
    }
}
