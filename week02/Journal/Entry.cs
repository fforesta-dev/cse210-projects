using System;

public class Entry {
    public string _date = "";
    public string _promptText = "";
    public string _entryText = "";
    public string _mood = "";

    public void Display() {
        Console.WriteLine($"{_date} — {_promptText}");
        if (!string.IsNullOrWhiteSpace(_mood)) {
            Console.WriteLine($"Mood: {_mood}");
        }
        Console.WriteLine(_entryText);
    }
}