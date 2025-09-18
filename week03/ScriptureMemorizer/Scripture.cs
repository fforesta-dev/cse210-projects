using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        private static readonly Random Rng = new Random();

        private readonly Reference _reference;
        private readonly List<Word> _words;

        public Scripture(Reference reference, string verseText)
        {
            _reference = reference;
            _words = SplitIntoWords(verseText);
        }

        public string GetDisplayText()
        {
            string text = string.Join(" ", _words.Select(w => w.GetDisplayText()));
            return $"{_reference.GetDisplayText()} — {text}";
        }

        public bool AllHidden() => _words.All(w => w.IsHidden || IsTriviallyEmpty(w));

        public void HideRandomWords(int count = 2, bool onlyFromVisible = true)
        {
            var candidates = onlyFromVisible
                ? _words.Where(w => !w.IsHidden && !IsTriviallyEmpty(w)).ToList()
                : _words.Where(w => !IsTriviallyEmpty(w)).ToList();

            if (candidates.Count == 0) return;

            for (int i = 0; i < count; i++)
            {
                if (candidates.Count == 0) break;

                int idx = Rng.Next(candidates.Count);
                candidates[idx].Hide();

                if (onlyFromVisible)
                {
                    candidates.RemoveAt(idx);
                }
            }
        }

        public void RevealOneRandomHiddenWord()
        {
            var hidden = _words.Where(w => w.IsHidden && !IsTriviallyEmpty(w)).ToList();
            if (hidden.Count == 0) return;
            int idx = Rng.Next(hidden.Count);
            hidden[idx].Reveal();
        }

        private static List<Word> SplitIntoWords(string text)
        {
            return text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                       .Select(token => new Word(token))
                       .ToList();
        }

        private static bool IsTriviallyEmpty(Word w)
        {
            string s = w.ToString();
            return s.All(ch => !char.IsLetter(ch));
        }
    }
}
