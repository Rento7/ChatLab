﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatDb;
using ChatAPI;
using ChatServer.Models;
using ChatServer.Extensions;

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

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }

        public async Task RequestUser() 
        {
            if (Context.UserIdentifier is string id)
            {
                var user = await _repository.GetUserAsync(Guid.Parse(id));
                await Clients.User(id).SendAsync(nameof(IClientApi.InitUser), user.ToDto());
            }
        }
    }
}
