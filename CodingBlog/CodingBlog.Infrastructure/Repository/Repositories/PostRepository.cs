using CodingBlog.Domain.Entities;
using CodingBlog.Domain.Repositories;
using CodingBlog.Infrastructure.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodingBlog.Infrastructure.EntityFramework.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostgresDBContext _context;

    public PostRepository(PostgresDBContext context)
        => _context = context;

    public async Task<List<Post>> GetAll(CancellationToken cancellationToken)
        => await _context.Posts.ToListAsync(cancellationToken);

    public async Task<Post> Create(Post post, CancellationToken cancellationToken)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
        return post;
    }

    public async Task<Post?> Update(int id, Post post, CancellationToken cancellationToken)
    {
        var existingPost = await _context.Posts.FindAsync(id, cancellationToken);
        if (existingPost == null) return null;

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        await _context.SaveChangesAsync(cancellationToken);
        return existingPost;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FindAsync(id, cancellationToken);
        if (post == null) return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}