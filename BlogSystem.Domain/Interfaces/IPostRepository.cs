using BlogSystem.Domain.Entities;

namespace BlogSystem.Domain.Interfaces
{
    /// <summary>
    /// Defines methods for managing blog posts in a repository.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Asynchronously gets all posts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of posts.</returns>
        Task<IEnumerable<Post>> GetAllPostsAsync();

        /// <summary>
        /// Asynchronously gets a post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the post.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the post with the specified identifier.</returns>
        Task<Post> GetPostByIdAsync(int id);

        /// <summary>
        /// Asynchronously adds a new post.
        /// </summary>
        /// <param name="post">The post to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddPostAsync(Post post);

        /// <summary>
        /// Asynchronously updates an existing post.
        /// </summary>
        /// <param name="post">The post to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdatePostAsync(Post post);

        /// <summary>
        /// Asynchronously deletes a post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the post to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeletePostAsync(int id);
    }
}