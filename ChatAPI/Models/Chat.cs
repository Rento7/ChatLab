namespace ChatAPI.Models;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ContactUser> Users { get; set; }
    public List<Message> Messages { get; set; }
}
