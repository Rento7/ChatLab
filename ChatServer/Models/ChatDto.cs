using ChatAPI.Models;

namespace ChatServer.Models
{
    public class ChatDto : IChat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<IContactUser> Users { get; set; } = null!;
        public List<IMessage> Messages { get; set; } = null!;
    }
}
