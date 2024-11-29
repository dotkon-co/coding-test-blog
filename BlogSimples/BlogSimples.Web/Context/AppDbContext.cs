using BlogSimples.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimples.Web.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<Postagem> Postagens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AppDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasKey(e => e.Id);
            modelBuilder.Entity<Postagem>().HasKey(e => e.PostId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
