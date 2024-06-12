using BlogSystem.Application.Dto;
using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                AuthorName = p.Author.UserName,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorName = post.Author.UserName,
                CreatedAt = post.CreatedAt
            };
        }

        public async Task AddPostAsync(PostDto postDto)
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                AuthorId = postDto.AuthorId,
                CreatedAt = DateTime.UtcNow
            };
            await _postRepository.AddPostAsync(post);
        }

        public async Task UpdatePostAsync(PostDto postDto)
        {
            var post = new Post
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Content = postDto.Content,
                AuthorId = postDto.AuthorId,
                CreatedAt = postDto.CreatedAt
            };
            await _postRepository.UpdatePostAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeletePostAsync(id);
        }
    }
}