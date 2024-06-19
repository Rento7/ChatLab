namespace ChatAPI.Models;

public interface IChat
{
    Guid Id { get; set; }
    string Name { get; set; }
    List<IContactUser> Users { get; set; }
    List<IMessage> Messages { get; set; }
}
