using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Models;

namespace BlogAPI.Data
{
    public class BlogContext : IdentityDbContext<User>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed data
            builder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "First Post", Content = "This is the first post." },
                new Post { Id = 2, Title = "Second Post", Content = "This is the second post." }
            );
        }
    }
}
