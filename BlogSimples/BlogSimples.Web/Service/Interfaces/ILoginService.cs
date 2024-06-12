using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;

namespace BlogSimples.Web.Service.Interfaces
{
    public interface ILoginService
    {
        string GravarAsync(Login login);
        Task<IEnumerable<Login>> ListarAsync();
    }
}
