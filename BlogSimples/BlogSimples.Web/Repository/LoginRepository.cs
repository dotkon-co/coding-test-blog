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

        public async Task AddLoginAsync(Login login)
        {
            await _context.Logins.AddAsync(login);
            await _context.SaveChangesAsync();
        }

    }

}
