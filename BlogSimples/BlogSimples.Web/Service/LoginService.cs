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

        public async Task<IEnumerable<Login>> GetAllLoginsAsync()
        {
            return await _loginRepository.GetAllAsync();
        }

        public async Task<Login> GetLoginByIdAsync(int id)
        {
            return await _loginRepository.GetByIdAsync(id);
        }

        public async Task AddLoginAsync(Login login)
        {
            await _loginRepository.AddAsync(login);
        }

        public void UpdateLogin(Login login)
        {
            _loginRepository.Update(login);
        }

        public void DeleteLogin(Login login)
        {
            _loginRepository.Delete(login);
        }
    }
}
