using BlogSimples.Web.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogSimples.Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Notify()
        //{
        //    await _notificationService.NotifyAllAsync("This is a notification!");
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Notify(string mensagem)
        {
            await _notificationService.NotifyAllAsync(mensagem);
            return Ok();
        }
    }
}
