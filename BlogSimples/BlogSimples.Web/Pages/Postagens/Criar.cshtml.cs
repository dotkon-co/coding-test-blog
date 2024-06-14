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
using BlogSimples.Web.Service;
using BlogSimples.Web.Service.Interfaces;
using BlogSimples.Web.Controllers;

namespace BlogSimples.Web.Pages.Postagens
{
    public class CriarModel : PageModel
    {
        private readonly IPostagemService _context;
        private readonly NotificationController _notify;

        public CriarModel(IPostagemService context, NotificationController notify)
        {
            _context = context;
            _notify = notify;
        }

        [BindProperty]
        public int UserId { get; set; } = default!;

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
            await _context.GravarAsync(Postagem);
            await _notify.Notify("Postagem criada com sucesso."); 
            return RedirectToPage("/ListaPostagens", new { userId = Postagem.UserId });
        }
    }
}

