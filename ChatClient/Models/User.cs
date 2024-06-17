using System;
using ChatAPI.Models;

namespace ChatClient.Models
{
    internal class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
