using BlogSimples.Web.Models;

namespace BlogSimples.Web.Repository.Interfaces
{
    public interface ILoginRepository : IRepository<Login>
    {
        Task<IEnumerable<Login>> GetAllLoginsAsync();
        Task<int> AddLoginAsync(Login login);
        Task<Login> GetLoginAsync(Login login);


    }
}
