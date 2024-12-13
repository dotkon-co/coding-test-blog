namespace CodingBlog.Infrastructure.Notification;

using Microsoft.AspNetCore.SignalR;

public interface IPostNotificationService
{
    Task SendPostNotificationAsync(string message);
}

public class PostNotificationService: IPostNotificationService
{
    private readonly IHubContext<Hub> _hubContext;

    public PostNotificationService(IHubContext<Hub> hubContext)
     => _hubContext = hubContext;
    

    public async Task SendPostNotificationAsync(string message)
     => await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
    
}