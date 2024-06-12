using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;

namespace BlogSimples.Web.Repository
{
    public class PostagemRepository : Repository<Postagem>, IPostagemRepository
    {
        public PostagemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
