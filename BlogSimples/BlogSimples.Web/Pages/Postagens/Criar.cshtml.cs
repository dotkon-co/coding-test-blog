using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;

namespace BlogSimples.Web.Pages.Postagens
{
    public class CriarModel : PageModel
    {
        private readonly BlogSimples.Web.Context.AppDbContext _context;

        public CriarModel(BlogSimples.Web.Context.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Postagens.Add(Postagem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
