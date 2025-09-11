/*
 * Creativity:
 * 1) JSON persistence: If filename ends with .json, Journal.SaveToFile/LoadFromFile
 *    uses System.Text.Json with IncludeFields=true to serialize/deserialize the full journal.
 * 2) Optional mood per entry (_mood) that displays if present.
 * 3) PromptGenerator avoids repeats until all prompts have been used in the current session.
 */

using System;

class Program {
    static void Main(string[] args) {
        var journal = new Journal();
        var prompts = new PromptGenerator();

        while (true) {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file (.txt or .json)");
            Console.WriteLine("4. Load the journal from a file (.txt or .json)");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1") {
                string prompt = prompts.GetRandomPrompt();
                Console.WriteLine();
                Console.WriteLine($"Prompt: {prompt}");
                Console.Write("> ");
                string entryText = Console.ReadLine() ?? "";

                Console.Write("Mood (optional): ");
                string mood = Console.ReadLine() ?? "";

                var entry = new Entry {
                    _date = DateTime.Now.ToShortDateString(),
                        _promptText = prompt,
                        _entryText = entryText,
                        _mood = string.IsNullOrWhiteSpace(mood) ? "" : mood.Trim()
                };

                journal.AddEntry(entry);
                Console.WriteLine("Entry added.");
            } else if (choice == "2") {
                journal.DisplayAll();
            } else if (choice == "3") {
                Console.Write("Filename to save (e.g., journal.txt or journal.json): ");
                string file = Console.ReadLine() ?? "";
                try {
                    journal.SaveToFile(file);
                    Console.WriteLine("Saved.");
                } catch (Exception ex) {
                    Console.WriteLine($"Save failed: {ex.Message}");
                }
            } else if (choice == "4") {
                Console.Write("Filename to load (e.g., journal.txt or journal.json): ");
                string file = Console.ReadLine() ?? "";
                try {
                    journal.LoadFromFile(file);
                    Console.WriteLine("Loaded.");
                } catch (Exception ex) {
                    Console.WriteLine($"Load failed: {ex.Message}");
                }
            } else if (choice == "5") {
                break;
            } else {
                Console.WriteLine("Please choose 1–5.");
            }
        }
    }
}