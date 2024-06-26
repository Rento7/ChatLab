﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using ChatDb;
using ChatAPI;
using ChatServer.Extensions;

using Dto = ChatAPI.Models;

namespace ChatServer
{
    [Authorize]
    public class ChatHub : Hub, IServerApi
    {
        IChatRepository _repository;
        public ChatHub(IChatRepository chatRepository) 
        {
            _repository = chatRepository;
        }

        public async Task SendMessage(Dto.Message messageDto)
        {
            var message = messageDto.FromDto();
            await _repository.CreateMessageAsync(message);

            var chat = await _repository.GetChatByIdAsync(message.ChatId);
            var users = chat.Users.Select(user => user.Id.ToString());

            await Clients.Users(users).SendAsync(nameof(IClientApi.ReceiveMessage), message.ToDto());
        }

        public async Task EditMessage(Guid messageId, string newText)
        {
            await _repository.UpdateMessage(messageId, newText);
            var message = await _repository.GetMessageByIdAsync(messageId);
            var chat = await _repository.GetChatByIdAsync(message.ChatId);
            var users = chat.Users.Select(user => user.Id.ToString());
            await Clients.Users(users).SendAsync(nameof(IClientApi.MessageHasEdited), message.ToDto());
        }

        public async Task DeleteMessage(Guid messageId)
        {
            var message = await _repository.GetMessageByIdAsync(messageId);
            var chat = await _repository.GetChatByIdAsync(message.ChatId);
            var users = chat.Users.Select(user => user.Id.ToString());

            await _repository.DeleteMessage(messageId);

            await Clients.Users(users).SendAsync(nameof(IClientApi.MessageHasDeleted), messageId);
        }

        public async Task RequestUser() 
        {
            if (Context.UserIdentifier is string id)
            {
                var user = await _repository.GetUserAsync(Guid.Parse(id));
                await Clients.User(id).SendAsync(nameof(IClientApi.InitUser), user.ToDto());
            }
        }

        public async Task RenameChat(Guid chatId, string newName)
        {
            await _repository.UpdateChatName(chatId, newName);
            var chat = await _repository.GetChatByIdAsync(chatId);
            var users = chat.Users.Select(user => user.Id.ToString());
            await Clients.Users(users).SendAsync(nameof(IClientApi.ChatHasRenamed), chat.ToDto());
        }

        public async Task SelectChat(Guid chatId)
        {
            if (Context.UserIdentifier is string id)
            {
                var chat = await _repository.GetChatByIdAsync(chatId);
                await Clients.User(id).SendAsync(nameof(IClientApi.ChatHasSelected), chat.ToDto());
            }
        }

    }
}
