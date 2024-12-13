namespace CodingBlog.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

public interface IPostRepository
{
    Task<List<Post>> GetAll();
    Task<Post> Create(Post post);
    Task<Post?> Update(int id, Post post);
    Task<bool> Delete(int id);
}