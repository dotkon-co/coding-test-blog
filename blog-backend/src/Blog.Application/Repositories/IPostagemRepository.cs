using Blog.Core.RepositorioBase;
using Blog.Domain.Entidades.Postagem;

namespace Blog.Application.Repositories
{
    public interface IPostagemRepository : IRepository<Post>
    {
        Task AdicionarPostagem(Post post);
        void AlterarPostagem(Post post);
        void ExcluirPostagem(Post post);

        Task<Post> ObterPostagemPorId(Guid postId);
        Task<Post> ObterPostagemPorIdTracking(Guid postId);

        IQueryable<Post> ObterTodasPostagens();
    }
}
