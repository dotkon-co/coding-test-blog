using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CodingBlog.Infrastructure.EntityFramework.Configuration;

using Domain.Entities;
using ModelConfiguration;
using Microsoft.EntityFrameworkCore;

public class PostgresDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public PostgresDBContext(DbContextOptions<PostgresDBContext> options)
        : base(options)
    {
       
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        modelBuilder.ApplyConfiguration(new UserModelConfiguration());
        modelBuilder.ApplyConfiguration(new PostModelConfiguration());


        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "user",
                Email = "user@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "Editor"
            }
        );

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                Title = "Post 1",
                Content = "Content of Post 1",
                CreatedAt = DateTime.Now,
                UserId = 1
            },
            new Post
            {
                Id = 2,
                Title = "Post 2",
                Content = "Content of Post 2",
                CreatedAt = DateTime.Now,
                UserId = 2
            },
            new Post
            {
                Id = 3,
                Title = "Post 3",
                Content = "Content of Post 3",
                CreatedAt = DateTime.Now,
                UserId = 1
            },
            new Post
            {
                Id = 4,
                Title = "Post 4",
                Content = "Content of Post 4",
                CreatedAt = DateTime.Now,
                UserId = 2
            }
        );
    }
}