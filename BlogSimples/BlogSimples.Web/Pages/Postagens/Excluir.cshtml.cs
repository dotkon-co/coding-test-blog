using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;

namespace BlogSimples.Web.Pages.Postagens
{
    public class ExcluirModel : PageModel
    {
        private readonly BlogSimples.Web.Context.AppDbContext _context;

        public ExcluirModel(BlogSimples.Web.Context.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postagem = await _context.Postagens.FirstOrDefaultAsync(m => m.PostId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postagem = await _context.Postagens.FindAsync(id);
            if (postagem != null)
            {
                Postagem = postagem;
                _context.Postagens.Remove(Postagem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
