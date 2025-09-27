using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var videos = new List<Video>
        {
            new Video("C# OOP Basics", "TechTeacher", 480),
            new Video("Nature Walk in the Alps", "AdventureLife", 600),
            new Video("Best Pasta Recipe", "CookingWithAnna", 420),
            new Video("Funny Puppy Compilation", "DogLover99", 300)
        };

        videos[0].AddComment(new Comment("Alice", "Very helpful explanation!"));
        videos[0].AddComment(new Comment("Bob", "Thanks for the clear examples."));
        videos[0].AddComment(new Comment("Charlie", "Now I understand abstraction."));

        videos[1].AddComment(new Comment("Derek", "Breathtaking scenery."));
        videos[1].AddComment(new Comment("Ella", "I want to visit the Alps now!"));
        videos[1].AddComment(new Comment("Finn", "So relaxing to watch."));

        videos[2].AddComment(new Comment("Grace", "Made this tonight, delicious!"));
        videos[2].AddComment(new Comment("Hank", "Can you share more Italian recipes?"));
        videos[2].AddComment(new Comment("Ivy", "My family loved it."));

        videos[3].AddComment(new Comment("Jack", "That puppy at 2:05 is adorable."));
        videos[3].AddComment(new Comment("Kim", "I can’t stop laughing!"));
        videos[3].AddComment(new Comment("Leo", "Instant mood booster."));

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var c in video.GetComments())
            {
                Console.WriteLine($" - {c.CommenterName}: {c.Text}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}
