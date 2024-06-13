using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;

namespace BlogSimples.Web.Service.Interfaces
{
    public interface ILoginService
    {
        Task<int> GravarAsync(Login login);
        Task<IEnumerable<Login>> ListarAsync();
        Task<Login> BuscarAsync(Login login);
    }
}
