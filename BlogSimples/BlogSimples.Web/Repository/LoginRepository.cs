using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;

namespace BlogSimples.Web.Repository
{
    public class LoginRepository : Repository<Login>, ILoginRepository
    {
        public LoginRepository(AppDbContext context) : base(context)
        {
        }
    }

}
