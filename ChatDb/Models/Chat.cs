using System;
 
namespace ChatDb.Models
{
    public class Chat
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Message> Messages { get; } = new List<Message>();
        public List<User> Users { get; } = new List<User>();
    }
}
