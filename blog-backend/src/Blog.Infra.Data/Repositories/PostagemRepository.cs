using Blog.Application.Repositories;
using Blog.Core.RepositorioBase;
using Blog.Domain.Entidades.Postagem;
using Blog.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infra.Data.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly BlogContexto _contexto;
        public IUnitOfWork UnitOfWork => _contexto;

        public PostagemRepository(BlogContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarPostagem(Post post) => await _contexto.Post.AddAsync(post);

        public void AlterarPostagem(Post post) => _contexto.Post.Update(post);

        public void ExcluirPostagem(Post post) => _contexto.Post.Remove(post);

        public async Task<Post> ObterPostagemPorId(Guid postId) =>
            await _contexto.Post
                .Include(p => p.Autor)
                .AsNoTrackingWithIdentityResolution()
                .FirstAsync(p => p.Id == postId);

        public async Task<Post> ObterPostagemPorIdTracking(Guid postId) =>
            await _contexto.Post
                .Include(p => p.Autor)
                .FirstAsync(p => p.Id == postId);

        public IQueryable<Post> ObterTodasPostagens() =>
            _contexto.Post
                .Include(p => p.Autor)
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(x => x.DataCriacao)
                .AsQueryable();

        public void Dispose() => _contexto?.Dispose();
    }
}
