using BlogSimples.Web.Context;
using BlogSimples.Web.Repository.Interfaces;
using BlogSimples.Web.Repository;
using BlogSimples.Web.Service.Interfaces;
using BlogSimples.Web.Service;
using BlogSimples.Web.Models;
using System.Net.WebSockets;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Adicionado servico para notificao
builder.Services.AddSingleton<NotificationService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPostagemRepository, PostagemRepository>();
builder.Services.AddScoped<IPostagemService, PostagemService>();

var serviceProvider = builder.Services.BuildServiceProvider();
var loginService = serviceProvider.GetRequiredService<ILoginRepository>();
// Adicionar alguns logins
loginService.AddLoginAsync(new Login { Username = "user1", Password = "pass1" });
loginService.AddLoginAsync(new Login { Username = "user2", Password = "pass2" });
loginService.AddLoginAsync(new Login { Username = "user3", Password = "pass3" });

//var postagemService = serviceProvider.GetRequiredService<IPostagemRepository>();
//postagemService.AddPostagemAsync(new Postagem { UserId=2, Titulo = "Teste Blog Postagem-2", Postage = "Essa é uma postagem Teste-2" });
//postagemService.AddPostagemAsync(new Postagem { UserId = 3, Titulo = "Teste Blog Postagem", Postage = "Essa é uma postagem Teste 3" });


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);

app.Use(async (context, next) =>
{
    //if (context.Request.Path.ToString().Contains("ws"))
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var notificationService = context.RequestServices.GetRequiredService<NotificationService>();
            notificationService.AddSocket(webSocket);
            await Notify(context, webSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static async Task Notify(HttpContext context, WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

    while (!result.CloseStatus.HasValue)
    {
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    }

    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}
