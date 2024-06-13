using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Service.Interfaces;
using Newtonsoft.Json;

namespace BlogSimples.Web.Pages
{
    public class listaPostagensModel : PageModel
    {
        private readonly IPostagemService _postagem;
        public IEnumerable<Postagem> Postagem { get;set; }
        
        [BindProperty]
        public int UserId { get; set; } = default!;

        public listaPostagensModel(IPostagemService postagem)
        {
           _postagem = postagem;
        }

        public void Post() 
        {
            int cod = 0;
            //var decodedPostagemJson = Uri.UnescapeDataString(postagemJson);
            //Postagem = JsonConvert.DeserializeObject<List<Postagem>>(decodedPostagemJson);
        }

        public void OnGet(int userId)
        {
           
            var lista = _postagem.ListarPostUserIdAsync(userId).Result;
            var postagemJson = JsonConvert.SerializeObject(lista);
            var decodedPostagemJson = Uri.UnescapeDataString(postagemJson);

            Postagem = JsonConvert.DeserializeObject<List<Postagem>>(decodedPostagemJson);

            UserId = 0;
        }        
        
    }
}
