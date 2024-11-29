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

namespace BlogSimples.Web.Pages.Postagens
{
    public class ExcluirModel : PageModel
    {
        private readonly IPostagemService _context;

        public ExcluirModel(IPostagemService context)
        {
            _context = context;
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;

        [BindProperty]
        public int UserId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var postagem = await _context.BuscarIdAsync(id);
            UserId = postagem.UserId;
            if (postagem == null)
            {
                return NotFound();
            }
            else
            {
                Postagem = postagem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var postagem = await _context.BuscarIdAsync(id);
            if (postagem != null)
            {
                Postagem = postagem;
                await _context.DeletarAsync(Postagem);
              //await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ListaPostagens", new { userId = Postagem.UserId });
        }
    }
}
