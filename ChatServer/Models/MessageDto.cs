using ChatAPI.Models;

namespace ChatServer.Models;

public class MessageDto : IMessage
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
}
