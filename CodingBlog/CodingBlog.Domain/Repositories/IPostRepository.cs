using CodingBlog.Domain.Entities;

namespace CodingBlog.Domain.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetAll(CancellationToken cancellationToken);
    Task<Post?> GetById(int id, CancellationToken cancellationToken);
    Task<Post> Create(Post post, CancellationToken cancellationToken);
    Task<Post?> Update(int id, Post post, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}