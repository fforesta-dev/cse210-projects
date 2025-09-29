using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Mindfulness
{
    abstract class Activity
    {
        protected string Name { get; }
        protected string Description { get; }
        public int DurationSeconds { get; set; }

        private static readonly char[] _spinnerChars = new[] { '|', '/', '-', '\\' };
        private readonly Random _rng = new();

        protected Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void ShowIntro()
        {
            Program.WriteHeader(Name);
            Console.WriteLine(Description);
        }

        public void ShowOutro()
        {
            Console.WriteLine("\nGreat job! Take a breath and notice how you feel.");
            ShowSpinner(2);
            Console.WriteLine($"\nYou completed the {Name} for {DurationSeconds} seconds.");
            ShowSpinner(3);

            LogSession($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {Name} | Duration: {DurationSeconds}s");
        }

        protected void LogSession(string line)
        {
            try
            {
                File.AppendAllLines("mindfulness_log.txt", new[] { line });
            }
            catch
            {
            }
        }

        public void ShowSpinner(int seconds)
        {
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < end)
            {
                Console.Write(_spinnerChars[i % _spinnerChars.Length]);
                Thread.Sleep(120);
                Console.Write("\b \b");
                i++;
            }
        }

        public void ShowCountdown(int seconds, string? leadText = null)
        {
            if (!string.IsNullOrWhiteSpace(leadText))
            {
                Console.Write(leadText.TrimEnd() + " ");
            }
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
        }

        protected int ClampPositiveSeconds(int requested) => Math.Max(1, requested);

        protected T DrawWithoutRepeat<T>(List<T> pool, HashSet<int> usedIndexes)
        {
            if (pool.Count == 0) throw new InvalidOperationException("Pool is empty.");

            if (usedIndexes.Count == pool.Count)
            {
                usedIndexes.Clear();
            }

            int idx;
            do
            {
                idx = _rng.Next(pool.Count);
            }
            while (usedIndexes.Contains(idx) && usedIndexes.Count < pool.Count);

            usedIndexes.Add(idx);
            return pool[idx];
        }

        public abstract void Run();
    }
}
