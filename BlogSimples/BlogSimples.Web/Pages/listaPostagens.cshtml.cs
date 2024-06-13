using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;

namespace BlogSimples.Web.Pages
{
    public class listaPostagensModel : PageModel
    {
        private readonly BlogSimples.Web.Context.AppDbContext _context;

        public listaPostagensModel(BlogSimples.Web.Context.AppDbContext context)
        {
            _context = context;
        }

        public IList<Postagem> Postagem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Postagem = await _context.Postagens.ToListAsync();
        }
    }
}
