using BlogSimples.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimples.Web.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<Postagens> Postagen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AppDb");
        }
    }
}
