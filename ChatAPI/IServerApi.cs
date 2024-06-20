﻿using ChatAPI.Models;

namespace ChatAPI
{
    public interface IServerApi
    {
        Task RequestUser();
        Task SendMessage(Message message);
        Task RenameChat(Guid chatId, string newName);
    }
}
