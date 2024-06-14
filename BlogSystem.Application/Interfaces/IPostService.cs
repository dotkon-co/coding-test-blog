using BlogSystem.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogSystem.Application.Interfaces
{
    /// <summary>
    /// Defines methods for managing blog posts in the application layer.
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Asynchronously gets all posts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of post DTOs.</returns>
        Task<IEnumerable<PostDto>> GetAllPostsAsync();

        /// <summary>
        /// Asynchronously gets a post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the post.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the post DTO with the specified identifier.</returns>
        Task<PostDto> GetPostByIdAsync(int id);

        /// <summary>
        /// Asynchronously adds a new post.
        /// </summary>
        /// <param name="postDto">The post DTO to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddPostAsync(PostDto postDto);

        /// <summary>
        /// Asynchronously updates an existing post.
        /// </summary>
        /// <param name="postDto">The post DTO to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdatePostAsync(PostDto postDto);

        /// <summary>
        /// Asynchronously deletes a post by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the post to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeletePostAsync(int id);
    }
}