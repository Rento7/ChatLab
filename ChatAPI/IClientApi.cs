using ChatAPI.Models;

namespace ChatAPI;

public interface IClientApi
{
    //dumb naming...
    void InitUser(User user);
    void ReceiveMessage(Message message);
    void MessageHasEdited(Message message);
    void MessageHasDeleted(Guid messageId);
    void ChatHasRenamed(Chat chat);
    void ChatHasSelected(Chat chat);
}
