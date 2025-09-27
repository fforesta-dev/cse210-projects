using System;
using System.Collections.Generic;
using System.IO;

namespace ScriptureMemorizer
{
    public static class ScriptureLibrary
    {
        public static List<Scripture> LoadFromFileOrDefaults(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var list = new List<Scripture>();
                    foreach (var line in File.ReadAllLines(path))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var parts = line.Split('|', 4, StringSplitOptions.None);
                        if (parts.Length < 4) continue;

                        string book = parts[0].Trim();
                        if (!int.TryParse(parts[1], out int chapter)) continue;

                        var versePart = parts[2].Trim();
                        var text = parts[3].Trim();

                        Reference reference;
                        if (versePart.Contains('-'))
                        {
                            var r = versePart.Split('-', StringSplitOptions.RemoveEmptyEntries);
                            if (r.Length == 2 &&
                                int.TryParse(r[0], out int s) &&
                                int.TryParse(r[1], out int e))
                            {
                                reference = new Reference(book, chapter, s, e);
                            }
                            else continue;
                        }
                        else
                        {
                            if (!int.TryParse(versePart, out int v)) continue;
                            reference = new Reference(book, chapter, v);
                        }

                        list.Add(new Scripture(reference, text));
                    }

                    if (list.Count > 0) return list;
                }
                catch
                {
                }
            }

            return new List<Scripture>
            {
                new Scripture(
                    new Reference("John", 3, 16),
                    "For God so loved the world, that he gave his only begotten Son, " +
                    "that whosoever believeth in him should not perish, but have everlasting life."
                ),
                new Scripture(
                    new Reference("Proverbs", 3, 5, 6),
                    "Trust in the LORD with all thine heart; and lean not unto thine own understanding. " +
                    "In all thy ways acknowledge him, and he shall direct thy paths."
                ),
                new Scripture(
                    new Reference("Moroni", 10, 5),
                    "And by the power of the Holy Ghost ye may know the truth of all things."
                )
            };
        }
    }
}
