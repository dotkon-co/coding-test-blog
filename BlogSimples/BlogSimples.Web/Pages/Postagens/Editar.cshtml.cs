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

namespace BlogSimples.Web.Pages.Postagens
{
    public class EditarModel : PageModel
    {
        private readonly BlogSimples.Web.Context.AppDbContext _context;

        public EditarModel(BlogSimples.Web.Context.AppDbContext context)
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

            var postagem =  await _context.Postagens.FirstOrDefaultAsync(m => m.PostId == id);
            if (postagem == null)
            {
                return NotFound();
            }
            Postagem = postagem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Postagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            //return RedirectToPage("/Index");
            return RedirectToPage("/ListaPostagens", new { userId = Postagem.UserId } );
        }

        private bool PostagemExists(int id)
        {
            return _context.Postagens.Any(e => e.PostId == id);
        }
    }
}
