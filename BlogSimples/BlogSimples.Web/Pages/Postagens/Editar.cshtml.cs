using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using BlogSimples.Web.Service.Interfaces;

namespace BlogSimples.Web.Pages.Postagens
{
    public class EditarModel : PageModel
    {
        private readonly IPostagemService _context;

        public EditarModel(IPostagemService context)
        {
            _context = context;
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var postagem = await _context.BuscarIdAsync(id);
            if (postagem == null)
            {
                // return NotFound();
            }
            Postagem = postagem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _context.AlterarAsync(Postagem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostagemExists(Postagem.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ListaPostagens", new { userId = Postagem.UserId } );
        }

        private bool PostagemExists(int id)
        {
            return _context.ListarAsync().Result.Any(e => e.PostId == id);
        }
    }
}
