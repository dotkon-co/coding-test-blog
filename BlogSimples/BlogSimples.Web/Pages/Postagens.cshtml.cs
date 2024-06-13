using BlogSimples.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogSimples.Web.Pages
{
    public class PostagensModel : PageModel
    {
        public IEnumerable<Postagem> ListaPostagens { get; set; }

        public PostagensModel()
        {
                ListaPostagens = new List<Postagem>();
        }

        public void OnGet()
        {

        }
    }
}
