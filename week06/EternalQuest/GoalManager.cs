using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private List<string> _badges = new List<string>();
    private const int LevelStep = 1000;

    public GoalManager() { }

    private int GetLevel() => _score / LevelStep;
    private int PointsToNextLevel() => ((GetLevel() + 1) * LevelStep) - _score;

    private string ProgressBar(int current, int max, int width = 20)
    {
        if (max <= 0) max = 1;
        if (current < 0) current = 0;
        if (current > max) current = max;
        double ratio = (double)current / max;
        int filled = (int)Math.Round(ratio * width);
        return "[" + new string('#', Math.Min(filled, width)) + new string('-', Math.Max(0, width - filled)) + $"] {current}/{max}";
    }

    private void MaybeAwardLevelBadges()
    {
        int level = GetLevel();
        string badge = $"Level {level} Achieved";
        if (level > 0 && !_badges.Contains(badge))
        {
            _badges.Add(badge);
            Console.WriteLine($"🏅 Badge earned: {badge}");
        }
    }

    private void MaybeAwardFirstCompletionBadge(Goal gBefore, Goal gAfter)
    {
        if (gBefore is SimpleGoal && gAfter.IsComplete())
        {
            if (!_badges.Contains("First Finisher"))
            {
                _badges.Add("First Finisher");
                Console.WriteLine("🏅 Badge earned: First Finisher");
            }
        }
    }

    private void MaybeAwardChecklistBadge(ChecklistGoal cg)
    {
        if (cg.IsComplete())
        {
            string badge = $"Checklist Master: {cg.GetShortName()}";
            if (!_badges.Contains(badge))
            {
                _badges.Add(badge);
                Console.WriteLine($"🏅 Badge earned: {badge}");
            }
        }
    }

    private void MaybeAwardEternalStreakBadge(EternalGoal eg)
    {
        if (eg.GetStreak() > 0 && eg.GetStreak() % 7 == 0)
        {
            string badge = $"7-Day Streak: {eg.GetShortName()}";
            if (!_badges.Contains(badge))
            {
                _badges.Add(badge);
                Console.WriteLine($"🏅 Badge earned: {badge}");
            }
        }
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goal Names");
            Console.WriteLine("  3. List Goal Details");
            Console.WriteLine("  4. Record Event");
            Console.WriteLine("  5. Save Goals");
            Console.WriteLine("  6. Load Goals");
            Console.WriteLine("  7. Show Badges");
            Console.WriteLine("  8. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoalNames(); break;
                case "3": ListGoalDetails(); break;
                case "4": RecordEvent(); break;
                case "5": SaveGoals(); break;
                case "6": LoadGoals(); break;
                case "7": ShowBadges(); break;
                case "8": return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = GetLevel();
        int inLevelPoints = _score - level * LevelStep;
        Console.WriteLine($"You have {_score} points.  Level: {level}");
        Console.WriteLine($"{ProgressBar(inLevelPoints, LevelStep)} (+{PointsToNextLevel()} pts to next)");
    }

    public void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
        if (_goals.Count == 0) Console.WriteLine("(No goals yet)");
    }

    public void ListGoalDetails()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
        if (_goals.Count == 0) Console.WriteLine("(No goals yet)");
    }

    public void ShowBadges()
    {
        if (_badges.Count == 0)
        {
            Console.WriteLine("(No badges yet)");
            return;
        }
        Console.WriteLine("Badges:");
        foreach (var b in _badges)
            Console.WriteLine($"- {b}");
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string typeChoice = Console.ReadLine() ?? "";

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? "";
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine() ?? "0");

        switch (typeChoice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine() ?? "0");
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Unknown goal type.");
                break;
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to record yet.");
            return;
        }

        Console.WriteLine("Which goal did you accomplish?");
        ListGoalNames();
        Console.Write("Enter number: ");
        int idx = int.Parse(Console.ReadLine() ?? "0") - 1;
        if (idx < 0 || idx >= _goals.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        Goal g = _goals[idx];

        if (g is SimpleGoal && g.IsComplete())
        {
            Console.WriteLine("That simple goal is already complete. No additional points awarded.");
            return;
        }

        bool wasComplete = g.IsComplete();
        int pointsAwarded = g.GetPoints();
        int bonusAwarded = 0;
        int beforeCount = 0, target = 0, bonus = 0;

        if (g is ChecklistGoal cgBefore)
        {
            beforeCount = cgBefore.GetAmountCompleted();
            target = cgBefore.GetTarget();
            bonus = cgBefore.GetBonus();
        }

        g.RecordEvent();

        if (g is ChecklistGoal cgAfter)
        {
            int after = cgAfter.GetAmountCompleted();
            if (after > beforeCount) _score += pointsAwarded;
            if (after == target) bonusAwarded = bonus;
            MaybeAwardChecklistBadge(cgAfter);
        }
        else
        {
            _score += pointsAwarded;
        }

        if (g is EternalGoal eg)
        {
            MaybeAwardEternalStreakBadge(eg);
        }

        if (!wasComplete && g.IsComplete())
        {
            MaybeAwardFirstCompletionBadge(g, g);
        }

        if (bonusAwarded > 0)
        {
            _score += bonusAwarded;
            Console.WriteLine($"Congratulations! You earned a bonus of {bonusAwarded} points!");
        }

        Console.WriteLine($"Event recorded. You earned {pointsAwarded + bonusAwarded} points.");
        MaybeAwardLevelBadges();
    }

    public void SaveGoals()
    {
        Console.Write("Enter filename to save to: ");
        string filename = Console.ReadLine() ?? "goals.txt";

        using (var writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (var b in _badges) writer.WriteLine($"BADGE|{b}");
            foreach (Goal g in _goals) writer.WriteLine(g.GetStringRepresentation());
        }
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals()
    {
        Console.Write("Enter filename to load from: ");
        string filename = Console.ReadLine() ?? "goals.txt";
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _badges.Clear();

        if (lines.Length == 0) return;

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("BADGE|"))
            {
                _badges.Add(line.Substring("BADGE|".Length));
                continue;
            }

            string[] parts = line.Split('|');
            string type = parts[0];

            switch (type)
            {
                case "SimpleGoal":
                    {
                        string name = parts[1];
                        string desc = parts[2];
                        int pts = int.Parse(parts[3]);
                        bool isComplete = bool.Parse(parts[4]);
                        var sg = new SimpleGoal(name, desc, pts);
                        if (isComplete) sg.RecordEvent();
                        _goals.Add(sg);
                        break;
                    }
                case "EternalGoal":
                    {
                        string name = parts[1];
                        string desc = parts[2];
                        int pts = int.Parse(parts[3]);
                        if (parts.Length >= 6)
                        {
                            int streak = int.Parse(parts[4]);
                            long ticks = long.Parse(parts[5]);
                            _goals.Add(EternalGoal.FromParts(name, desc, pts, streak, ticks));
                        }
                        else
                        {
                            _goals.Add(new EternalGoal(name, desc, pts));
                        }
                        break;
                    }
                case "ChecklistGoal":
                    {
                        string name = parts[1];
                        string desc = parts[2];
                        int pts = int.Parse(parts[3]);
                        int amountCompleted = int.Parse(parts[4]);
                        int targ = int.Parse(parts[5]);
                        int bon = int.Parse(parts[6]);
                        var cg = new ChecklistGoal(name, desc, pts, targ, bon);
                        for (int k = 0; k < amountCompleted; k++) cg.RecordEvent();
                        _goals.Add(cg);
                        break;
                    }
            }
        }

        Console.WriteLine("Goals loaded.");
        MaybeAwardLevelBadges();
    }
}
