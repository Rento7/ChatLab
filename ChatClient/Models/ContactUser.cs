using ChatAPI.Models;
using System;

namespace ChatClient.Models;

internal class ContactUser : IContactUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
