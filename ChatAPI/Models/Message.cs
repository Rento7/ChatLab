namespace ChatAPI.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
}
