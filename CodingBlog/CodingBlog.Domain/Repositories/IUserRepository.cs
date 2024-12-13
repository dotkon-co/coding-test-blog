namespace CodingBlog.Domain.Repositories;

using System.Threading.Tasks;
using Entities;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
    Task<User> Create(User user);
}