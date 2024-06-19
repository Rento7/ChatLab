using ChatAPI.Models;

namespace ChatAPI;

public interface IClientApi
{
    void ReceiveMessage(Message message);
    void InitUser(User user);
}
