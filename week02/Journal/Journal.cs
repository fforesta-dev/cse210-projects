using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class Journal {
    private List < Entry > _entries = new List < Entry > ();

    private
    const string SEP = " ~|~ ";

    public void AddEntry(Entry newEntry) {
        if (newEntry == null) return;
        _entries.Add(newEntry);
    }

    public void DisplayAll() {
        if (_entries.Count == 0) {
            Console.WriteLine("No entries yet.");
            return;
        }

        foreach(var e in _entries) {
            e.Display();
            Console.WriteLine(new string('-', 50));
        }
    }

    public void SaveToFile(string file) {
        if (string.IsNullOrWhiteSpace(file))
            throw new ArgumentException("Filename cannot be empty.");

        if (file.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
            var options = new JsonSerializerOptions {
                WriteIndented = true, IncludeFields = true
            };
            string json = JsonSerializer.Serialize(_entries, options);
            File.WriteAllText(file, json, Encoding.UTF8);
            return;
        }

        using(var sw = new StreamWriter(file, false, Encoding.UTF8)) {
            foreach(var e in _entries) {
                string date = Sanitize(e._date);
                string prompt = Sanitize(e._promptText);
                string text = Sanitize(e._entryText);
                string mood = Sanitize(e._mood);

                sw.WriteLine($"{date}{SEP}{prompt}{SEP}{text}{SEP}{mood}");
            }
        }
    }

    public void LoadFromFile(string file) {
        if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
            throw new FileNotFoundException("File not found.", file);

        if (file.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
            var options = new JsonSerializerOptions {
                IncludeFields = true
            };
            string json = File.ReadAllText(file, Encoding.UTF8);
            var list = JsonSerializer.Deserialize < List < Entry >> (json, options) ?? new List < Entry > ();
            _entries = list;
            return;
        }

        var loaded = new List < Entry > ();
        string[] lines = File.ReadAllLines(file, Encoding.UTF8);

        foreach(var line in lines) {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] parts = line.Split(SEP, StringSplitOptions.None);

            if (parts.Length >= 3) {
                var e = new Entry {
                    _date = Desanitize(parts[0]),
                        _promptText = Desanitize(parts[1]),
                        _entryText = Desanitize(parts[2]),
                        _mood = parts.Length >= 4 ? Desanitize(parts[3]) : ""
                };
                loaded.Add(e);
            }
        }

        _entries = loaded;
    }

    private static string Sanitize(string s) =>
        (s ?? "").Replace("\r\n", "\\n").Replace("\n", "\\n");

    private static string Desanitize(string s) =>
        (s ?? "").Replace("\\n", Environment.NewLine);
}