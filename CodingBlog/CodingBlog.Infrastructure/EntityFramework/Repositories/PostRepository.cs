namespace CodingBlog.Infrastructure.EntityFramework.Repositories;

using Domain.Entities;
using CodingBlog.Domain.Repositories;
using Configuration;
using Microsoft.EntityFrameworkCore;

public class PostRepository : IPostRepository
{
    private readonly PostgresDBContext _context;

    public PostRepository(PostgresDBContext context)
      =>  _context = context;

    public async Task<List<Post>> GetAll()
        => await _context.Posts.ToListAsync();

    public async Task<Post> Create(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> Update(int id, Post post)
    {
        var existingPost = await _context.Posts.FindAsync(id);
        if (existingPost == null) return null;

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        await _context.SaveChangesAsync();
        return existingPost;
    }

    public async Task<bool> Delete(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }
}