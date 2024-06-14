using BlogSimples.Web.Models;
using BlogSimples.Web.Repository.Interfaces;
using BlogSimples.Web.Service.Interfaces;

namespace BlogSimples.Web.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public Task<Login> BuscarAsync(Login login)
        {
            return _loginRepository.GetLoginAsync(login);
        }

        public Task<int> GravarAsync(Login login)
        {
            try
            {
                return _loginRepository.AddLoginAsync(login);

            }catch
            {
                return Task.FromResult(-1);
            }
        }

        public async Task<IEnumerable<Login>> ListarAsync()
        {
           return await _loginRepository.GetAllLoginsAsync();
        }
    }
}
