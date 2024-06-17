using ChatAPI.Models;

namespace ChatServer.Models
{
    public class UserDto : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
