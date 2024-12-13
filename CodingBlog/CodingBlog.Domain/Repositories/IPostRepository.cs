namespace CodingBlog.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

public interface IPostRepository
{
    Task<List<Post>> GetAll(CancellationToken cancellationToken);
    Task<Post> Create(Post post, CancellationToken cancellationToken);
    Task<Post?> Update(int id, Post post, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}