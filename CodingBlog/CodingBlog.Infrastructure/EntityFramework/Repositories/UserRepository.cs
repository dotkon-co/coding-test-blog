namespace CodingBlog.Infrastructure.EntityFramework.Repositories;

using Domain.Entities;
using CodingBlog.Domain.Repositories;
using Configuration;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly PostgresDBContext _context;

    public UserRepository(PostgresDBContext context)
        =>  _context = context;
    
    public async Task<User?> GetByUsername(string username)
      => await _context.Users.FirstOrDefaultAsync(u => u.Email == username);

    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}