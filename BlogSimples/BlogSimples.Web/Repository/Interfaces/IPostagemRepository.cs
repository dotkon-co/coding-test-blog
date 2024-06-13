using BlogSimples.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSimples.Web.Repository.Interfaces
{
    public interface IPostagemRepository : IRepository<Postagem>
    {
        Task<IEnumerable<Postagem>> GetAllPostagensAsync();

        Task<int> AddPostagemAsync(Postagem post);

        Task UpdatePostagemAsync(Postagem post);

        Task DeletePostagemAsync(Postagem post);

        Task<Postagem> GetPostagemAsync(int postId);
       
    }
}
