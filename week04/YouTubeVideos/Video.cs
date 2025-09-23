using System.Collections.Generic;

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }

    private readonly List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment c) => _comments.Add(c);

    public int GetCommentCount() => _comments.Count;

    public List<Comment> GetComments() => _comments;
}
