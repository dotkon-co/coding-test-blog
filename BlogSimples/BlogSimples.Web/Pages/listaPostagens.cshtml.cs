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
        public int UserId { get; set; }

        public listaPostagensModel(IPostagemService postagem)
        {
           _postagem = postagem;
        }

        public void Post(string postagemJson) 
        {
            //var decodedPostagemJson = Uri.UnescapeDataString(postagemJson);
            //Postagem = JsonConvert.DeserializeObject<List<Postagem>>(decodedPostagemJson);
        }

        public void OnGet(int userId)
        {
            var lista = _postagem.ListarPostUserIdAsync(userId).Result;
            var postagemJson = JsonConvert.SerializeObject(lista);
            var decodedPostagemJson = Uri.UnescapeDataString(postagemJson);

            Postagem = JsonConvert.DeserializeObject<List<Postagem>>(decodedPostagemJson);
        }
        //public void OnGet(string postagemJson)
        //{
        //    var decodedPostagemJson = Uri.UnescapeDataString(postagemJson);
        //    Postagem = JsonConvert.DeserializeObject<List<Postagem>>(decodedPostagemJson);

        //   // Postagem = postagemJson.ToList();

        //    //var lista = Postagem.ToList();
        //    //int id = UserId;

        //    //int cod = 0;
        //    //if (TempData["UserId"] != null)
        //    //{
        //    //    UserId = TempData["UserId"].ToString();
        //    //}

        //    //if (TempData["Postagem"] != null)
        //    //{
        //    //    Postagem = JsonConvert.DeserializeObject<List<Postagem>>(TempData["Postagem"].ToString());
        //    //}
        //}
        
    }
}
