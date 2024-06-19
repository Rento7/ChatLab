namespace ChatAPI.Models;

public interface IMessage
{
    Guid Id { get; set; }
    string Text { get; set; }
    Guid SenderId { get; set; }
    Guid ChatId { get; set; }
}
