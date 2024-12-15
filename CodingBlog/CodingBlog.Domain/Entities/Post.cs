namespace CodingBlog.Domain.Entities;

public class Post
{
    public Post()
    {
        CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public int UserId { get; set; }
    public User User { get; set; }
}