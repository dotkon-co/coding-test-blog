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

        public string GravarAsync(Login login)
        {
            try
            {
                _loginRepository.AddLoginAsync(login);
                return "Gravado com sucesso.";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<Login>> ListarAsync()
        {
           return await _loginRepository.GetAllLoginsAsync();
        }
    }
}
