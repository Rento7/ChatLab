using ChatAPI.Models;

namespace ChatAPI;

public interface IClientApi
{
    void ReceiveMessage(Message message);
    void InitUser(User user);
    void ChatHasRenamed(Chat chat);
    void ChatHasSelected(Chat chat);
}
