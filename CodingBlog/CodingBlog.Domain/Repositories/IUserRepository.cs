namespace CodingBlog.Domain.Repositories;

using System.Threading.Tasks;
using Entities;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
    Task<User> Create(User user, CancellationToken cancellationToken);
}