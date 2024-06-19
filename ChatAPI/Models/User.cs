namespace ChatAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Login { get; set; } 
    public List<ContactUser> Contacts { get; set; }
    public List<Chat> Chats { get; set; }
}
