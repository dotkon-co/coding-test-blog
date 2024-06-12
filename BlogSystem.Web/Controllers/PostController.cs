using BlogSystem.Application.Dto;
using BlogSystem.Application.Interfaces;
using BlogSystem.Domain.Entities;
using BlogSystem.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlogSystem.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public PostController(
            IPostService postService,
            UserManager<User> userManager,
            IHubContext<NotificationHub> hubContext)
        {
            _postService = postService;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostDto postDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                postDto.AuthorId = user.Id;
                await _postService.AddPostAsync(postDto);

                // Enviar notificação via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New post created");

                return RedirectToAction(nameof(Index));
            }
            return View(postDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostDto postDto)
        {
            if (ModelState.IsValid)
            {
                await _postService.UpdatePostAsync(postDto);

                // Enviar notificação via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Post updated");

                return RedirectToAction(nameof(Index));
            }
            return View(postDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeletePostAsync(id);

            // Enviar notificação via SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Post deleted");

            return RedirectToAction(nameof(Index));
        }
    }
}