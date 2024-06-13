using BlogSimples.Web.Models;
using BlogSimples.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogSimples.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILoginService _login;

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

        public IActionResult OnPost() {

            //string usuario = formLogin.Username;
            //string password = formLogin.Password;
            TempData["userId"] = "0";
            var usuario = new Login { Username = formLogin.Username, Password = formLogin.Password };

            var login = _login.BuscarAsync(usuario).Result;

            if ( login is null)
            {
                var retorno =_login.GravarAsync(usuario);
                TempData["userId"] = retorno.Result.ToString();
            }else
                TempData["userId"] = login.Id;

            return RedirectToPage("./Postagens");
            

        }
    }
}
