namespace BlogSystem.Domain.Entities
{
    /// <summary>
    /// Represents a blog post with properties such as Id, Title, Content, AuthorId, CreatedAt, and a reference to the Author.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the unique identifier for the post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the author.
        /// </summary>
        public string? AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the author of the post.
        /// </summary>
        public User? Author { get; set; }
    }
}