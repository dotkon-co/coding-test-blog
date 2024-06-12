using BlogSimples.Web.Models;

namespace BlogSimples.Web.Repository.Interfaces
{
    public interface ILoginRepository : IRepository<Login>
    {
        Task<IEnumerable<Login>> GetAllLoginsAsync();
        Task AddLoginAsync(Login login);
       
    }
}
