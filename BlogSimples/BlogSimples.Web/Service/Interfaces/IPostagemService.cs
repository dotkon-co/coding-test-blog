using BlogSimples.Web.Models;

namespace BlogSimples.Web.Service.Interfaces
{
    public interface IPostagemService
    {
        Task<int> GravarAsync(Postagem post);
        Task<IEnumerable<Postagem>> ListarAsync();
        Task<Postagem> BuscarIdAsync(int postId);
        Task AlterarAsync(Postagem post);
        Task DeletarAsync(Postagem post);
    }
}
