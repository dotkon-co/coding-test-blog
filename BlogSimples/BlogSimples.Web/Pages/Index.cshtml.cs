using BlogSimples.Web.Models;
using BlogSimples.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogSimples.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILoginService _login;

        List<Login> listaLogin = new List<Login>();

        [BindProperty]
        public Login formLogin { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ILoginService login )
        {
            _logger = logger;
            _login = login;
        }

        public void OnGet()
        {

        }

        public void OnPost() {

            string usuario = formLogin.Username;
            string password = formLogin.Password;

            _login.GravarAsync(new Login { Username = usuario, Password = password });

            var lista = _login.ListarAsync();
            //string testo = "~hjhj";
        }
    }
}
