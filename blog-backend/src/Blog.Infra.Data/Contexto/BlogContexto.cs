using Blog.Core.RepositorioBase;
using Blog.Domain.Entidades.ControleAcesso;
using Blog.Domain.Entidades.Postagem;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Data.Contexto
{
    public class BlogContexto : IdentityDbContext<Usuario, Perfil, Guid,
        UsuarioPermissao, UsuarioPerfil, IdentityUserLogin<Guid>, PerfilPermissao, IdentityUserToken<Guid>>, IUnitOfWork
    {
        public BlogContexto(
            DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Perfil> Perfil => Set<Perfil>();
        public DbSet<UsuarioPerfil> UsuarioPerfil => Set<UsuarioPerfil>();

        public DbSet<Post> Post { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<IdentityUserLogin<Guid>>();
            modelBuilder.Ignore<IdentityUserToken<Guid>>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContexto).Assembly);
        }

        public async Task<bool> Commit()
        {
            try
            {
                var sucesso = await base.SaveChangesAsync() > 0;
                return sucesso;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}