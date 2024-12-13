namespace CodingBlog.Domain.Entities;
using System.Collections.Generic;

public class User
{

    public User()
    {
        
    }
    
    public User(string username, string email, string password, string role)
    {
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }
    
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}