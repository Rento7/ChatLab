using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDb.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public User ContactsOwner { get; set; } = null!;
        public Guid? ContactsOwnerId { get; set; } = null!;
        public ICollection<User> Contacts { get; } = new List<User>();
        public List<Chat> Chats { get; } = new List<Chat>();
    }
}