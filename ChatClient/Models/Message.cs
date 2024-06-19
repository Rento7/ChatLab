using ChatAPI.Models;
using System;

namespace ChatClient.Models
{
    internal class Message : IMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
    }
}
