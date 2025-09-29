// ========================
// EXCEEDS REQUIREMENTS:
// 1) Session log file (mindfulness_log.txt) with timestamps.
// 2) No-repeat random prompts/questions until all used (per session).
// 3) Enhanced breathing animation (smooth in/out “bubble”).
// 4) Extra activity: GratitudeActivity.
// ========================

using System;
using System.Collections.Generic;

namespace Mindfulness
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var activities = new Dictionary<string, Activity>
            {
                { "1", new BreathingActivity() },
                { "2", new ReflectingActivity() },
                { "3", new ListingActivity() },
                { "4", new GratitudeActivity() }
            };

            while (true)
            {
                Console.Clear();
                WriteHeader("Mindfulness Program");
                Console.WriteLine("Menu:");
                Console.WriteLine("  1) Breathing Activity");
                Console.WriteLine("  2) Reflection Activity");
                Console.WriteLine("  3) Listing Activity");
                Console.WriteLine("  4) Gratitude Activity");
                Console.WriteLine("  0) Quit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine()?.Trim() ?? "";
                if (choice == "0")
                {
                    Console.WriteLine("\nGoodbye! Take a mindful moment again soon.");
                    return;
                }

                if (!activities.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice. Press ENTER to continue...");
                    Console.ReadLine();
                    continue;
                }

                var selected = activities[choice];

                Console.Clear();
                selected.ShowIntro();
                int duration = ReadDurationSeconds();
                if (duration <= 0) continue;

                selected.DurationSeconds = duration;
                Console.WriteLine("\nGet ready to begin...");
                selected.ShowSpinner(3);

                Console.Clear();
                selected.Run();

                selected.ShowOutro();
                Console.WriteLine("\nPress ENTER to return to the menu...");
                Console.ReadLine();
            }
        }

        private static int ReadDurationSeconds()
        {
            while (true)
            {
                Console.Write("\nEnter duration (seconds): ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive integer.");
            }
        }

        public static void WriteHeader(string text)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine(text);
            Console.WriteLine("=====================================\n");
        }
    }
}
