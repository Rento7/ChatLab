using System;
using System.Collections.Generic;
using ChatAPI.Models;

namespace ChatClient.Models
{
    internal class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public List<IContactUser> Contacts { get; set; } = null!;
        public List<IChat> Chats { get; set; } = null!;
    }
}
