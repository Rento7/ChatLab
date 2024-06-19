using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatDb;
using ChatAPI;
using ChatServer.Extensions;
using ChatAPI.Models;

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

        public async Task SendMessage(Message message)
        {
            //TODO change
            //await Clients.All.SendAsync("Receive", message);
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
