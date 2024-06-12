using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;

namespace BlogSimples.Web.Service.Interfaces
{
    public interface ILoginService
    {
        Task<IEnumerable<Login>> GetAllLoginsAsync();
        Task<Login> GetLoginByIdAsync(int id);
        Task AddLoginAsync(Login login);
        void UpdateLogin(Login login);
        void DeleteLogin(Login login);
    }
}
