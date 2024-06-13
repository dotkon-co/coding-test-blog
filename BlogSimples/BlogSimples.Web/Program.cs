using BlogSimples.Web.Context;
using BlogSimples.Web.Repository.Interfaces;
using BlogSimples.Web.Repository;
using BlogSimples.Web.Service.Interfaces;
using BlogSimples.Web.Service;
using BlogSimples.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
