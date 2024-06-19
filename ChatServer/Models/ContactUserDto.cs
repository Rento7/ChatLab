using ChatAPI.Models;

namespace ChatServer.Models;

public class ContactUserDto : IContactUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
