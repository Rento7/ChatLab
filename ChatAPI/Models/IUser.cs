namespace ChatAPI.Models;

public interface IUser
{
    Guid Id { get; set; }
    string Name { get; set; } 
    string Login { get; set; } 
    List<IContactUser> Contacts { get; }
    List<IChat> Chats { get; }
}
