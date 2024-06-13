using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogSimples.Web.Repository
{
    public class LoginRepository : Repository<Login>, ILoginRepository
    {

        public LoginRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Login>> GetAllLoginsAsync()
        {
            return await _context.Logins.AsNoTracking().ToListAsync();
        }

        public async Task<int> AddLoginAsync(Login login)
        {
            var usuario = await _context.Logins.AddAsync(login);
            await _context.SaveChangesAsync();

            return usuario.Entity.Id;
        }

        public async Task<Login> GetLoginAsync(Login login)
        {
            return await _context.Logins.Where(x=> x.Username.Equals(login.Username) && x.Password.Equals(login.Password)).FirstOrDefaultAsync();
        }

    }

}
