using BlogSimples.Web.Models;
using BlogSimples.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BlogSimples.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILoginService _login;
        private readonly IPostagemService _post;

        [BindProperty]
        public Login formLogin { get; set; }
        [BindProperty]
        public int UserId { get; set; } = default!;

        public IndexModel(ILoginService login, IPostagemService post)
        {
            _login = login;
            _post = post;   
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {

            int userId = 0;
            var usuario = new Login { Username = formLogin.Username, Password = formLogin.Password };

            var login = _login.BuscarAsync(usuario).Result;

            if (login is null)
            {
                var retorno = _login.GravarAsync(usuario);
                userId = retorno.Result;
            }
            else
                userId = login.Id;

            //await _post.GravarAsync(new Postagem { UserId = userId, Titulo = "Primeiro Post automatico de TESTE", Postage = "Postagem Teste OK" });

            var listaPostagens = await _post.ListarPostUserIdAsync(userId);
            var postagemJson = JsonConvert.SerializeObject(listaPostagens);
            
            return RedirectToPage("./listaPostagens", new { UserId = userId });

        }

        
    }
}
