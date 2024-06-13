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

namespace BlogSimples.Web.Pages
{
    public class listaPostagensModel : PageModel
    {
        private readonly IPostagemService _postagem;

        public listaPostagensModel(IPostagemService postagem)
        {
            _postagem = postagem;
        }
        //private readonly BlogSimples.Web.Context.AppDbContext _context;

        //public listaPostagensModel(BlogSimples.Web.Context.AppDbContext context)
        //{
        //    _context = context;
        //}

        public IList<Postagem> Postagem { get;set; } = default!;

        public async Task<IEnumerable<Postagem>> Listar()
        {
            return await _postagem.ListarAsync();
        }
    }
}
