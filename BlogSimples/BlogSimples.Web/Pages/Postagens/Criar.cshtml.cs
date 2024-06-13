using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogSimples.Web.Context;
using BlogSimples.Web.Models;
using System.Dynamic;

namespace BlogSimples.Web.Pages.Postagens
{
    public class CriarModel : PageModel
    {
        private readonly BlogSimples.Web.Context.AppDbContext _context;
        
        [BindProperty]
        public int UserId { get; set; } = default!;

        public CriarModel(BlogSimples.Web.Context.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int userId)
        {
            UserId = userId;
            return Page();
        }

        [BindProperty]
        public Postagem Postagem { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            Postagem.UserId = UserId;   

            _context.Postagens.Add(Postagem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ListaPostagens", new { userId = Postagem.UserId });
        }
    }
}
