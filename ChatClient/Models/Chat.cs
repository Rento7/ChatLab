﻿using ChatAPI.Models;
using System.Collections.Generic;
using System;

namespace ChatClient.Models;

internal class Chat : IChat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<IContactUser> Users { get; set; } = null!;
    public List<IMessage> Messages { get; set; } = null!;
}
