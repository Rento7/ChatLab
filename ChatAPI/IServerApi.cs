using ChatAPI.Models;

namespace ChatAPI
{
    public interface IServerApi
    {
        Task RequestUser();
        Task SendMessage(Message message);
        Task EditMessage(Guid messageId, string newText);
        Task DeleteMessage(Guid messageId);
        Task RenameChat(Guid chatId, string newName);
        Task SelectChat(Guid guid);
    }
}
