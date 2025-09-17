using System;
using System.Collections.Generic;

public class PromptGenerator {
    private readonly List < string > _prompts = new List < string > {
        "What moment today felt most alive for me?",
        "When did I feel most at peace?",
        "Where did I spend most of my energy today?",
        "What is one thing I would repeat from today?",
        "What is one thing I would do differently?",
        "What surprised me today?",
        "What took longer than I expected and why?",
        "What did I notice on my commute or walk that I usually miss?",
        "Three small mercies I noticed today were…",
        "Who helped me today and how can I thank them?",
        "What ordinary object served me well today?",
        "What taste, smell, or sound brought me joy?",
        "What technology made my day easier?",
        "What blessing do I often overlook?",
        "How did someone show me patience today?",
        "What emotion visited me most today and what was its message?",
        "When did I feel stressed and how did I respond?",
        "What did my body need today that I gave or withheld?",
        "What restful moment did I protect?",
        "What gave me energy today?",
        "What boundary did I honor?",
        "What thought loop do I want to release?",
        "What did I learn today that changed how I see a problem?",
        "What mistake taught me something useful?",
        "What skill moved forward, even 1%?",
        "What question am I sitting with right now?",
        "Who taught me something today?",
        "What feedback did I receive and how will I apply it?",
        "What did I read, watch, or hear that mattered?",
        "Which task moved the needle most and why?",
        "What was the hardest part of my work and how did I tackle it?",
        "What did I postpone and why?",
        "How did I make a process clearer or faster?",
        "Where did I show initiative?",
        "What will be my first, smallest step tomorrow?",
        "What tool or shortcut saved time?",
        "Who needed encouragement today and how did I respond?",
        "How did I listen well today?",
        "What act of service did I give or receive?",
        "How did I strengthen trust with someone?",
        "Who deserves a note or message from me?",
        "What conflict did I handle with grace?",
        "Whom did I include today?",
        "Where did I feel the Spirit today?",
        "How did I see the hand of the Lord in my life today?",
        "Which scripture stood out and why?",
        "What did I learn from prayer today?",
        "Did I act on a prompting today?",
        "How did I serve in my calling or community?",
        "What truth felt new or freshly remembered?",
        "How did I keep my covenants today?",
        "What blessing can I ask for others tonight?",
        "How did I show Christlike love today?",
        "Which habit got 1% better today?",
        "What tiny win am I proud of?",
        "What cue or environment helped my habit stick?",
        "What obstacle did I design around?",
        "What goal still matters and why?",
        "What is my next right step?",
        "What idea did I capture today?",
        "If I could explore any topic for an hour, what would it be?",
        "What did I make, even if it was small?",
        "What constraint sparked creativity today?",
        "What question do I wish someone would ask me?"
    };

    private readonly Random _rand = new Random();

    private readonly HashSet < int > _usedIndices = new HashSet < int > ();

    public string GetRandomPrompt() {
        if (_prompts.Count == 0) return "";

        if (_usedIndices.Count == _prompts.Count) {
            _usedIndices.Clear();
        }

        int i;
        do {
            i = _rand.Next(_prompts.Count);
        } while (_usedIndices.Contains(i) && _usedIndices.Count < _prompts.Count);

        _usedIndices.Add(i);
        return _prompts[i];
    }
}