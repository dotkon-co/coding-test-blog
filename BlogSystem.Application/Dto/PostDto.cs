namespace BlogSystem.Application.Dto
{
    /// <summary>
    /// Represents a data transfer object for a blog post.
    /// </summary>
    public class PostDto
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
        /// Gets or sets the id of the author of the post.
        /// </summary>
        public string? AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the name of the author of the post.
        /// </summary>
        public string? AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}