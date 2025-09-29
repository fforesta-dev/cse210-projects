using System;
using System.Collections.Generic;

namespace Mindfulness
{
    class ReflectingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private readonly HashSet<int> _usedPromptIdxs = new();
        private readonly HashSet<int> _usedQuestionIdxs = new();

        public ReflectingActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience.")
        { }

        public override void Run()
        {
            string prompt = DrawWithoutRepeat(_prompts, _usedPromptIdxs);
            Console.WriteLine("Consider this prompt:\n");
            Console.WriteLine($"  — {prompt}\n");

            Console.WriteLine("Focus on the details. We will begin reflecting shortly...");
            ShowSpinner(3);

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            int questionsAsked = 0;

            while (DateTime.Now < end)
            {
                string q = DrawWithoutRepeat(_questions, _usedQuestionIdxs);
                Console.Write($"\n→ {q} ");
                ShowSpinner(6);
                questionsAsked++;
            }

            LogSession($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Reflection Qs asked: {questionsAsked}");
        }
    }
}
