using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogSimples.Web.Repository
{
    public class PostagemRepository : Repository<Postagem>, IPostagemRepository
    {
        public PostagemRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Postagem>> GetAllPostagensAsync()
        {
            return await _context.Postagens.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Postagem>> GetPostagensIdAsync(int userId)
        {
            return await _context.Postagens.AsNoTracking().Where(x=> x.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<int> AddPostagemAsync(Postagem post)
        {
            var postagem = await _context.Postagens.AddAsync(post);
            await _context.SaveChangesAsync();

            return postagem.Entity.PostId;
        }

        public async Task UpdatePostagemAsync(Postagem post)
        {
            var postagem = _context.Postagens.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostagemAsync(Postagem post)
        {
            var postagem = _context.Postagens.Remove(post);
            await _context.SaveChangesAsync();
        }


        public async Task<Postagem> GetPostagemAsync(int postId)
        {
            return await _context.Postagens.FindAsync(postId);
        }
    }
}
