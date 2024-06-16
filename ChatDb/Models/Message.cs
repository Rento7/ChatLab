using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDb.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public User Sender { get; set; }
        public Guid SenderId { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
