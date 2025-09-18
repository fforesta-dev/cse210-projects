/*
 * Exceeding requirements:
 * - ScriptureLibrary with built-in defaults + optional file load ("scriptures.txt").
 * - HideRandomWords selects from only NON-hidden words.
 * - Extra commands (optional): "new" (pick a new scripture), "count N" (hide N words),
 *   "hint" (reveal one hidden word).
 *   If no extras wanted, just use Enter or type 'quit'.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    internal class Program
    {
        private static readonly Random _rng = new Random();

        static void Main()
        {
            var library = ScriptureLibrary.LoadFromFileOrDefaults("scriptures.txt");

            Scripture scripture = PickRandomScripture(library);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();
                if (scripture.AllHidden())
                {
                    break;
                }

                Console.Write("Press ENTER to hide words, type 'quit' to exit");
                Console.Write(" (extras: 'new', 'hint', 'count N'): ");
                string input = Console.ReadLine();

                if (input == null) continue;
                string cmd = input.Trim();

                if (cmd.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else if (cmd.Equals("new", StringComparison.OrdinalIgnoreCase))
                {
                    scripture = PickRandomScripture(library);
                }
                else if (cmd.StartsWith("count", StringComparison.OrdinalIgnoreCase))
                {
                    int count = ParseCount(cmd) ?? 3;
                    scripture.HideRandomWords(count, onlyFromVisible: true);
                }
                else if (cmd.Equals("hint", StringComparison.OrdinalIgnoreCase))
                {
                    scripture.RevealOneRandomHiddenWord();
                }
                else
                {
                    scripture.HideRandomWords(count: 2, onlyFromVisible: true);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Goodbye!");
        }

        private static int? ParseCount(string cmd)
        {
            var parts = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[1], out int n) && n > 0) return n;
            return null;
        }

        private static Scripture PickRandomScripture(List<Scripture> library)
        {
            int idx = _rng.Next(library.Count);
            return library[idx];
        }
    }
}