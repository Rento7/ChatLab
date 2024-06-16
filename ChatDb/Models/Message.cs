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
        public string Text { get; set; } = null!;
        public DateTime? SendTime { get; set; } = null!;
        public bool? IsEdited { get; set; } = null!;
        public DateTime? LastEditTime { get; set; } = null!;

        public User Sender { get; set; } = null!;
        public Guid SenderId { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
    }
}
