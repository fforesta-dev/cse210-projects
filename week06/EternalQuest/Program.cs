/*
 * EXCEED REQUIREMENTS:
 * 1) Leveling System — Level increases every 1000 points. UI shows points to next level.
 * 2) Badges — Earn milestone badges. Badges are persisted in the save file as lines starting with "BADGE|".
 * 3) Eternal Streaks — Eternal goals track daily streaks; every 7th consecutive day awards a badge.
 */

using System;

class Program
{
    static void Main(string[] args)
    {
        var manager = new GoalManager();
        manager.Start();
    }
}
