
namespace ChatAPI.Models;

public interface IUser
{
    Guid Id { get; set; }
    string Name { get; set; } 
    string Login { get; set; } 
    string Password { get; set; }
}
