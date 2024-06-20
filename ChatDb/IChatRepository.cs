using ChatDb.Models;

namespace ChatDb;

public interface IChatRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserAsync(Guid id);
    Task<IEnumerable<Chat>> GetChatsAsync();
    Task<IEnumerable<Chat>> GetChatsByUserAsync(Guid userId);
    Task<Chat> GetChatByIdAsync(Guid id);
    Task<IEnumerable<Message>> GetMessagesAsync();
    Task<IEnumerable<Message>> GetMessagesByChatAsync(Guid chatId);
    Task<Message> GetMessageByIdAsync(Guid id);
    Task CreateUserAsync(User user);
    Task CreateChatAsync(Chat chat);
    Task CreateMessageAsync(Message message);
    Task UpdateChatName(Guid chatId, string newName);
}